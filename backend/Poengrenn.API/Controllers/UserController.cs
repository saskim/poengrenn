using Poengrenn.API.Models;
using Poengrenn.DAL.EFRepository;
using Poengrenn.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Poengrenn.API.Controllers
{
    [ApiController]
    [Route("api/bruker")]
    public class UserController : ControllerBase
    {
        private readonly EFPoengrennRepository<Person> _personInfoRepo;

        public UserController(EFPoengrennRepository<Person> personInfoRepo)
        {
            _personInfoRepo = personInfoRepo;
        }
        
        [HttpPost("login")]
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

                var person = _personInfoRepo.GetByID(personId);
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

        [HttpPost("logout")]
        public void Post()
        {
            
        }

    }
}
