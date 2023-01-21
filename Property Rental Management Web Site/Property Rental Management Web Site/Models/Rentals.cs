using System;
using System.Collections.Generic;

namespace Property_Rental_Management_Web_Site.Models
{
    public partial class Rentals
    {
        public string RentalId { get; set; }
        public string RentalPrice { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? Enddate { get; set; }
        public string ApartmentId { get; set; }
        public string TenantId { get; set; }
        public string EmployeeId { get; set; }

        public virtual Apartments Apartment { get; set; }
        public virtual Employees Employee { get; set; }
        public virtual Tenants1 Tenant { get; set; }
    }
}
