namespace LostAndFound.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updates1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ClientBasicInfo", "clientContactPerson", c => c.String());
            AddColumn("dbo.ClientBasicInfo", "clientContactNumber", c => c.String());
            AddColumn("dbo.ClientBasicInfo", "clientContactEmail", c => c.String());
            AddColumn("dbo.ClientBasicInfo", "clientCommisionInPercent", c => c.String());
            AddColumn("dbo.ClientBasicInfo", "clientPerItemBaseFee", c => c.String());
            AddColumn("dbo.ClientBasicInfo", "clientSalesFee", c => c.String());
            AddColumn("dbo.ClientBasicInfo", "clientShippingFee", c => c.String());
            AddColumn("dbo.ClientDeviceInfo", "deviceOtherComments", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ClientDeviceInfo", "deviceOtherComments");
            DropColumn("dbo.ClientBasicInfo", "clientShippingFee");
            DropColumn("dbo.ClientBasicInfo", "clientSalesFee");
            DropColumn("dbo.ClientBasicInfo", "clientPerItemBaseFee");
            DropColumn("dbo.ClientBasicInfo", "clientCommisionInPercent");
            DropColumn("dbo.ClientBasicInfo", "clientContactEmail");
            DropColumn("dbo.ClientBasicInfo", "clientContactNumber");
            DropColumn("dbo.ClientBasicInfo", "clientContactPerson");
        }
    }
}
