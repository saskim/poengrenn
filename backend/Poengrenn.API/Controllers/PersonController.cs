using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Poengrenn.DAL.Models;
using Poengrenn.DAL.EFRepository;

namespace Poengrenn.API.Controllers
{
    [ApiController]
    [Route("api/person")]
    public class PersonController : ControllerBase
    {
        private readonly EFPoengrennRepository<Person> _personInfoRepo;

        public PersonController(EFPoengrennRepository<Person> personInfoRepo)
        {
            _personInfoRepo = personInfoRepo;
        }


        // GET api/persons
        [HttpGet("")]
        public IEnumerable<Person> Get()
        {
            return _personInfoRepo.Get();
            //return new string[] { "value1", "value2" };
        }

        // GET api/persons/5
        [HttpGet("{id}")]
        public Person Get(int id)
        {
            return _personInfoRepo.GetByID(id);
            //return "value";
        }

        // POST api/persons
        [HttpPost("")]
        public Person Post([FromBody] Person person)
        {
            return _personInfoRepo.Insert(person);
        }

        // PUT api/persons/5
        [HttpPut("")]
        public Person Put(Person person)
        {
            var updatePerson = _personInfoRepo.GetByID(person.PersonID);
            updatePerson.Fornavn = person.Fornavn;
            updatePerson.Etternavn = person.Etternavn;
            updatePerson.Fodselsar = person.Fodselsar;
            updatePerson.Kjonn = person.Kjonn;
            updatePerson.Epost = person.Epost;
            updatePerson.Telefon = person.Telefon;
            return _personInfoRepo.Update(updatePerson);
        }

        // DELETE api/persons/5
        [HttpDelete("")]
        public void Delete(int id)
        {
            
        }
    }
}
