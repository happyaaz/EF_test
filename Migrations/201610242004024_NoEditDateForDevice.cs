namespace LostAndFound.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NoEditDateForDevice : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ClientDeviceInfo", "deviseEditDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ClientDeviceInfo", "deviseEditDate", c => c.String());
        }
    }
}
