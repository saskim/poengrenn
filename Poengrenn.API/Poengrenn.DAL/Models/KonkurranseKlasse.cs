namespace Poengrenn.DAL.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.ModelConfiguration;

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

    class KonkurranseKlasseMapper : EntityTypeConfiguration<KonkurranseKlasse>
    {
        public KonkurranseKlasseMapper()
        {
            ToTable("KonkurranseKlasse");

            HasKey(k => new { k.KlasseID, k.TypeID });

            Property(k => k.KlasseID).IsRequired().HasMaxLength(50);
            Property(k => k.TypeID).IsRequired().HasMaxLength(20);
            Property(k => k.Navn).HasMaxLength(100);
            Property(k => k.MinAlder).IsRequired();
            Property(k => k.MaxAlder).IsRequired();
            Property(k => k.Kjonn).HasMaxLength(10);
            Property(k => k.ForsteStartnummer).IsRequired();
            Property(k => k.SisteStartnummer).IsRequired();
            Property(k => k.MedTidtaking).IsRequired();
            Property(k => k.DistanseKm).HasPrecision(5, 2);

            HasRequired(k => k.KonkurranseType)
                .WithMany(t => t.KonkurranseKlasser)
                .HasForeignKey(k => k.TypeID)
                .WillCascadeOnDelete(false);
        }
    }
}
