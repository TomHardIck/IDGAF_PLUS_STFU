using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class Favorite
    {
        public int? IdFavorites { get; set; }
        public int? UserId { get; set; }
        public int? PositionId { get; set; }
    }
}
