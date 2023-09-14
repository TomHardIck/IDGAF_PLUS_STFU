using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class UsersDatum
    {
        public string Имя { get; set; } = null!;
        public string Фамилия { get; set; } = null!;
        public string Логин { get; set; } = null!;
        public string Пароль { get; set; } = null!;
        public string РольПользователя { get; set; } = null!;
    }
}
