using System;
using System.Collections.Generic;

namespace AtTempleteWeb_API.Entires
{
    public partial class MSC_TableCode
    {
        public string TableID { get; set; }
        public string TableCode { get; set; }
        public decimal CurrentValue { get; set; }
        public string TableName { get; set; }
        public string Prefix { get; set; }
        public int? Lenght { get; set; }
        public string UnsignName { get; set; }
    }
}
