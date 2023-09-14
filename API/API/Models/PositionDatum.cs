using System;
using System.Collections.Generic;

namespace API.Models
{
    public partial class PositionDatum
    {
        public string НазваниеТовара { get; set; } = null!;
        public string СсылкаНаФото { get; set; } = null!;
        public double ЦенаТовара { get; set; }
        public int КоличествоТовара { get; set; }
        public DateTime ДоКакойДатыГоден { get; set; }
        public string НазваниеКатегорииТовара { get; set; } = null!;
        public string ПоставщикТовара { get; set; } = null!;
        public string АдресПоставщикаТовара { get; set; } = null!;
    }
}
