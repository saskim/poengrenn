using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace Poengrenn.DAL.Models
{
    public class PoengrennContext : DbContext
    {
        public PoengrennContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PoengrennContext, Migrations.Configuration>("PoengrennContext"));

            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public IDbSet<Konkurranse> Konkurranse { get; set; }
        public IDbSet<KonkurranseDeltaker> KonkurranseDeltaker { get; set; }
        public IDbSet<KonkurranseKlasse> KonkurranseKlasse { get; set; }
        public IDbSet<KonkurranseType> KonkurranseType { get; set; }
        public IDbSet<Person> Person { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new KonkurranseMapper());
            modelBuilder.Configurations.Add(new KonkurranseDeltakerMapper());
            modelBuilder.Configurations.Add(new KonkurranseKlasseMapper());
            modelBuilder.Configurations.Add(new KonkurranseTypeMapper());
            modelBuilder.Configurations.Add(new PersonMapper());
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                List<string> errors = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    errors.Add("Entity of type " + eve.Entry.Entity.GetType().Name + " in state " + eve.Entry.State + " has the following validation errors:");
                    foreach (var ve in eve.ValidationErrors)
                    {
                        errors.Add(" - Property: " + ve.PropertyName + ", Error: " + ve.ErrorMessage);
                    }
                }
                throw new DbEntityValidationException(string.Concat(errors));
            }
        }
    }
}
