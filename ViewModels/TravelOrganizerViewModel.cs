using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TourismWebApp.Models;

namespace TourismWebApp.ViewModels
{
    public class TravelOrganizerViewModel
    {
        public TravelOrganizer TravelOrganizer { get; set; }
        public IEnumerable<Travel> Travels { get; set; }
    }
}