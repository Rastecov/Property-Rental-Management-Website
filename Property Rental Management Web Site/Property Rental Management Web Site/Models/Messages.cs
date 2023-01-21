using System;
using System.Collections.Generic;

namespace Property_Rental_Management_Web_Site.Models
{
    public partial class Messages
    {
        public string MessageId { get; set; }
        public string Message { get; set; }
        public string TenantId { get; set; }
        public string EmployeeId { get; set; }

        public virtual Employees Employee { get; set; }
        public virtual Tenants1 Tenant { get; set; }
    }
}
