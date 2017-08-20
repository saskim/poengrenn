namespace Poengrenn.DAL.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.ModelConfiguration;

    public partial class KonkurranseType
    {
        public KonkurranseType()
        {
            this.Konkurranser = new HashSet<Konkurranse>();
            this.KonkurranseKlasser = new HashSet<KonkurranseKlasse>();
        }
    
        public string TypeID { get; set; }
        public string Navn { get; set; }
        public int StandardAntallKonkurranser { get; set; }
        public bool Aktiv { get; set; }
    
        public virtual ICollection<Konkurranse> Konkurranser { get; set; }
        public virtual ICollection<KonkurranseKlasse> KonkurranseKlasser { get; set; }
    }

    class KonkurranseTypeMapper : EntityTypeConfiguration<KonkurranseType>
    {
        public KonkurranseTypeMapper()
        {
            ToTable("KonkurranseType");

            HasKey(k => k.TypeID);

            Property(k => k.TypeID).IsRequired().HasMaxLength(20);
            Property(k => k.Navn).IsRequired().HasMaxLength(50);
            Property(k => k.StandardAntallKonkurranser).IsRequired();
            Property(k => k.Aktiv).IsRequired();
        }
    }
}
