using System;
using System.Collections.Generic;

namespace Property_Rental_Management_Web_Site.Models
{
    public partial class Buildings
    {
        public Buildings()
        {
            Apartments = new HashSet<Apartments>();
            Properties = new HashSet<Properties>();
        }

        public string BuildingId { get; set; }
        public string BuildingName { get; set; }
        public string AdressId { get; set; }

        public virtual Addresses Adress { get; set; }
        public virtual ICollection<Apartments> Apartments { get; set; }
        public virtual ICollection<Properties> Properties { get; set; }
    }
}
