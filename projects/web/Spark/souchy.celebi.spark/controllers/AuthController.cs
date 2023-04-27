//using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
//using System.Web.Http;
//using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;

namespace souchy.celebi.spark.controllers
{
    public class AuthController : ControllerBase // ApiController //
    {
        //[HttpGet]
        [HttpGet("ping")]
        public string ping()
        {
            Debug.WriteLine(Request.Cookies);
            //Console.WriteLine(token);
            return "pong";
        }

        [Authorize]
        //[HttpGet]
        [HttpGet("privatePring")]
        public string privatePring()
        {
            Debug.WriteLine(Request.Cookies);
            return "private";
        }

        //[Route("google")]
        //[HttpPost]
        [HttpPost("google")]
        public void signInGoogle()
        {

        }

        public void signIn()
        {

        }

        public void refresh()
        {

        }

        public void signOut()
        {

        }



    }
}
