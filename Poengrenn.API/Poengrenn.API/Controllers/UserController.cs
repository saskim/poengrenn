using Poengrenn.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Poengrenn.API.Controllers
{
    [RoutePrefix("api/bruker")]
    [AllowAnonymous]
    public class UserController : ApiController
    {
        // GET api/user
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/user/5
        public string Get(int id)
        {
            return "value";
        }

        [Route("registrer")]
        [HttpPost]
        public void Post([FromBody]RegistrerBruker bruker)
        {
        }

        [Route("login")]
        [HttpPost]
        public string Post(LoginBruker bruker)
        {
            return "jwt token";
        }

        [Route("logout")]
        [HttpPost]
        public void Post()
        {
            
        }

    }
}
