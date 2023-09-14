using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class UserRole
    {
        public int? IdUserRole { get; set; }
        public string UserRoleName { get; set; } = null!;
    }
}
