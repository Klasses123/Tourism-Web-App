namespace TourismWebApp.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TourismWebApp.TourismDB.TourismDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "TourismWebApp.TourismDB.TourismDB";
        }

        protected override void Seed(TourismWebApp.TourismDB.TourismDB context)
        {
        }
    }
}
