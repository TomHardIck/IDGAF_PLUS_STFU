using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class Discount
    {
        public int? IdDiscount { get; set; }
        public int DiscountPercentId { get; set; }
        public string DiscountName { get; set; }
    }
}
