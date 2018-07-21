namespace Poengrenn.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLagNummer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.KonkurranseDeltaker", "LagNummer", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.KonkurranseDeltaker", "LagNummer");
        }
    }
}
