﻿using System;
using System.Collections.Generic;

#nullable disable

namespace ShoppingAssignment_SE151295.Models
{
    public partial class OrderDetail
    {
        public string OrderId { get; set; }
        public int ProductId  {get;set;}
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }

}
