namespace TourismWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TourismTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Desc = c.String(),
                        ImgUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TravelImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TravelId = c.Int(nullable: false),
                        ImgUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Travels", t => t.TravelId, cascadeDelete: true)
                .Index(t => t.TravelId);
            
            CreateTable(
                "dbo.TravelOrganizers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        Desc = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Travels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TourismTypeName = c.String(),
                        TravelOrganizerName = c.String(),
                        Name = c.String(),
                        Desc = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        TourismType_Id = c.Int(),
                        TravelOrganizer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TourismTypes", t => t.TourismType_Id)
                .ForeignKey("dbo.TravelOrganizers", t => t.TravelOrganizer_Id)
                .Index(t => t.TourismType_Id)
                .Index(t => t.TravelOrganizer_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Travels", "TravelOrganizer_Id", "dbo.TravelOrganizers");
            DropForeignKey("dbo.TravelImages", "TravelId", "dbo.Travels");
            DropForeignKey("dbo.Travels", "TourismType_Id", "dbo.TourismTypes");
            DropIndex("dbo.Travels", new[] { "TravelOrganizer_Id" });
            DropIndex("dbo.Travels", new[] { "TourismType_Id" });
            DropIndex("dbo.TravelImages", new[] { "TravelId" });
            DropTable("dbo.Travels");
            DropTable("dbo.TravelOrganizers");
            DropTable("dbo.TravelImages");
            DropTable("dbo.TourismTypes");
        }
    }
}
