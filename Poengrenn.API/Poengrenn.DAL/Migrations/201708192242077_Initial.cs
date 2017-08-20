namespace Poengrenn.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Konkurranse",
                c => new
                    {
                        KonkurranseID = c.Int(nullable: false, identity: true),
                        Serie = c.String(maxLength: 50),
                        Navn = c.String(maxLength: 150),
                        Dato = c.DateTime(),
                        TypeID = c.String(nullable: false, maxLength: 20),
                        Status = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.KonkurranseID)
                .ForeignKey("dbo.KonkurranseType", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.TypeID);
            
            CreateTable(
                "dbo.KonkurranseDeltaker",
                c => new
                    {
                        KonkurranseID = c.Int(nullable: false),
                        KlasseID = c.String(nullable: false, maxLength: 50),
                        PersonID = c.Int(nullable: false),
                        TypeID = c.String(nullable: false, maxLength: 20),
                        StartNummer = c.Int(),
                        StartTid = c.Time(precision: 7),
                        SluttTid = c.Time(precision: 7),
                        Tidsforbruk = c.Time(precision: 7),
                        Betalt = c.Boolean(),
                        Tilstede = c.Boolean(),
                    })
                .PrimaryKey(t => new { t.KonkurranseID, t.KlasseID, t.PersonID })
                .ForeignKey("dbo.Konkurranse", t => t.KonkurranseID, cascadeDelete: true)
                .ForeignKey("dbo.KonkurranseKlasse", t => new { t.KlasseID, t.TypeID }, cascadeDelete: true)
                .ForeignKey("dbo.Person", t => t.PersonID, cascadeDelete: true)
                .Index(t => t.KonkurranseID)
                .Index(t => new { t.KlasseID, t.TypeID })
                .Index(t => t.PersonID);
            
            CreateTable(
                "dbo.KonkurranseKlasse",
                c => new
                    {
                        KlasseID = c.String(nullable: false, maxLength: 50),
                        TypeID = c.String(nullable: false, maxLength: 20),
                        Navn = c.String(maxLength: 100),
                        MinAlder = c.Int(nullable: false),
                        MaxAlder = c.Int(nullable: false),
                        Kjonn = c.String(maxLength: 10),
                        ForsteStartnummer = c.Int(nullable: false),
                        SisteStartnummer = c.Int(nullable: false),
                        MedTidtaking = c.Boolean(nullable: false),
                        DistanseKm = c.Decimal(precision: 5, scale: 2),
                    })
                .PrimaryKey(t => new { t.KlasseID, t.TypeID })
                .ForeignKey("dbo.KonkurranseType", t => t.TypeID)
                .Index(t => t.TypeID);
            
            CreateTable(
                "dbo.KonkurranseType",
                c => new
                    {
                        TypeID = c.String(nullable: false, maxLength: 20),
                        Navn = c.String(nullable: false, maxLength: 50),
                        StandardAntallKonkurranser = c.Int(nullable: false),
                        Aktiv = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.TypeID);
            
            CreateTable(
                "dbo.Person",
                c => new
                    {
                        PersonID = c.Int(nullable: false, identity: true),
                        Fornavn = c.String(nullable: false, maxLength: 100),
                        Etternavn = c.String(nullable: false, maxLength: 100),
                        Fodselsar = c.Int(),
                        Kjonn = c.String(maxLength: 10),
                        Epost = c.String(maxLength: 255),
                        Telefon = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.PersonID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Konkurranse", "TypeID", "dbo.KonkurranseType");
            DropForeignKey("dbo.KonkurranseDeltaker", "PersonID", "dbo.Person");
            DropForeignKey("dbo.KonkurranseDeltaker", new[] { "KlasseID", "TypeID" }, "dbo.KonkurranseKlasse");
            DropForeignKey("dbo.KonkurranseKlasse", "TypeID", "dbo.KonkurranseType");
            DropForeignKey("dbo.KonkurranseDeltaker", "KonkurranseID", "dbo.Konkurranse");
            DropIndex("dbo.KonkurranseKlasse", new[] { "TypeID" });
            DropIndex("dbo.KonkurranseDeltaker", new[] { "PersonID" });
            DropIndex("dbo.KonkurranseDeltaker", new[] { "KlasseID", "TypeID" });
            DropIndex("dbo.KonkurranseDeltaker", new[] { "KonkurranseID" });
            DropIndex("dbo.Konkurranse", new[] { "TypeID" });
            DropTable("dbo.Person");
            DropTable("dbo.KonkurranseType");
            DropTable("dbo.KonkurranseKlasse");
            DropTable("dbo.KonkurranseDeltaker");
            DropTable("dbo.Konkurranse");
        }
    }
}
