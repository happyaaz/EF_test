namespace LostAndFound.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class idunnowhathappened : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ClientDeviceInfo", "deviceModel");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ClientDeviceInfo", "deviceModel", c => c.String());
        }
    }
}
