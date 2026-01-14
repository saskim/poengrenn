namespace Poengrenn.DAL.Models
{
    using System;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public partial class KonkurranseKlasse
    {
        public KonkurranseKlasse()
        {
            KonkurranseDeltakere = new HashSet<KonkurranseDeltaker>();
        }
    
        public string KlasseID { get; set; }
        public string TypeID { get; set; }
        public string Navn { get; set; }
        public int MinAlder { get; set; }
        public int MaxAlder { get; set; }
        public string Kjonn { get; set; }
        public int ForsteStartnummer { get; set; }
        public int SisteStartnummer { get; set; }
        public bool MedTidtaking { get; set; }
        public Nullable<decimal> DistanseKm { get; set; }

        public virtual ICollection<KonkurranseDeltaker> KonkurranseDeltakere { get; set; }
        public virtual KonkurranseType KonkurranseType { get; set; }
    }

    class KonkurranseKlasseMapper : IEntityTypeConfiguration<KonkurranseKlasse>
    {
        public void Configure(EntityTypeBuilder<KonkurranseKlasse> builder)
        {
            builder.ToTable("KonkurranseKlasse");

            builder.HasKey(k => new { k.KlasseID, k.TypeID });

            builder.Property(k => k.KlasseID).IsRequired().HasMaxLength(50);
            builder.Property(k => k.TypeID).IsRequired().HasMaxLength(20);
            builder.Property(k => k.Navn).HasMaxLength(100);
            builder.Property(k => k.MinAlder).IsRequired();
            builder.Property(k => k.MaxAlder).IsRequired();
            builder.Property(k => k.Kjonn).HasMaxLength(10);
            builder.Property(k => k.ForsteStartnummer).IsRequired();
            builder.Property(k => k.SisteStartnummer).IsRequired();
            builder.Property(k => k.MedTidtaking).IsRequired();
            builder.Property(k => k.DistanseKm).HasPrecision(5, 2);

            builder.HasOne(k => k.KonkurranseType)
                .WithMany(t => t.KonkurranseKlasser)
                .HasForeignKey(k => k.TypeID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
