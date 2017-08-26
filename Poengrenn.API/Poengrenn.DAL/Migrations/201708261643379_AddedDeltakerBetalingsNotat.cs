namespace Poengrenn.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDeltakerBetalingsNotat : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.KonkurranseDeltaker", "BetalingsNotat", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            DropColumn("dbo.KonkurranseDeltaker", "BetalingsNotat");
        }
    }
}
