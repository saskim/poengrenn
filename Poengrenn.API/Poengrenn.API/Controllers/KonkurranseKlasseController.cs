using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Poengrenn.DAL.Models;
using Poengrenn.DAL.EFRepository;
using Poengrenn.API.Models;
using Poengrenn.API.Models.Enums;

namespace Poengrenn.API.Controllers
{
    [RoutePrefix("api/konkurranseklasse")]
    public class KonkurranseKlasseController : ApiController
    {
        private readonly EFPoengrennRepository<KonkurranseKlasse> _konkurranseKlasseRepo;
        private readonly EFPoengrennRepository<Konkurranse> _konkurranseRepo;
        
        public KonkurranseKlasseController()
        {
            _konkurranseKlasseRepo = new EFPoengrennRepository<KonkurranseKlasse>();
            _konkurranseRepo = new EFPoengrennRepository<Konkurranse>();
        }

        // GET api/konkurranseklasse
        [Route("")]
        [HttpGet]
        public IEnumerable<KonkurranseKlasse> Get()
        {
            return _konkurranseKlasseRepo.Get();
        }

        // GET api/konkurranseklasse/type/{typeID}
        [Route("type/{typeID}")]
        [HttpGet]
        public IEnumerable<KonkurranseKlasse> GetByTypeID(string typeID)
        {
            if (_konkurranseKlasseRepo.Any(k => k.TypeID == typeID))
                return _konkurranseKlasseRepo.Get(k => k.TypeID == typeID);
            return _konkurranseKlasseRepo.Get(k => k.TypeID == "0");
        }

        // GET api/konkurranseklasse/5
        [Route("{id}")]
        [HttpGet]
        public KonkurranseKlasse Get(int id)
        {
            return _konkurranseKlasseRepo.GetByID(id);
        }

        // POST api/konkurranseklasse
        [Route("")]
        [HttpPost]
        public KonkurranseKlasse Post(KonkurranseKlasse nyKonkurranseKlasse)
        {
            return _konkurranseKlasseRepo.Insert(nyKonkurranseKlasse);
        }

        // PUT api/konkurranseklasse
        [Route("")]
        [HttpPut]
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
        [Route("{id}")]
        [HttpDelete]
        public bool Delete(string id)
        {
            var konkurranseKlasseDelete = _konkurranseKlasseRepo.Get(k => k.KlasseID == id).SingleOrDefault();
            return _konkurranseKlasseRepo.Delete(konkurranseKlasseDelete);
        }
    }

    
}
