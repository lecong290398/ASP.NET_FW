using System;
using System.Collections.Generic;

namespace AtTempleteWeb_API.Entires
{
    public partial class Setting
    {
        public string Id { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public bool? IsManual { get; set; }
        public int RowStatus { get; set; }
        public byte[] RowVersion { get; set; }
        public string ImageSlug { get; set; }
        public string IdParent { get; set; }
    }
}
