using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TourismWebApp.Models
{
    public class TravelOrganizer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public string Desc { get; set; }
        public string ImgUrl { get; set; }
        public virtual List<Travel> Travels { get; set; }
    }
}