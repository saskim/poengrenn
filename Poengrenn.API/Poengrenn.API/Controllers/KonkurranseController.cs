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
    [RoutePrefix("api/konkurranse")]
    public class KonkurranseController : ApiController
    {
        private readonly EFPoengrennRepository<Konkurranse> _konkurranseRepo;
        private readonly EFPoengrennRepository<KonkurranseKlasse> _konkurranseKlasseRepo;
        private readonly EFPoengrennRepository<KonkurranseDeltaker> _konkurranseDeltagerRepo;
        private readonly EFPoengrennRepository<Person> _personRepo;


        public KonkurranseController()
        {
            _konkurranseRepo = new EFPoengrennRepository<Konkurranse>();
            _konkurranseKlasseRepo = new EFPoengrennRepository<KonkurranseKlasse>();
            _konkurranseDeltagerRepo = new EFPoengrennRepository<KonkurranseDeltaker>();
            _personRepo = new EFPoengrennRepository<Person>();
        }

        // GET api/konkurranse
        [Route("")]
        [HttpGet]
        public IEnumerable<Konkurranse> Get()
        {
            return _konkurranseRepo.Get();
        }
        // GET api/konkurranse/open
        [Route("open")]
        [HttpGet]
        public IEnumerable<Konkurranse> GetOpen()
        {
            var dt = DateTime.Now.AddHours(4);
            return _konkurranseRepo.Get(k => k.Dato >= dt);
        }
        // GET api/konkurranse/done
        [Route("done")]
        [HttpGet]
        public IEnumerable<Konkurranse> GetDone()
        {
            var dt = DateTime.Now;
            return _konkurranseRepo.Get(k => k.Dato < dt);
        }

        // GET api/konkurranse/5
        [Route("{id}")]
        [HttpGet]
        public Konkurranse Get(int id)
        {
            return _konkurranseRepo.GetByID(id);
        }

        // POST api/konkurranse
        [Route("")]
        [HttpPost]
        public IEnumerable<Konkurranse> Post(NyKonkurranse nyKonkurranse)
        {
            var konkurranser = new List<Konkurranse>();
            var stdKonkurranseKlasser = _konkurranseKlasseRepo.Get(kk => kk.TypeID == nyKonkurranse.TypeID).ToList();
            var countDatoer = nyKonkurranse.Datoer?.Count;
            var serie = (countDatoer > 1) ? nyKonkurranse.TypeID + nyKonkurranse.Datoer.ElementAt(0).ToShortDateString() : null;
            for (var i = 0; i < countDatoer; i++)
            {
                var dato = nyKonkurranse.Datoer.ElementAt(i);
                var konk = _konkurranseRepo.Insert(new Konkurranse
                {
                    Navn = nyKonkurranse.Navn,
                    TypeID = nyKonkurranse.TypeID,
                    Serie = serie,
                    Dato = dato,
                    Status = KonkurranseStatus.Aktiv.ToString(),
                    
                });
                konkurranser.Add(konk);
            }
            return konkurranser;
        }

        // PUT api/konkurranse/5
        [Route("")]
        [HttpPut]
        public Konkurranse Put(Konkurranse konkurranse)
        {
            var konkurranseUpdate = _konkurranseRepo.GetByID(konkurranse.KonkurranseID);
            konkurranseUpdate.TypeID = konkurranse.TypeID;
            konkurranseUpdate.Serie = konkurranse.Serie;
            konkurranseUpdate.Dato = konkurranse.Dato;
            konkurranseUpdate.Status = KonkurranseStatus.Aktiv.ToString();
            
            return _konkurranseRepo.Update(konkurranseUpdate);
        }

        // DELETE api/konkurranse/5
        [Route("")]
        [HttpDelete]
        public Konkurranse Delete(int id)
        {
            var konkurranseDelete = _konkurranseRepo.GetByID(id);
            konkurranseDelete.Status = KonkurranseStatus.Avlyst.ToString();
            return _konkurranseRepo.Update(konkurranseDelete);
        }



        // GET api/konkurranse/5/deltaker
        [Route("{id}/deltaker")]
        [HttpGet]
        public IEnumerable<KonkurranseDeltaker> GetDeltakere(int id)
        {
            return _konkurranseDeltagerRepo.Get(d => d.KonkurranseID == id, null, _konkurranseDeltagerRepo.FindPropertyName(typeof(Person)));
        }
        // POST api/konkurranse/5/deltaker
        [Route("{id}/deltaker")]
        [HttpPost]
        public KonkurranseDeltaker PostDeltaker(int id, NyKonkurranseDeltaker deltaker)
        {
            var konkurranse = _konkurranseRepo.Get(d => d.KonkurranseID == id).FirstOrDefault();
            if (konkurranse ==  null)
                return null;
            
            return _konkurranseDeltagerRepo.Insert(new KonkurranseDeltaker
            {
                KonkurranseID = id,
                KlasseID = deltaker.KlasseID,
                TypeID = deltaker.TypeID,
                PersonID = deltaker.PersonID,
                StartNummer = FinnNesteLedigeStartnummer(konkurranse, deltaker.KlasseID)
            });
        }

        // PUT api/konkurranse/5/deltaker/2
        [Route("{id}/deltaker")]
        [HttpPut]
        public KonkurranseDeltaker PutDeltaker(int id, KonkurranseDeltaker deltaker)
        {
            var konkurranse = _konkurranseRepo.Get(d => d.KonkurranseID == id).FirstOrDefault();
            if (konkurranse == null)
                return null;

            var deltakerUpdate = _konkurranseDeltagerRepo.Get(d => d.KonkurranseID == id && d.PersonID == deltaker.PersonID).FirstOrDefault();
            if (deltakerUpdate == null)
                return null;

            if (deltakerUpdate.KlasseID == deltaker.KlasseID)
            {
                deltakerUpdate.StartNummer = deltaker.StartNummer;
                deltakerUpdate.StartTid = deltaker.StartTid;
                deltakerUpdate.SluttTid = deltaker.SluttTid;
                deltakerUpdate.Tidsforbruk = deltaker.SluttTid - deltaker.StartTid;
                deltakerUpdate.KlasseID = deltaker.KlasseID;
                deltakerUpdate.Tilstede = deltaker.Tilstede;
                deltakerUpdate.Betalt = deltaker.Betalt;
                deltakerUpdate.BetalingsNotat = deltaker.BetalingsNotat;

                return _konkurranseDeltagerRepo.Update(deltakerUpdate);
            }
            else
            {
                _konkurranseDeltagerRepo.Delete(deltakerUpdate);

                deltaker.StartNummer = FinnNesteLedigeStartnummer(konkurranse, deltaker.KlasseID);
                deltaker.Person = null;
                return _konkurranseDeltagerRepo.Insert(deltaker);
            }
        }

        [Route("{id}/deltaker/{deltakerId}/klasse/{klasseId}")]
        [HttpDelete]
        public bool DeleteDeltaker(int id, int deltakerId, string klasseId)
        {
            var deltaker = _konkurranseDeltagerRepo.Get(d => d.KonkurranseID == id && d.PersonID == deltakerId && d.KlasseID == klasseId).FirstOrDefault();
            return _konkurranseDeltagerRepo.Delete(deltaker);
        }


        private int FinnNesteLedigeStartnummer(Konkurranse konkurranse, string klasseID)
        {
            var konkurranseKlasse = _konkurranseKlasseRepo.GetByID(klasseID, konkurranse.TypeID);
            if (konkurranseKlasse == null)
                konkurranseKlasse = _konkurranseKlasseRepo.GetByID(klasseID, "0");

            var startnummer = _konkurranseDeltagerRepo.Get(kd => kd.KonkurranseID == konkurranse.KonkurranseID && kd.KlasseID == klasseID).OrderByDescending(x => x.StartNummer).Select(x => x.StartNummer).FirstOrDefault();
            if (startnummer == null)
                startnummer = konkurranseKlasse.ForsteStartnummer - 1;
            return (int)startnummer + 1;
        }

        private KonkurranseKlasse FinnKonkurranseKlasse(string typeID, Person person)
        {
            var personAlder = DateTime.Today.Year - person.Fodselsar;
            var klasser = _konkurranseKlasseRepo.Get(k => (k.Kjonn == person.Kjonn || k.Kjonn == "Mix") &&
                                            k.MinAlder <= personAlder &&
                                            k.MaxAlder >= personAlder);
            if (klasser == null)
                return null;

            var klasse = klasser.Where(k => k.TypeID == typeID).FirstOrDefault();
            if (klasse == null)
                klasse = klasser.FirstOrDefault();

            return klasse;
        }
    }

    
}
