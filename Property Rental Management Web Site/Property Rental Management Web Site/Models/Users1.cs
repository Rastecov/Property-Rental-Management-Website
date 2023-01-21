using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Property_Rental_Management_Web_Site.Models
{
    public partial class Users1
    {
        [Key]
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }

        public virtual Employees Employees { get; set; }
        public virtual Tenants1 Tenants1 { get; set; }


        public bool validatePassword(string password)
        {
            return ( password == Password) ? true : false;
        }
    }
}
