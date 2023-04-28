//using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using souchy.celebi.spark.models;
using souchy.celebi.spark.services;
using System.Diagnostics;

namespace souchy.celebi.spark.controllers
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
            if(new DateTime().Year > 2024) return this.BadRequest();
            return Ok("pong");
        }

        [Authorize]
        [HttpGet("privatePring")]
        public ActionResult<string> privatePring()
        {
            Debug.WriteLine(Request.Cookies);
            if (new DateTime().Year > 2024) return this.BadRequest();
            return Ok("private");
        }

        [HttpPost("signUp")]
        public async Task<bool> signUp()
        {
            var auth = Request.Headers.Authorization;
            var cookies = Request.Cookies;
            Account newAcc = new Account();
            var success = await accountService.Create(newAcc);
            return success;
        }
        [HttpPost("signIn")]
        public void signIn()
        {
            var auth = Request.Headers.Authorization;
            var cookies = Request.Cookies;
            Account account = null;

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
