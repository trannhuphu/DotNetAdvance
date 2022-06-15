using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ShoppingAssignment_SE151295.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        [Required(ErrorMessage = "CustomerID is required!")]
        public string CustomerId { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "ContactName is required!")]
        public string ContactName { get; set; }

        [Required(ErrorMessage = "Address is required!")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Phone is required!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Please input valid phone number!")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email is required!")]
        [EmailAddress]
        public string Email { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
