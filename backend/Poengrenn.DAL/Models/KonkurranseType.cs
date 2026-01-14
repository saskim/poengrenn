namespace Poengrenn.DAL.Models
{
    using System;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

    class KonkurranseTypeMapper : IEntityTypeConfiguration<KonkurranseType>
    {
        public void Configure(EntityTypeBuilder<KonkurranseType> builder)
        {
            builder.ToTable("KonkurranseType");

            builder.HasKey(k => k.TypeID);

            builder.Property(k => k.TypeID).IsRequired().HasMaxLength(20);
            builder.Property(k => k.Navn).IsRequired().HasMaxLength(50);
            builder.Property(k => k.StandardAntallKonkurranser).IsRequired();
            builder.Property(k => k.Aktiv).IsRequired();

            builder.HasData(
                new KonkurranseType
                {
                    TypeID = "1",
                    Navn = "Telenorkarusellen",
                    StandardAntallKonkurranser = 4,
                    Aktiv = true
                },
                new KonkurranseType
                {
                    TypeID = "2",
                    Navn = "Klubbmesterskap",
                    StandardAntallKonkurranser = 1,
                    Aktiv = true
                });
        }
    }
}
