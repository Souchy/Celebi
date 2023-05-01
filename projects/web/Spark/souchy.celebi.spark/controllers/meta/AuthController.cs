//using Microsoft.AspNetCore.Mvc;
using Azure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using souchy.celebi.spark.models;
using souchy.celebi.spark.services;
using souchy.celebi.spark.services.meta;
using Spark;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
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
        public AuthController(AccountService service, UserManager<Account> accountManager, RoleManager<AccountRole> roleManager,
            SignInManager<Account> signinManager,
            IOptions<JwtSettings> jwtSettings
        )
        {
            accountService = service;
            this.accountManager = accountManager;
            this.roleManager = roleManager;
            this.signinManager = signinManager;
            this.jwtSettings = jwtSettings.Value;
            //this.config = config;

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
            if (account == null) return false; // idk why this could be null if we have [authorize] ?
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

        /*
        [HttpPost("signUp")]
        public async Task<ActionResult<Account>> signUp([FromBody] Account acc)
        {
            var auth = Request.Headers.Authorization;
            var cookies = Request.Cookies;
            Account newAcc = acc != null ? acc : new Account();
            bool success = await accountService.Create(newAcc);
            if (success) return Ok(acc);
            else return BadRequest();
        }
        [HttpPost("signIn")]
        public async Task<ActionResult<Account>> signIn([FromBody] Account acc) // signin with either: tokenID or email+pass
        {
            var auth = Request.Headers.Authorization;
            var cookies = Request.Cookies;
            Debug.WriteLine("auth: " + auth);
            Debug.WriteLine("cookies: " + cookies);
            Account? account = await accountService.FindAuthorizedAccount(acc);
            if(account == null) return this.Unauthorized();
            else return account;
        }
        [HttpPost("signOut")]
        public async Task<IActionResult> signOut()
        {
            var auth = Request.Headers.Authorization;
            var cookies = Request.Cookies;
            Debug.WriteLine("auth: " + auth);
            if (true) return Ok();
            else return BadRequest();
        }
        */

        #region reg
        /*
        [HttpPost("regup")]
        public async Task<ActionResult<TokenResponse>> regup(string displayName, string email, string pass)
        {
            var existing = await accountManager.FindByEmailAsync(email);
            if (existing != null) return BadRequest("Already exists");
            existing = await accountService.FindByDisplayName(displayName);
            if (existing != null) return BadRequest("Already exists");

            var account = new Account()
            {
                Email = email,
                UserName = email,
                Info = new AccountInfo()
                {
                    DisplayName = displayName
                }
            };
            var token = GetAccessToken(account);

            var result = await accountManager.CreateAsync(account, pass);
            if (!result.Succeeded)
                return BadRequest(result.Errors.ToList()); // Problem("User creation failed: " + string.Join(", ", result.Errors));
           
            return Ok(token);
        }
        [HttpPost("regin")]
        public async Task<ActionResult<TokenResponse>> regin(string email, string pass)
        {
            var account = await accountManager.FindByEmailAsync(email);
            if (account == null) return this.NoContent();
            var result = await accountManager.CheckPasswordAsync(account, pass);
            if (!result) return Unauthorized();

            //account.AccessByIp
            var ip = this.Request.HttpContext.Connection.RemoteIpAddress;
            var port = this.Request.HttpContext.Connection.RemotePort;
            account.Info.CheckAccess(ip + ":" + port);
            account.Info.AddAccess();

            var accesser = grantAccess(account);
            return accesser;
        }
        [HttpGet("refreshToken")]
        public async Task<ActionResult<TokenResponse>> refresh()
        {
            string? token = null;
            Request.Cookies.TryGetValue("refresh", out token);
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            var user = await accountManager.GetUserAsync(this.User);
            return grantAccess(user); // GetAccessToken(user);
        }
        // Dont need to do anything on the server, just delete the token on the client when they press Signout and redirect to home
        // Maybe we can call this if theres something additional you want to do, for example setting "isOnline" flag in the account idk (like to show friends who's online)
        //   actually we do have the list of ipAccess we can use to track the last connection and then compare with current time to see if elapsedTime > timeout
        [Authorize]
        [HttpPost("regout")]
        public async Task regout()
        {
            var account = await accountManager.GetUserAsync(this.User);
            //account.
            //    RefreshToken
            if (account == null) return;
            Response.Cookies.Delete("refresh");
            Redirect("localhost:9000/home");
        }
        */
        #endregion


        #region identity
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

            await this.signinManager.SignInAsync(account, false);
            return Ok(account.Info);
        }
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        [HttpPost("identitySignin")]
        public async Task<ActionResult<AccountInfo>> identitySignin([Required][EmailAddress] string email, [Required] string pass)
        {
            var account = await accountManager.FindByEmailAsync(email);
            if (account == null) return this.NoContent();
            SignInResult result = await this.signinManager.PasswordSignInAsync(account, pass, false, false);
            if(result.Succeeded) return Ok(account.Info);
            else return Unauthorized(result.IsNotAllowed);
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
        #endregion

        /*
        private TokenResponse grantAccess(Account account)
        {
            var accesser = GetAccessToken(account);
            Response.Cookies.Append("bearer", accesser.Token, new CookieOptions()
            {
                HttpOnly = true,
                Secure = true,
                Domain = jwtSettings.Issuer,
                Path = "neversendthiscookieagain",
                Expires = accesser.Expiration
                //SameSite = SameSiteMode.None
            });
            var refresher = GetRefreshToken(account);
            // account.SetRefreshToken(refresher); // TODO: set it in database so that we can revoke it from all devices
            Response.Cookies.Append("refresh", refresher.Token, new CookieOptions()
            {
                HttpOnly = true,
                Secure = true,
                Domain = jwtSettings.Issuer,
                Path = "meta/auth/refreshToken"
                //SameSite = SameSiteMode.None
            });
            return accesser;
        }

        private TokenResponse GetAccessToken(Account acc)
        {

            var claims = new List<Claim>();
            foreach (var claim in acc.Claims)
            {
                claims.Add(new Claim(claim.ClaimType!, claim.ClaimValue!));
            }
            foreach (var role in acc.Roles) //accountManager.GetRolesAsync(acc);
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            //claims.AddRange(new[]
            //{
            //    new Claim(nameof(Account.Id), "mongoUser.mongoId"),
            //    new Claim(nameof(Account.DisplayName), "account.DisplayName"),
            //    //new Claim("Id", Guid.NewGuid().ToString()),
            //    //new Claim(JwtRegisteredClaimNames.Sub, ""), //user.UserName),
            //    new Claim(JwtRegisteredClaimNames.Email, acc.Email!), //user.UserName),
            //    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            //});

            var key = Encoding.ASCII.GetBytes(jwtSettings.SecretKey);
            var desc = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(nameof(Account.Id), "mongoUser.mongoId"),
                    new Claim(nameof(Account.Info.DisplayName), "account.DisplayName"),
                    //new Claim("Id", Guid.NewGuid().ToString()),
                    //new Claim(JwtRegisteredClaimNames.Sub, ""), //user.UserName),
                    new Claim(JwtRegisteredClaimNames.Email, acc.Email!), //user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                }),
                //Claims = claims,
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(20),
                Issuer = jwtSettings.Issuer,
                Audience = jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var hand = new JwtSecurityTokenHandler();
            var token = hand.CreateToken(desc);
            var jwt = hand.WriteToken(token);
            //string str = hand.WriteToken(token);
            //desc.Expires

            return new(jwt, desc.Expires.Value);
        }

        private TokenResponse GetRefreshToken(Account acc)
        {
            var key = Encoding.ASCII.GetBytes(jwtSettings.SecretKey);
            var desc = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(nameof(Account.Id), "mongoUser.mongoId"),
                    new Claim(nameof(Account.Info.DisplayName), "account.DisplayName"),
                    new Claim("random", GenerateRandomString()),
                    new Claim(JwtRegisteredClaimNames.Email, acc.Email!),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                }),
                IssuedAt = DateTime.UtcNow,
                Issuer = jwtSettings.Issuer,
                Audience = jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var hand = new JwtSecurityTokenHandler();
            var token = hand.CreateToken(desc);
            var jwt = hand.WriteToken(token);
            return new(jwt, desc.Expires);
        }

        private string GenerateRandomString()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        */
    }
}
