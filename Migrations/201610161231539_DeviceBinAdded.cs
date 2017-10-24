namespace LostAndFound.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeviceBinAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DeviceBin",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        binName = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DeviceBin");
        }
    }
}
