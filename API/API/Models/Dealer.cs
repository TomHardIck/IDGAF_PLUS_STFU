using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class Dealer
    {

        public int? IdDealer { get; set; }
        public string DealerName { get; set; } = null!;
        public string DealerAddr { get; set; } = null!;
    }
}
