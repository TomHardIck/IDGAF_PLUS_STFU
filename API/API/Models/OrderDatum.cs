using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class OrderDatum
    {
        public string НомерЗаказа { get; set; } = null!;
        public DateTime ДатаЗаказа { get; set; }
        public double СуммаЗаказа { get; set; }
        public string СтатусЗаказа { get; set; } = null!;
        public string СтатусДоставки { get; set; } = null!;
        public string ТипДоставки { get; set; } = null!;
    }
}
