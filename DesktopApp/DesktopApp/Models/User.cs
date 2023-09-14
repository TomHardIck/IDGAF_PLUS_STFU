using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class User
    {
        public int? IdUser { get; set; }
        public string UserLogin { get; set; }
        public string UserPassword { get; set; }
        public string UserFname { get; set; }
        public string UserLname { get; set; }
        public int? UserRoleId { get; set; }
        public string Salt { get; set; }
    }
}
