namespace Poengrenn.DAL.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.ModelConfiguration;

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

    class PersonMapper : EntityTypeConfiguration<Person>
    {
        public PersonMapper()
        {
            ToTable("Person");

            HasKey(k => k.PersonID);

            Property(k => k.PersonID).IsRequired();
            Property(k => k.Fornavn).IsRequired().HasMaxLength(100);
            Property(k => k.Etternavn).IsRequired().HasMaxLength(100);
            Property(k => k.Kjonn).HasMaxLength(10);
            Property(k => k.Epost).HasMaxLength(255);
            Property(k => k.Telefon).HasMaxLength(10);
        }
    }
}
