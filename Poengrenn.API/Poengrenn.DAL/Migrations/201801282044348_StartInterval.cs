namespace Poengrenn.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StartInterval : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Konkurranse", "StartInterval", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Konkurranse", "StartInterval");
        }
    }
}
