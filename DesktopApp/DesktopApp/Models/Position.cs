using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class Position
    {
        public int? IdPosition { get; set; }
        public string PositionName { get; set; }
        public int PositionQuantity { get; set; }
        public string PositionSlife { get; set; }
        public int? CategoryId { get; set; }
        public string PhotoUrl { get; set; }
        public bool IsExists { get; set; }
        public int? DealerId { get; set; }
        public double PositionPrice { get; set; }
    }
}
