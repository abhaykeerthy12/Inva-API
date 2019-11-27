namespace InvaAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedIsActiveBackv3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "IsActive", c => c.Boolean(nullable: false, defaultValue: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "IsActive");
        }
    }
}
