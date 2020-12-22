using System;
using System.Collections.Generic;

namespace AtTempleteWeb_API.Entires
{
    public partial class WikiHistory
    {
        public string Id { get; set; }
        public string Fk_Wiki { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
        public byte[] AtRowversion { get; set; }
    }
}
