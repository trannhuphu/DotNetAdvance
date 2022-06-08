using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ShoppingAssignment_SE151295.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        [Required(ErrorMessage = "ProductId is required!")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "ProductName is required!")]
        public string ProductName { get; set; }

        public int? SupplierId { get; set; }
        public int? CategoryId { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [RegularExpression("^[1-9]+[0-9]*$", ErrorMessage = "Please enter positive number")]
        public int? QuantityPerUnit { get; set; }

        [Required(ErrorMessage = "UnitPrice is required")]
        [RegularExpression("^[1-9]+[0-9]*$", ErrorMessage = "Please enter positive number")]
        public decimal? UnitPrice { get; set; }
        public string ProductImage { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [RegularExpression("^[01]$", ErrorMessage = "Please enter status 0 or 1")]
        public byte? ProductStatus { get; set; }

        public virtual Category Category { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
