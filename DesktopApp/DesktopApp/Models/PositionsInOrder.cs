using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class PositionsInOrder
    {
        public int? IdPositionInOrder { get; set; }
        public int? PositionId { get; set; }
        public int? OrderId { get; set; }
    }
}
