using System;
using System.Collections.Generic;

namespace FW_MVC_API.Models
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
