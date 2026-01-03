using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Poengrenn.DAL.Models;
using Poengrenn.DAL.EFRepository;
using Poengrenn.API.Models;
using Poengrenn.API.Models.Enums;

namespace Poengrenn.API.Controllers
{
    [ApiController]
    [Route("api/konkurranse")]
    public class KonkurranseController : ControllerBase
    {
        private readonly EFPoengrennRepository<Konkurranse> _konkurranseRepo;
        private readonly EFPoengrennRepository<KonkurranseKlasse> _konkurranseKlasseRepo;
        private readonly EFPoengrennRepository<KonkurranseDeltaker> _konkurranseDeltagerRepo;

        public KonkurranseController(
            EFPoengrennRepository<Konkurranse> konkurranseRepo,
            EFPoengrennRepository<KonkurranseKlasse> konkurranseKlasseRepo,
            EFPoengrennRepository<KonkurranseDeltaker> konkurranseDeltagerRepo)
        {
            _konkurranseRepo = konkurranseRepo;
            _konkurranseKlasseRepo = konkurranseKlasseRepo;
            _konkurranseDeltagerRepo = konkurranseDeltagerRepo;
        }

        // GET api/konkurranse
        [HttpGet("")]
        public IEnumerable<Konkurranse> Get()
        {
            return _konkurranseRepo.Get();
        }
        // GET api/konkurranse/open
        [HttpGet("open")]
        public IEnumerable<Konkurranse> GetOpen()
        {
            var dt = DateTime.UtcNow;
            return _konkurranseRepo.Get(k => k.Dato >= dt);
        }
        // GET api/konkurranse/done
        [HttpGet("done")]
        public IEnumerable<Konkurranse> GetDone()
        {
            var dt = DateTime.UtcNow;
            return _konkurranseRepo.Get(k => k.Dato < dt).OrderByDescending(k => k.Dato);
        }

        // GET api/konkurranse/5
        [HttpGet("{id}")]
        public Konkurranse Get(int id)
        {
            return _konkurranseRepo.GetByID(id);
        }

