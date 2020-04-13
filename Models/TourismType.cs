using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TourismWebApp.Models
{
    public class TourismType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string ImgUrl { get; set; }
    }
}