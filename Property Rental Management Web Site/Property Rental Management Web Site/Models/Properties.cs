using System;
using System.Collections.Generic;

namespace Property_Rental_Management_Web_Site.Models
{
    public partial class Properties
    {
        public string UserId { get; set; }
        public string BuildingId { get; set; }
        public string AdressId { get; set; }

        public virtual Addresses Adress { get; set; }
        public virtual Buildings Building { get; set; }
    }
}
