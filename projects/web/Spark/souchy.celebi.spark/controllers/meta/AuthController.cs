using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using souchy.celebi.spark.models;
using souchy.celebi.spark.models.settings;
using souchy.celebi.spark.services.meta;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace souchy.celebi.spark.controllers.meta
{

    [ApiController]
    [Route(Routes.Meta + "auth")]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly AccountService accountService;
        private readonly UserManager<Account> accountManager;
        private readonly RoleManager<AccountRole> roleManager;
        private readonly SignInManager<Account> signinManager;
        private readonly JwtSettings jwtSettings;
        private readonly GoogleSettings googleSettings;
        private readonly AdminUserSettings adminUserSettings;

        public AuthController(AccountService service, UserManager<Account> accountManager, RoleManager<AccountRole> roleManager,
            SignInManager<Account> signinManager,
            IOptions<JwtSettings> jwtSettings,
            IOptions<GoogleSettings> googleSettings,
            IOptions<AdminUserSettings> adminUserSettings
        )
        {
            accountService = service;
            this.accountManager = accountManager;
            this.roleManager = roleManager;
            this.signinManager = signinManager;
            this.jwtSettings = jwtSettings.Value;
            this.googleSettings = googleSettings.Value;
            this.adminUserSettings = adminUserSettings.Value;
            //this.config = config;

            CreateRoles();
        }

        /// <summary>
        /// this sucks here, need to put it in Spark.cs somehow at startup.
        /// + not all roles get created (missing admin) + the admin account isnt created eitehr
        /// </summary>
        private async Task CreateRoles() //IServiceProvider serviceProvider, IConfiguration configuration)
        {
            //initializing custom roles 
            //var accountManager = serviceProvider.GetRequiredService<UserManager<Account>>();
            //var roleManager = serviceProvider.GetRequiredService<RoleManager<AccountRole>>();
            //string[] roleNames = { "Admin", "Manager", "Member" };
            IdentityResult roleResult;

            foreach (var role in Enum.GetValues<AccountType>())
            {
                var roleExist = await roleManager.RoleExistsAsync(Enum.GetName(role)!);
                if (!roleExist)
                {
                    //create the roles and seed them to the database: Question 1
                    roleResult = await roleManager.CreateAsync(new AccountRole(role));
                }
            }

            //Here you could create a super user who will maintain the web app
            var poweruser = new Account
            {
                UserName = adminUserSettings.Username,
                Email = adminUserSettings.Email,
                EmailConfirmed = true,
                Info = new AccountInfo()
                {
                    Currency = 1000,
                    DisplayName = adminUserSettings.DisplayName //configuration["admin:displayname"]!
                }
            };
            //Ensure you have these values in your appsettings.json file
            string userPWD = adminUserSettings.Password; //configuration["admin:passwd"]!;
            var _user = await accountManager.FindByEmailAsync(poweruser.Email!);

            if (_user == null)
            {
                var createPowerUser = await accountManager.CreateAsync(poweruser, userPWD);
                if (createPowerUser.Succeeded)
                {
                    //here we tie the new user to the role
                    await accountManager.AddToRoleAsync(poweruser, nameof(AccountType.Admin));
                }
            }
        }

        #region shared
        [HttpGet("mammoth")]
        public RedirectResult mammoth()
        {
            return Redirect("https://localhost:9000");
        }

        [HttpGet("ping")]
        public ActionResult<string> ping()
        {
            Debug.WriteLine(Request.Cookies);
            if (new DateTime().Year > 2024) return BadRequest();
            return Ok("pong");
        }

        [Authorize] //(Roles = nameof(AccountType.VerifiedUser))]
        [HttpGet("privatePing")]
        public ActionResult<string> privatePing()
        {
            Debug.WriteLine(Request.Cookies);
            if (new DateTime().Year > 2024) return BadRequest();
            return Ok("private");
        }
        [Authorize]
        [HttpPost("displayName")]
        public async Task<bool> setDisplayName(string displayname)
        {
            var account = await accountManager.GetUserAsync(this.User);
            if (account == null) return false; // idk why this could be null if we have [authorize] ? // -> i think if we have a cookie token from Google, but no Celebi Account yet
            var existing = await accountService.FindByDisplayName(displayname);
            if (existing != null) return false;
            account.Info.DisplayName = displayname;
            await accountManager.UpdateAsync(account);
            return true;
        }
        [Authorize]
        [HttpGet("accountInfo")]
        public async Task<AccountInfo?> GetAccountInfo()
        {
            var acc = await accountManager.GetUserAsync(this.User);
            if(acc == null) return null;
            return acc.Info;
        }
        #endregion


        #region identity


        /// <summary>
        /// Creates an account and signs in automatically with jwt token (signinManager identity)
        /// </summary>
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        [HttpPost("identitySignup")]
        public async Task<ActionResult<AccountInfo>> identitySignup([Required] string displayName, [Required][EmailAddress] string email, [Required] string pass)
        {
            var existing = await accountManager.FindByEmailAsync(email);
            if (existing != null) return BadRequest("Already exists");
            existing = await accountService.FindByDisplayName(displayName);
            if (existing != null) return BadRequest("Already exists");

            var account = new Account() { 
                Email = email, 
                UserName = email, 
                Info = new AccountInfo() { 
                    DisplayName = displayName 
                } 
            };

            var result = await accountManager.CreateAsync(account, pass);
            if (!result.Succeeded)
                return BadRequest(result.Errors.ToList()); // Problem("User creation failed: " + string.Join(", ", result.Errors));

            await accountManager.AddToRoleAsync(account, nameof(AccountType.User));
            await this.signinManager.SignInAsync(account, false);
            return Ok(account.Info);
        }

        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        [HttpPost("identitySigninUsername")]
        public async Task<ActionResult<AccountInfo>> identitySigninUsername([Required] string username, [Required] string pass)
        {
            var account = await accountManager.FindByNameAsync(username);
            if (account == null) return this.NoContent();
            SignInResult result = await this.signinManager.PasswordSignInAsync(account, pass, true, false);
            if (result.Succeeded) return Ok(account.Info);
            else return Unauthorized(result.IsNotAllowed);
        }

        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        [HttpPost("identitySigninEmail")]
        public async Task<ActionResult<AccountInfo>> identitySigninEmail([Required][EmailAddress] string email, [Required] string pass)
        {
            var account = await accountManager.FindByEmailAsync(email);
            if (account == null) return this.NoContent();
            SignInResult result = await this.signinManager.PasswordSignInAsync(account, pass, true, false);
            if(result.Succeeded) return Ok(account.Info);
            else return Unauthorized(result.IsNotAllowed);
        }

        /// <summary>
        /// Creates an account and signs in automatically with jwt token (signinManager identity)
        /// </summary>
        //[ValidateAntiForgeryToken]
        //[Authorize]
        [AllowAnonymous]
        [HttpPost("identitySigninGoogle")]
        public async Task<ActionResult<AccountInfo>> identitySigninGoogle(string idToken)
        {

            if (this.User == null) return new ForbidResult();
            //string email = this.User.Claims.Single(c => c.Type == ClaimTypes.Email).Value;
            //string name = this.User.Claims.Single(c => c.Type == ClaimTypes.Name).Value;
            //string givenName = this.User.Claims.Single(c => c.Type == ClaimTypes.GivenName).Value;
            //string emailVerified = this.User.Claims.First(c => c.Type == "email_verified").Value;

            //string exp = this.User.Claims.First(c => c.Type == "exp").Value;
            //string iat = this.User.Claims.First(c => c.Type == "iat").Value;
            //string nbf = this.User.Claims.First(c => c.Type == "nbf").Value;
            //string name = this.User.Claims.Single(c => c.Type == ClaimTypes.).Value;
            // aud/azp : google url
            // email
            // email_verified
            // exp, iat, nbf: 1685494798
            // given_name: Souchy
            // sub: some id
            // picture

            //this.HttpContext.User.
            var accessToken = Request.Headers[HeaderNames.Authorization];
            var validationSettings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string>() { googleSettings.ClientId }
            };
            var validPayload = await GoogleJsonWebSignature.ValidateAsync(idToken, validationSettings);


            var account = await accountManager.FindByEmailAsync(validPayload.Email);
            if (account == null)
            {
                account = new Account()
                {
                    Email = validPayload.Email,
                    UserName = validPayload.Email,
                    EmailConfirmed = validPayload.EmailVerified, //  bool.Parse(emailVerified), //
                    Info = new AccountInfo()
                    {
                        DisplayName = validPayload.Name
                    }
                };
                var claims = this.User.Claims.Select(c => new IdentityUserClaim<string>()
                {
                    ClaimType = c.Type,
                    ClaimValue = c.Value,
                });
                account.Claims.AddRange(claims);

                // create
                var result = await this.accountManager.CreateAsync(account);
                if (result.Succeeded)
                {
                    if (account.EmailConfirmed)
                        await accountManager.AddToRoleAsync(account, nameof(AccountType.VerifiedUser));
                    else
                        await accountManager.AddToRoleAsync(account, nameof(AccountType.User));
                    await this.signinManager.SignInAsync(account, true);
                    //await this.signinManager.ExternalLoginSignInAsync("loginProvider", "providerKey", true);
                    return Ok(account.Info);
                }
                else
                {
                    return Problem("Error while creating the account.");
                }
            } else
            {
                //await this.accountManager.AddToRoleAsync(account, nameof(AccountType.Admin));
                await this.signinManager.SignInAsync(account, true);
                return Ok(account.Info);
                //SignInResult result = await this.signinManager.PasswordSignInAsync(account, pass, false, false);
                //if (result.Succeeded) return Ok(account.Info);
                //else return Unauthorized(result.IsNotAllowed);
            }

        }

        [AllowAnonymous]
        [HttpPost("signinMicrosoft")]
        public async Task signinMicrosoft()
        {

        }

        [Authorize]
        [HttpPost("identitySignout")]
        public async Task identitySignout()
        {
            await signinManager.SignOutAsync();
        }

        [HttpPost("identitySigninExternal")]
        public async Task identitySigninWithToken()
        {
            await this.accountManager.FindByIdAsync("userid");
            await this.accountManager.FindByLoginAsync("loginProvider", "providerKey");

            await signinManager.ExternalLoginSignInAsync("loginProvider", "providerKey", false);
            //this.accountManager.token
            //this.signinManager.IsSignedIn/
        }
        [HttpGet("verify")]
        public async Task verifyToken(string token)
        {
            // TODO verify token from TCP game server
            // flow:
            //      - client signs into the http server
            //      - client obtains account info + cookie token // problem: it's httponly
            //      - client signs into the TCP game server with the token
            //      - game server verifies token with spark
            //      - game server accepts the connection
        }
        #endregion

    }
}
