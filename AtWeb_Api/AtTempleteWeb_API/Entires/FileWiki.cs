using System;
using System.Collections.Generic;

namespace AtTempleteWeb_API.Entires
{
    public partial class FileWiki
    {
        public string Id { get; set; }
        public string FK_Wiki { get; set; }
        public string DisplayName { get; set; }
        public string FilePath { get; set; }
        public int FileSize { get; set; }
        public DateTime AtCreatedDate { get; set; }
        public string AtCreatedBy { get; set; }
        public DateTime AtLastModifiedDate { get; set; }
        public string AtLastModifiedBy { get; set; }
        public byte[] AtRowversion { get; set; }
        public int AtRowStatus { get; set; }

        public virtual Wiki FK_WikiNavigation { get; set; }
    }
}
