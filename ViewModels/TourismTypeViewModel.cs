using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TourismWebApp.Models;

namespace TourismWebApp.ViewModels
{
    public class TourismTypeViewModel
    {
        public TourismType TourismType { get; set; }
        public IEnumerable<Travel> Travels { get; set; }
    }
}