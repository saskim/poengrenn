using Poengrenn.API.Models;
using Poengrenn.DAL.EFRepository;
using Poengrenn.DAL.Models;
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
        private readonly EFPoengrennRepository<Person> _personInfoRepo;

        public UserController()
        {
            _personInfoRepo = new EFPoengrennRepository<Person>();
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
                var personIDer = _personInfoRepo.Get(p => p.Epost == bruker.Passord || p.Telefon == bruker.Passord).Select(x => x.PersonID);
                if (person != null && ((person.Epost != null && person.Epost.ToLower() == bruker.Passord.ToLower()) || (person.Telefon != null && person.Telefon.Replace(" ", "") == bruker.Passord)))
                {
                    loginResponse.Rolle = "user";
                    loginResponse.Token = "jwtusertoken"; // TODO
                    loginResponse.PersonIDer = personIDer;
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
