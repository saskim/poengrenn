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
    public class UserController : ApiController
    {
        public UserController()
        {
            
        }
        
        [Route("login")]
        [HttpPost]
        public LoginResponse Post(LoginBruker bruker)
        {
            var loginResponse = new LoginResponse();
            loginResponse.Brukernavn = bruker.Brukernavn;

            if (bruker.Brukernavn == "admin" && bruker.Passord == "klabbe333")
            {
                loginResponse.Rolle = "admin";
                loginResponse.Token = "jwtadmintoken"; // TODO
            }
            else
            {
                int personId = 0;
                Int32.TryParse(bruker.Brukernavn, out personId);
                if (personId == 0)
                    return null;

                var personCtrl = new PersonController();
                var person = personCtrl.Get(personId);
                if (person != null && (person.Epost == bruker.Passord || person.Telefon == bruker.Passord))
                {
                    loginResponse.Rolle = "user";
                    loginResponse.Token = "jwtusertoken"; // TODO
                }
            }
            return loginResponse;
        }

        [Route("logout")]
        [HttpPost]
        public void Post()
        {
            
        }

    }
}
