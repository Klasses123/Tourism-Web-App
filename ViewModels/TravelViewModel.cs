using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TourismWebApp.Models;

namespace TourismWebApp.ViewModels
{
    public class TravelViewModel
    {
        public Travel travel { get; set; }
        public string TravelOrganizerEmail { get; set; }
        public string TravelOrganizerPhone { get; set; }
        public static List<string> GetTourismTypes()
        {
            List<string> TourismTypesNames = new List<string>();

            using (TourismDB.TourismDB db = new TourismDB.TourismDB())
            {
                foreach(var ttn in db.TourismTypes.ToList())
                {
                    TourismTypesNames.Add(ttn.Name);
                }
            }

            return TourismTypesNames;
        }

        public static List<string> GetTravelOrganizers()
        {
            List<string> TravelOrganizersNames = new List<string>();

            using (TourismDB.TourismDB db = new TourismDB.TourismDB())
            {
                foreach (var ton in db.TravelOrganizers.ToList())
                {
                    TravelOrganizersNames.Add(ton.Name);
                }
            }

            return TravelOrganizersNames;
        }
    }
}