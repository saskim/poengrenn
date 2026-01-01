namespace Poengrenn.DAL.Models
{
    using System;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public partial class Person
    {
        public Person()
        {
            KonkurranseDeltakere = new HashSet<KonkurranseDeltaker>();
        }
    
        public int PersonID { get; set; }
        public string Fornavn { get; set; }
        public string Etternavn { get; set; }
        public Nullable<int> Fodselsar { get; set; }
        public string Kjonn { get; set; }
        public string Epost { get; set; }
        public string Telefon { get; set; }
    
        public virtual ICollection<KonkurranseDeltaker> KonkurranseDeltakere { get; set; }
    }

    class PersonMapper : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Person");

            builder.HasKey(k => k.PersonID);

            builder.Property(k => k.PersonID).IsRequired();
            builder.Property(k => k.Fornavn).IsRequired().HasMaxLength(100);
            builder.Property(k => k.Etternavn).IsRequired().HasMaxLength(100);
            builder.Property(k => k.Kjonn).HasMaxLength(10);
            builder.Property(k => k.Epost).HasMaxLength(255);
            builder.Property(k => k.Telefon).HasMaxLength(10);
        }
    }
}
