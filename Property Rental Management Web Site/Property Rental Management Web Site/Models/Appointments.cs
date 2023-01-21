using System;
using System.Collections.Generic;

namespace Property_Rental_Management_Web_Site.Models
{
    public partial class Appointments
    {
        public Appointments()
        {
            Schedules = new HashSet<Schedules>();
        }

        public string AppointmentId { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public TimeSpan? AppointmentTime { get; set; }
        public string TenantId { get; set; }
        public string EmployeeId { get; set; }

        public virtual Employees Employee { get; set; }
        public virtual Tenants1 Tenant { get; set; }
        public virtual ICollection<Schedules> Schedules { get; set; }
    }
}
