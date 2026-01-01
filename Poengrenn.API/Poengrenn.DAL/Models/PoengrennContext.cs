using System;
using Microsoft.EntityFrameworkCore;

namespace Poengrenn.DAL.Models
{
    public class PoengrennContext : DbContext
    {
        public PoengrennContext(DbContextOptions<PoengrennContext> options) : base(options)
        {
        }

        public DbSet<Konkurranse> Konkurranse { get; set; }
        public DbSet<KonkurranseDeltaker> KonkurranseDeltaker { get; set; }
        public DbSet<KonkurranseKlasse> KonkurranseKlasse { get; set; }
        public DbSet<KonkurranseType> KonkurranseType { get; set; }
        public DbSet<Person> Person { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new KonkurranseMapper());
            modelBuilder.ApplyConfiguration(new KonkurranseDeltakerMapper());
            modelBuilder.ApplyConfiguration(new KonkurranseKlasseMapper());
            modelBuilder.ApplyConfiguration(new KonkurranseTypeMapper());
            modelBuilder.ApplyConfiguration(new PersonMapper());
        }
    }
}
