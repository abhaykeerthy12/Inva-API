namespace InvaAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modifications : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "Type", c => c.String(nullable: false));
            AlterColumn("dbo.Requests", "EmployeeId", c => c.String(nullable: false));
            AlterColumn("dbo.Requests", "ProductId", c => c.String(nullable: false));
            AlterColumn("dbo.Requests", "Status", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Requests", "Status", c => c.String());
            AlterColumn("dbo.Requests", "ProductId", c => c.String());
            AlterColumn("dbo.Requests", "EmployeeId", c => c.String());
            AlterColumn("dbo.Products", "Type", c => c.String());
            AlterColumn("dbo.Products", "Name", c => c.String());
        }
    }
}
