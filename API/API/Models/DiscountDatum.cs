using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class DiscountDatum
    {
        public string ЛогинПользователя { get; set; } = null!;
        public string ПроцентСкидки { get; set; } = null!;
        public string? ТипСкидки { get; set; }
    }
}
