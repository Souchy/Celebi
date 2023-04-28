using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace souchy.celebi.spark.controllers
{
    [ApiController]
    [Route("test")]
    //[Route("api/[controller]")]
    public class TestController : ControllerBase
    {

        [HttpGet("ping")]
        public string ping()
        {
            return "pong";
        }

        [Authorize]
        [HttpGet("privatePring")]
        public string privatePing()
        {
            return "pong";
        }


    }
}
