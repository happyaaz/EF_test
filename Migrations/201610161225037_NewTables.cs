namespace LostAndFound.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClientDeviceInfo",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        client = c.String(),
                        deviceCategory = c.String(),
                        deviceSubcategory = c.String(),
                        deviceName = c.String(),
                        deviceNUINTASerialNumber = c.String(),
                        deviceModel = c.String(),
                        deviceCarrier = c.String(),
                        deviceCondition = c.String(),
                        deviceBin = c.String(),
                        deviceUsedBy = c.String(),
                        deviceValue = c.String(),
                        deviceSoldFor = c.String(),
                        deviceRepairCost = c.String(),
                        deviceSalesFees = c.String(),
                        deviceCommisionToAgent = c.String(),
                        deviceNUINTAProfit = c.String(),
                        deviceAddedByUser = c.Int(nullable: false),
                        deviceRegistrationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.DeviceCategory",
                c => new
                    {
                        deviceCategoryId = c.Int(nullable: false, identity: true),
                        deviceCategoryName = c.String(),
                    })
                .PrimaryKey(t => t.deviceCategoryId);
            
            CreateTable(
                "dbo.DeviceCondition",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        deviceConditionName = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.DeviceSubcategory",
                c => new
                    {
                        deviceSubcategoryId = c.Int(nullable: false, identity: true),
                        deviceSubcategoryName = c.String(),
                    })
                .PrimaryKey(t => t.deviceSubcategoryId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DeviceSubcategory");
            DropTable("dbo.DeviceCondition");
            DropTable("dbo.DeviceCategory");
            DropTable("dbo.ClientDeviceInfo");
        }
    }
}
