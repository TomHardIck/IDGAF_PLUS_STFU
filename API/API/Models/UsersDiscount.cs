using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class UsersDiscount
    {
        public int? IdUserDiscount { get; set; }
        public int? UserId { get; set; }
        public int? DiscountId { get; set; }
    }
}
