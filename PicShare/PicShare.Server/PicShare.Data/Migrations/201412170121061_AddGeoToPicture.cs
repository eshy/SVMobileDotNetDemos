namespace PicShare.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Spatial;
    
    public partial class AddGeoToPicture : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pictures", "Location", c => c.Geography());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pictures", "Location");
        }
    }
}
