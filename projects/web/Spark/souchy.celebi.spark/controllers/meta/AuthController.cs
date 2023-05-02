using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using souchy.celebi.spark.models;
using souchy.celebi.spark.models.settings;
using souchy.celebi.spark.services.meta;
using Spark;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
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
