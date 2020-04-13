using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TourismWebApp.Models;

namespace TourismWebApp.ViewModels
{
    public class TourismTypesViewModel
    {
        public IEnumerable<TourismType> TourismTypes { get; set; }
    }
}