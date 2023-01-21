﻿using System;
using System.Collections.Generic;

namespace Property_Rental_Management_Web_Site.Models
{
    public partial class Employees
    {
        public Employees()
        {
            Appointments = new HashSet<Appointments>();
            Messages = new HashSet<Messages>();
            Rentals = new HashSet<Rentals>();
            Schedules = new HashSet<Schedules>();
        }

        public string EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public virtual Users1 Employee { get; set; }
        public virtual ICollection<Appointments> Appointments { get; set; }
        public virtual ICollection<Messages> Messages { get; set; }
        public virtual ICollection<Rentals> Rentals { get; set; }
        public virtual ICollection<Schedules> Schedules { get; set; }
    }
}
