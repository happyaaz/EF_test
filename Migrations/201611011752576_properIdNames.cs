namespace LostAndFound.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class properIdNames : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.DeviceBin");
            DropPrimaryKey("dbo.DeviceCondition");
            DropColumn("dbo.DeviceBin", "id");
            DropColumn("dbo.DeviceCondition", "id");
            AddColumn("dbo.DeviceBin", "deviceBinId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.DeviceCondition", "deviceConditionId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.DeviceBin", "deviceBinId");
            AddPrimaryKey("dbo.DeviceCondition", "deviceConditionId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DeviceCondition", "id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.DeviceBin", "id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.DeviceCondition");
            DropPrimaryKey("dbo.DeviceBin");
            DropColumn("dbo.DeviceCondition", "deviceConditionId");
            DropColumn("dbo.DeviceBin", "deviceBinId");
            AddPrimaryKey("dbo.DeviceCondition", "id");
            AddPrimaryKey("dbo.DeviceBin", "id");
        }
    }
}
