namespace InvaAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedIsActive : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "isActive");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "isActive", c => c.Boolean(nullable: false));
        }
    }
}
