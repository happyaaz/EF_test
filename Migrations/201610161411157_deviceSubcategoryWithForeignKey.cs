namespace LostAndFound.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deviceSubcategoryWithForeignKey : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeviceSubcategory", "deviceCategoryId_deviceCategoryId", c => c.Int());
            CreateIndex("dbo.DeviceSubcategory", "deviceCategoryId_deviceCategoryId");
            AddForeignKey("dbo.DeviceSubcategory", "deviceCategoryId_deviceCategoryId", "dbo.DeviceCategory", "deviceCategoryId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DeviceSubcategory", "deviceCategoryId_deviceCategoryId", "dbo.DeviceCategory");
            DropIndex("dbo.DeviceSubcategory", new[] { "deviceCategoryId_deviceCategoryId" });
            DropColumn("dbo.DeviceSubcategory", "deviceCategoryId_deviceCategoryId");
        }
    }
}
