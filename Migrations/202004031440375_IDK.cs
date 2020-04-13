namespace TourismWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IDK : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Travels", "TravelOrganizerEmail");
            DropColumn("dbo.Travels", "TravelOrganizerPhone");
            DropColumn("dbo.Travels", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Travels", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Travels", "TravelOrganizerPhone", c => c.String());
            AddColumn("dbo.Travels", "TravelOrganizerEmail", c => c.String());
        }
    }
}
