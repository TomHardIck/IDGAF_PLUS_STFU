using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class Position
    {
        public int? IdPosition { get; set; }
        public string PositionName { get; set; } = null!;
        public int PositionQuantity { get; set; }
        public string PositionSlife { get; set; } = null!;
        public int? CategoryId { get; set; }
        public string PhotoUrl { get; set; } = null!;
        public bool IsExists { get; set; }
        public int? DealerId { get; set; }
        public double PositionPrice { get; set; }
    }
}
