namespace LostAndFound.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deviceSubcategoryColumn1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.DeviceSubcategory", name: "deviceCategoryId_deviceCategoryId", newName: "deviceCategory_deviceCategoryId");
            RenameIndex(table: "dbo.DeviceSubcategory", name: "IX_deviceCategoryId_deviceCategoryId", newName: "IX_deviceCategory_deviceCategoryId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.DeviceSubcategory", name: "IX_deviceCategory_deviceCategoryId", newName: "IX_deviceCategoryId_deviceCategoryId");
            RenameColumn(table: "dbo.DeviceSubcategory", name: "deviceCategory_deviceCategoryId", newName: "deviceCategoryId_deviceCategoryId");
        }
    }
}
