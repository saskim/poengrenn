using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Poengrenn.DAL.Models;
using Poengrenn.DAL.EFRepository;

namespace Poengrenn.API.Controllers
{
    [ApiController]
    [Route("api/konkurransetype")]
    public class KonkurranseTypeController : ControllerBase
    {
        private readonly EFPoengrennRepository<KonkurranseType> _konkurranseTypeRepo;
        
        public KonkurranseTypeController(EFPoengrennRepository<KonkurranseType> konkurranseTypeRepo)
        {
            _konkurranseTypeRepo = konkurranseTypeRepo;
        }

        // GET api/konkurransetype
        [HttpGet("")]
        public IEnumerable<KonkurranseType> Get()
        {
            return _konkurranseTypeRepo.Get(t => t.Aktiv == true);
        }

        // GET api/konkurransetype/5
        [HttpGet("{id}")]
        public KonkurranseType Get(int id)
        {
            return _konkurranseTypeRepo.GetByID(id);
        }

        // POST api/konkurransetype
        [HttpPost("")]
        public KonkurranseType Post(KonkurranseType nyKonkurranseType)
        {
            return _konkurranseTypeRepo.Insert(nyKonkurranseType);
        }

        // PUT api/konkurransetype/5
        [HttpPut("")]
        public KonkurranseType Put(KonkurranseType konkurranseType)
        {
            var konkurranseTypeUpdate = _konkurranseTypeRepo.GetByID(konkurranseType.TypeID);
            if (konkurranseTypeUpdate == null)
                return null;

            konkurranseTypeUpdate.Navn = konkurranseType.Navn;
            konkurranseTypeUpdate.StandardAntallKonkurranser = konkurranseType.StandardAntallKonkurranser;
            return _konkurranseTypeRepo.Update(konkurranseTypeUpdate);
        }
        
    }

    
}
