using System;
using System.Collections.Generic;

namespace Property_Rental_Management_Web_Site.Models
{
    public partial class Addresses
    {
        public Addresses()
        {
            Buildings = new HashSet<Buildings>();
            Properties = new HashSet<Properties>();
        }

        public string AddressId { get; set; }
        public string StreetNumber { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }

        public virtual ICollection<Buildings> Buildings { get; set; }
        public virtual ICollection<Properties> Properties { get; set; }
    }
}
