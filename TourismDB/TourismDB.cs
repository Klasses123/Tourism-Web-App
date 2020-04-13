namespace TourismWebApp.TourismDB
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using TourismWebApp.Models;

    public class TourismDB : DbContext
    {
        public TourismDB()
            : base("name=TourismDB")
        {

        }

        public virtual DbSet<TourismType> TourismTypes { get; set; }
        public virtual DbSet<TravelOrganizer> TravelOrganizers { get; set; }
        public virtual DbSet<Travel> Travels { get; set; }
        public virtual DbSet <TravelImage> TravelImages { get; set; }
    }
}