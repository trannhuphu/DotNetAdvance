using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ShoppingAssignment_SE151295.Models
{
    public partial class OrderDetail
    {
        public string OrderId { get; set; }
        public int ProductId  {get;set;}
        public decimal UnitPrice { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [RegularExpression("^[1-9]+[0-9]*$", ErrorMessage = "Please enter positive number")]
        public short Quantity { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }

}
