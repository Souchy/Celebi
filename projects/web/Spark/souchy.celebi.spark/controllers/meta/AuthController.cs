//using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using souchy.celebi.spark.models;
using souchy.celebi.spark.services.meta;
using System.Diagnostics;

namespace souchy.celebi.spark.controllers.meta
{
    [ApiController]
    [Produces("application/json")]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly AccountService accountService;
        public AuthController(AccountService service) => accountService = service;

        [HttpGet("ping")]
        public ActionResult<string> ping()
        {
            Debug.WriteLine(Request.Cookies);
            if (new DateTime().Year > 2024) return BadRequest();
            return Ok("pong");
        }

        [Authorize]
        [HttpGet("privatePing")]
        public ActionResult<string> privatePing()
        {
            Debug.WriteLine(Request.Cookies);
            if (new DateTime().Year > 2024) return BadRequest();
            return Ok("private");
        }
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

        //[HttpPost]
        //public void signIn()
        //{

        //}

        //public void refresh()
        //{

        //}

        //public void signOut()
        //{

        //}



    }
}
