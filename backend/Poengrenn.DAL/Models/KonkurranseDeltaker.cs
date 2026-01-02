namespace Poengrenn.DAL.Models
{
    using System;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public partial class KonkurranseDeltaker
    {
        public int KonkurranseID { get; set; }
        public string KlasseID { get; set; }
        public int PersonID { get; set; }
        public string TypeID { get; set; }
        public int? LagNummer { get; set; }
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

    class KonkurranseDeltakerMapper : IEntityTypeConfiguration<KonkurranseDeltaker>
    {
        public void Configure(EntityTypeBuilder<KonkurranseDeltaker> builder)
        {
            builder.ToTable("KonkurranseDeltaker");

            builder.HasKey(k => new { k.KonkurranseID, k.KlasseID, k.PersonID });

            builder.Property(k => k.KonkurranseID).IsRequired();
            builder.Property(k => k.KlasseID).IsRequired().HasMaxLength(50);
            builder.Property(k => k.PersonID).IsRequired();
            builder.Property(k => k.TypeID).HasMaxLength(20);
            builder.Property(k => k.BetalingsNotat).HasMaxLength(255);

            builder.HasOne(k => k.Konkurranse)
                .WithMany(d => d.KonkurranseDeltakere)
                .HasForeignKey(d => d.KonkurranseID);

            builder.HasOne(k => k.KonkurranseKlasse)
                .WithMany(d => d.KonkurranseDeltakere)
                .HasForeignKey(d => new { d.KlasseID, d.TypeID });

            builder.HasOne(k => k.Person)
                .WithMany(d => d.KonkurranseDeltakere)
                .HasForeignKey(d => d.PersonID);

        }
    }
}
