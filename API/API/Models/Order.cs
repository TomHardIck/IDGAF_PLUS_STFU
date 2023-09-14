using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class Order
    {
        public int? IdOrder { get; set; }
        public string OrderNum { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public int? UserId { get; set; }
        public int? StatusId { get; set; }
        public double OrderSum { get; set; }
        public int? DeliveryId { get; set; }
    }
}
