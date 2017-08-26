namespace Poengrenn.DAL.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.ModelConfiguration;

    public partial class KonkurranseDeltaker
    {
        public int KonkurranseID { get; set; }
        public string KlasseID { get; set; }
        public int PersonID { get; set; }
        public string TypeID { get; set; }
        public Nullable<int> StartNummer { get; set; }
        public Nullable<TimeSpan> StartTid { get; set; }
        public Nullable<TimeSpan> SluttTid { get; set; }
        public Nullable<TimeSpan> Tidsforbruk { get; set; }
        public Nullable<bool> Betalt { get; set; }
        public Nullable<bool> Tilstede { get; set; }
        public string BetalingsNotat { get; set; }
    
        public virtual Konkurranse Konkurranse { get; set; }
        public virtual KonkurranseKlasse KonkurranseKlasse { get; set; }
        public virtual Person Person { get; set; }
    }

    class KonkurranseDeltakerMapper : EntityTypeConfiguration<KonkurranseDeltaker>
    {
        public KonkurranseDeltakerMapper()
        {
            ToTable("KonkurranseDeltaker");

            HasKey(k => new { k.KonkurranseID, k.KlasseID, k.PersonID });

            Property(k => k.KonkurranseID).IsRequired();
            Property(k => k.KlasseID).IsRequired().HasMaxLength(50);
            Property(k => k.PersonID).IsRequired();
            Property(k => k.TypeID).HasMaxLength(20);
            Property(k => k.BetalingsNotat).HasMaxLength(255);

            HasRequired(k => k.Konkurranse)
                .WithMany(d => d.KonkurranseDeltakere)
                .HasForeignKey(d => d.KonkurranseID);

            HasRequired(k => k.KonkurranseKlasse)
                .WithMany(d => d.KonkurranseDeltakere)
                .HasForeignKey(d => new { d.KlasseID, d.TypeID });

            HasRequired(k => k.Person)
                .WithMany(d => d.KonkurranseDeltakere)
                .HasForeignKey(d => d.PersonID);

        }
    }
}
