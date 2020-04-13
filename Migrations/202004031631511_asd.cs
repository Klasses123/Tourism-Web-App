namespace TourismWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asd : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Travels", "TourismType_Id", "dbo.TourismTypes");
            DropForeignKey("dbo.TravelImages", "TravelId", "dbo.Travels");
            DropIndex("dbo.TravelImages", new[] { "TravelId" });
            DropIndex("dbo.Travels", new[] { "TourismType_Id" });
            DropColumn("dbo.Travels", "TourismType_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Travels", "TourismType_Id", c => c.Int());
            CreateIndex("dbo.Travels", "TourismType_Id");
            CreateIndex("dbo.TravelImages", "TravelId");
            AddForeignKey("dbo.TravelImages", "TravelId", "dbo.Travels", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Travels", "TourismType_Id", "dbo.TourismTypes", "Id");
        }
    }
}
