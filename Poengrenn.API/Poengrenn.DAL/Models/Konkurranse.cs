namespace Poengrenn.DAL.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.ModelConfiguration;

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
    
        public virtual KonkurranseType KonkurranseType { get; set; }
        public virtual ICollection<KonkurranseDeltaker> KonkurranseDeltakere { get; set; }
    }

    class KonkurranseMapper : EntityTypeConfiguration<Konkurranse>
    {
        public KonkurranseMapper()
        {
            ToTable("Konkurranse");

            HasKey(k => k.KonkurranseID);

            Property(k => k.Serie).HasMaxLength(50);
            Property(k => k.Navn).HasMaxLength(150);
            Property(k => k.TypeID).IsRequired().HasMaxLength(20);
            Property(k => k.Status).IsRequired().HasMaxLength(20);

            HasRequired(k => k.KonkurranseType)
                .WithMany(t => t.Konkurranser)
                .HasForeignKey(k => k.TypeID);
        }
    }
}
