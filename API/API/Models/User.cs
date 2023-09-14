using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class User
    {
        public int? IdUser { get; set; }
        public string UserLogin { get; set; } = null!;
        public string UserPassword { get; set; } = null!;
        public string UserFname { get; set; } = null!;
        public string UserLname { get; set; } = null!;
        public int? UserRoleId { get; set; }
        public string Salt { get; set; } = null!;
    }
}
