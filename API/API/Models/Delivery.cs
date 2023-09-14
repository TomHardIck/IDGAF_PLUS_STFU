using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class Delivery
    {
        public int? IdDelivery { get; set; }
        public double DeliveryCost { get; set; }
        public int? DeliveryTypeId { get; set; }
        public int? DeliveryStatusId { get; set; }
    }
}
