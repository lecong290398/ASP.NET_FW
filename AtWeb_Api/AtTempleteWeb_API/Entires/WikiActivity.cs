using System;
using System.Collections.Generic;

namespace AtTempleteWeb_API.Entires
{
    public partial class WikiActivity
    {
        public string Id { get; set; }
        public string FK_Wiki { get; set; }
        public string Tile { get; set; }
        public string Description { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public int Status { get; set; }
    }
}
