namespace InvaAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedRequiredFromRequests : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Requests", "EmployeeId", c => c.String());
            AlterColumn("dbo.Requests", "ProductId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Requests", "ProductId", c => c.String(nullable: false));
            AlterColumn("dbo.Requests", "EmployeeId", c => c.String(nullable: false));
        }
    }
}
