using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TourismWebApp.Models
{
    public class Travel
    {
        public int Id { get; set; }
        public string TourismTypeName { get; set; }
        public string TravelOrganizerName { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public string Desc { get; set; }
    }
}