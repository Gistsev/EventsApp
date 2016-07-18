namespace TestRegister.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddToUserInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "PhoneNumber", c => c.String());
            AddColumn("dbo.User", "Name", c => c.String());
            AddColumn("dbo.User", "Surname", c => c.String());
            AddColumn("dbo.User", "LastName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "LastName");
            DropColumn("dbo.User", "Surname");
            DropColumn("dbo.User", "Name");
            DropColumn("dbo.User", "PhoneNumber");
        }
    }
}
