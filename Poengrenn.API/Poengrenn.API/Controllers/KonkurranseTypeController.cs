using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Poengrenn.DAL.Models;
using Poengrenn.DAL.EFRepository;
using Poengrenn.API.Models;

namespace Poengrenn.API.Controllers
{
    [RoutePrefix("api/konkurransetype")]
    public class KonkurranseTypeController : ApiController
    {
        private readonly EFPoengrennRepository<KonkurranseType> _konkurranseTypeRepo;
        
        public KonkurranseTypeController()
        {
            _konkurranseTypeRepo = new EFPoengrennRepository<KonkurranseType>();
        }

        // GET api/konkurransetype
        [Route("")]
        [HttpGet]
        public IEnumerable<KonkurranseType> Get()
        {
            return _konkurranseTypeRepo.Get(t => t.Aktiv == true);
        }

        // GET api/konkurransetype/5
        [Route("{id}")]
        [HttpGet]
        public KonkurranseType Get(int id)
        {
            return _konkurranseTypeRepo.GetByID(id);
        }

        // POST api/konkurransetype
        [Route("")]
        [HttpPost]
        public KonkurranseType Post(KonkurranseType nyKonkurranseType)
        {
            return _konkurranseTypeRepo.Insert(nyKonkurranseType);
        }

        // PUT api/konkurransetype/5
        [Route("")]
        [HttpPut]
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
