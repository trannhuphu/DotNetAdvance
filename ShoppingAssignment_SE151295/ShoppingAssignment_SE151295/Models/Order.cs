using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ShoppingAssignment_SE151295.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }
        [Required(ErrorMessage = "OrderId is required!")]
        public string OrderId { get; set; }

        [Required(ErrorMessage = "CustomerId is required!")]
        public string CustomerId { get; set; }

        public DateTime? OrderDate { get; set; }

        [Required(ErrorMessage = "Required Date is required!")]
        public DateTime? RequiredDate { get; set; }

        [Required(ErrorMessage = "Shipped Date is required!")]
        public DateTime? ShippedDate { get; set; }

        [Required(ErrorMessage = "Freight is required")]
        [RegularExpression("([0-9]+([.][0-9]*)?|[.][0-9]+)", ErrorMessage = "Please enter positive number")]
        public decimal? Freight { get; set; }

        [Required(ErrorMessage = "Ship Address is required!")]
        public string ShipAddress { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
