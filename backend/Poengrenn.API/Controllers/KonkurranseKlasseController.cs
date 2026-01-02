using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Poengrenn.DAL.Models;
using Poengrenn.DAL.EFRepository;

namespace Poengrenn.API.Controllers
{
    [ApiController]
    [Route("api/konkurranseklasse")]
    public class KonkurranseKlasseController : ControllerBase
    {
        private readonly EFPoengrennRepository<KonkurranseKlasse> _konkurranseKlasseRepo;
        
        public KonkurranseKlasseController(
            EFPoengrennRepository<KonkurranseKlasse> konkurranseKlasseRepo)
        {
            _konkurranseKlasseRepo = konkurranseKlasseRepo;
        }

        // GET api/konkurranseklasse
        [HttpGet("")]
        public IEnumerable<KonkurranseKlasse> Get()
        {
            return _konkurranseKlasseRepo.Get();
        }

        // GET api/konkurranseklasse/type/{typeID}
        [HttpGet("type/{typeID}")]
        public IEnumerable<KonkurranseKlasse> GetByTypeID(string typeID)
        {
            if (_konkurranseKlasseRepo.Any(k => k.TypeID == typeID))
                return _konkurranseKlasseRepo.Get(k => k.TypeID == typeID);
            return _konkurranseKlasseRepo.Get(k => k.TypeID == "0");
        }

        // GET api/konkurranseklasse/5
        [HttpGet("{id}")]
        public KonkurranseKlasse Get(int id)
        {
            return _konkurranseKlasseRepo.GetByID(id);
        }

        // POST api/konkurranseklasse
        [HttpPost("")]
        public KonkurranseKlasse Post(KonkurranseKlasse nyKonkurranseKlasse)
        {
            return _konkurranseKlasseRepo.Insert(nyKonkurranseKlasse);
        }

        // PUT api/konkurranseklasse
        [HttpPut("")]
        public KonkurranseKlasse Put(KonkurranseKlasse konkurranseKlasse)
        {
            var konkurranseKlasseUpdate = _konkurranseKlasseRepo.Get(k => k.KlasseID == konkurranseKlasse.KlasseID && k.TypeID == konkurranseKlasse.TypeID).SingleOrDefault();
            konkurranseKlasseUpdate.Navn = konkurranseKlasse.Navn;
            konkurranseKlasseUpdate.ForsteStartnummer = konkurranseKlasse.ForsteStartnummer;
            konkurranseKlasseUpdate.SisteStartnummer = konkurranseKlasse.SisteStartnummer;
            konkurranseKlasseUpdate.MedTidtaking = konkurranseKlasse.MedTidtaking;
            konkurranseKlasseUpdate.DistanseKm = konkurranseKlasse.DistanseKm;
            return _konkurranseKlasseRepo.Update(konkurranseKlasseUpdate);
        }

        // POST api/konkurranseklasse
        [HttpDelete("{id}")]
        public bool Delete(string id)
        {
            var konkurranseKlasseDelete = _konkurranseKlasseRepo.Get(k => k.KlasseID == id).SingleOrDefault();
            return _konkurranseKlasseRepo.Delete(konkurranseKlasseDelete);
        }
    }

    
}