        // POST api/konkurranse
        [HttpPost("")]
        public IEnumerable<Konkurranse> Post(NyKonkurranse nyKonkurranse)
        {
            var konkurranser = new List<Konkurranse>();
            var countDatoer = nyKonkurranse.Datoer?.Count;
            var serie = (countDatoer > 1) ? nyKonkurranse.TypeID + nyKonkurranse.Datoer.ElementAt(0).ToShortDateString() : null;
            for (var i = 0; i < countDatoer; i++)
            {
                var dato = NormalizeToUtc(nyKonkurranse.Datoer.ElementAt(i));
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
        [HttpPut("")]
        public Konkurranse Put(Konkurranse konkurranse)
        {
            var konkurranseUpdate = _konkurranseRepo.GetByID(konkurranse.KonkurranseID);
            konkurranseUpdate.TypeID = konkurranse.TypeID;
            konkurranseUpdate.Serie = konkurranse.Serie;
            konkurranseUpdate.Dato = NormalizeToUtc(konkurranse.Dato);
            konkurranseUpdate.Navn = konkurranse.Navn;
            konkurranseUpdate.Status = konkurranse.Status.ToString();
            konkurranseUpdate.StartInterval = konkurranse.StartInterval;
            
            return _konkurranseRepo.Update(konkurranseUpdate);
        }


        // DELETE api/konkurranse/5
        [HttpDelete("")]
        public Konkurranse Delete(int id)
        {
            var konkurranseDelete = _konkurranseRepo.GetByID(id);
            konkurranseDelete.Status = KonkurranseStatus.Avlyst.ToString();
            return _konkurranseRepo.Update(konkurranseDelete);
        }



        // GET api/konkurranse/5/deltaker
        [HttpGet("{id}/deltaker")]
        public IEnumerable<KonkurranseDeltaker> GetDeltakere(int id)
        {
            return _konkurranseDeltagerRepo.Get(d => d.KonkurranseID == id, null, _konkurranseDeltagerRepo.FindPropertyName(typeof(Person)));
        }
        // POST api/konkurranse/5/deltaker
        [HttpPost("{id}/deltaker")]
        public IEnumerable<KonkurranseDeltaker> PostDeltaker(int id, NyKonkurranseDeltaker deltaker)
        {
            var konkurranse = _konkurranseRepo.Get(d => d.KonkurranseID == id).FirstOrDefault();
            if (konkurranse == null)
                return null;

            var insertedList = new List<KonkurranseDeltaker>();

            // Insert for "poengrenn"-serie
            if (konkurranse.TypeID == "poengrenn" && konkurranse.Serie != null)
            {
                var konkurranseSeriePoengrenn = _konkurranseRepo.Get(k => k.Serie == konkurranse.Serie && k.TypeID == konkurranse.TypeID).ToList();
                if (konkurranseSeriePoengrenn != null)
                {
                    foreach (var konkurranseSerie in konkurranseSeriePoengrenn)
                    {
                        var alreadyRegistered = _konkurranseDeltagerRepo.Any(d => d.PersonID == deltaker.PersonID && d.KonkurranseID == konkurranseSerie.KonkurranseID);
                        if (alreadyRegistered)
                            continue;

                        var inserted = _konkurranseDeltagerRepo.Insert(new KonkurranseDeltaker
                        {
                            KonkurranseID = konkurranseSerie.KonkurranseID,
                            KlasseID = deltaker.KlasseID,
                            TypeID = deltaker.TypeID,
                            PersonID = deltaker.PersonID,
                            StartNummer = FinnNesteLedigeStartnummer(konkurranseSerie, deltaker.KlasseID)
                        });
                        insertedList.Add(inserted);
                    }
                }
            }
            else
            {

                var inserted = _konkurranseDeltagerRepo.Insert(new KonkurranseDeltaker
                {
                    KonkurranseID = id,
                    KlasseID = deltaker.KlasseID,
                    TypeID = deltaker.TypeID,
                    PersonID = deltaker.PersonID,
                    StartNummer = FinnNesteLedigeStartnummer(konkurranse, deltaker.KlasseID)
                });
                insertedList.Add(inserted);
            }
            return insertedList;
        }

        // PUT api/konkurranse/5/deltaker/2
        [HttpPut("{id}/deltaker")]
        public KonkurranseDeltaker PutDeltaker(int id, KonkurranseDeltaker deltaker)
        {
            var konkurranse = _konkurranseRepo.Get(d => d.KonkurranseID == id).FirstOrDefault();
            if (konkurranse == null)
                return null;

            var deltakerUpdate = _konkurranseDeltagerRepo.Get(d => d.KonkurranseID == id && d.PersonID == deltaker.PersonID).FirstOrDefault();
            if (deltakerUpdate == null)
                return null;

            // Update payment for "poengrenn"-serie
            if (konkurranse.TypeID == "poengrenn" && konkurranse.Serie != null && deltaker.Betalt != deltakerUpdate.Betalt)
            {
                var konkurranseDeltakerSerie = _konkurranseDeltagerRepo.Get(d => d.Konkurranse.Serie == konkurranse.Serie && d.PersonID == deltaker.PersonID).ToList();
                if (konkurranseDeltakerSerie != null)
                {
                    foreach (var deltakerSerie in konkurranseDeltakerSerie)
                    {
                        deltakerSerie.Betalt = deltaker.Betalt;
                        _konkurranseDeltagerRepo.Update(deltakerSerie);
                    }
                }
            }

            if (deltakerUpdate.KlasseID == deltaker.KlasseID)
            {
                deltakerUpdate.LagNummer = deltaker.LagNummer;
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

        private static DateTime? NormalizeToUtc(DateTime? value)
        {
            if (!value.HasValue)
                return null;

            var dt = value.Value;
            if (dt.Kind == DateTimeKind.Utc)
                return dt;

            if (dt.Kind == DateTimeKind.Unspecified)
                dt = DateTime.SpecifyKind(dt, DateTimeKind.Local);

            return dt.ToUniversalTime();
        }
    }

    
}
