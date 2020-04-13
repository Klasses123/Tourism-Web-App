using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TourismWebApp.Models
{
    public class TravelImage
    {
        public int Id { get; set; }
        public int TravelId { get; set; }
        public string ImgUrl { get; set; }
    }
}