namespace Poengrenn.DAL.Models
{
    using System;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public partial class Konkurranse
    {
        public Konkurranse()
        {
            KonkurranseDeltakere = new HashSet<KonkurranseDeltaker>();
        }
    
        public int KonkurranseID { get; set; }
        public string Serie { get; set; }
        public string Navn { get; set; }
        public Nullable<DateTime> Dato { get; set; }
        public string TypeID { get; set; }
        public string Status { get; set; }
        public int StartInterval { get; set; }
    
        public virtual KonkurranseType KonkurranseType { get; set; }
        public virtual ICollection<KonkurranseDeltaker> KonkurranseDeltakere { get; set; }
    }

    class KonkurranseMapper : IEntityTypeConfiguration<Konkurranse>
    {
        public void Configure(EntityTypeBuilder<Konkurranse> builder)
        {
            builder.ToTable("Konkurranse");

            builder.HasKey(k => k.KonkurranseID);

            builder.Property(k => k.Serie).HasMaxLength(50);
            builder.Property(k => k.Navn).HasMaxLength(150);
            builder.Property(k => k.TypeID).IsRequired().HasMaxLength(20);
            builder.Property(k => k.Status).IsRequired().HasMaxLength(20);

            builder.HasOne(k => k.KonkurranseType)
                .WithMany(t => t.Konkurranser)
                .HasForeignKey(k => k.TypeID);
        }
    }
}
