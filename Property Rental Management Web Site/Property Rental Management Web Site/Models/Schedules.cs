using System;
using System.Collections.Generic;

namespace Property_Rental_Management_Web_Site.Models
{
    public partial class Schedules
    {
        public string ScheduleId { get; set; }
        public DateTime? ScheduleDate { get; set; }
        public TimeSpan? ScheduleTime { get; set; }
        public string TenantId { get; set; }
        public string EmployeeId { get; set; }
        public string AppointmentId { get; set; }

        public virtual Appointments Appointment { get; set; }
        public virtual Employees Employee { get; set; }
        public virtual Tenants1 Tenant { get; set; }
    }
}
