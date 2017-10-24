namespace LostAndFound.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datetimestuff : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ClientDeviceInfo", "deviceRegistrationTime", c => c.String());
            AddColumn("dbo.ClientDeviceInfo", "deviseEditDate", c => c.String());
            AlterColumn("dbo.ClientDeviceInfo", "deviceRegistrationDate", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ClientDeviceInfo", "deviceRegistrationDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.ClientDeviceInfo", "deviseEditDate");
            DropColumn("dbo.ClientDeviceInfo", "deviceRegistrationTime");
        }
    }
}
