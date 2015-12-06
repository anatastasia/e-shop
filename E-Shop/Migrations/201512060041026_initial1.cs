namespace E_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Client", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Client");
        }
    }
}
