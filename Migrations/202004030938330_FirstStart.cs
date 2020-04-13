namespace TourismWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstStart : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TravelOrganizers", "ImgUrl", c => c.String());
            AddColumn("dbo.Travels", "ImgUrl", c => c.String());
            AddColumn("dbo.Travels", "TravelOrganizerEmail", c => c.String());
            AddColumn("dbo.Travels", "TravelOrganizerPhone", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Travels", "TravelOrganizerPhone");
            DropColumn("dbo.Travels", "TravelOrganizerEmail");
            DropColumn("dbo.Travels", "ImgUrl");
            DropColumn("dbo.TravelOrganizers", "ImgUrl");
        }
    }
}
