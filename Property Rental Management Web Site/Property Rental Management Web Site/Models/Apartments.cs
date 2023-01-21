using System;
using System.Collections.Generic;

namespace Property_Rental_Management_Web_Site.Models
{
    public partial class Apartments
    {
        public Apartments()
        {
            Rentals = new HashSet<Rentals>();
        }

        public string ApartmentNumber { get; set; }
        public string ApartmentType { get; set; }
        public string Description { get; set; }
        public string Floor { get; set; }
        public string Status { get; set; }
        public string BuildingId { get; set; }

        public virtual Buildings Building { get; set; }
        public virtual ICollection<Rentals> Rentals { get; set; }
    }
}
