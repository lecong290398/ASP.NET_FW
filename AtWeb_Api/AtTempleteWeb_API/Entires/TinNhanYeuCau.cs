using System;
using System.Collections.Generic;

namespace AtTempleteWeb_API.Entires
{
    public partial class TinNhanYeuCau
    {
        public TinNhanYeuCau()
        {
            FileTinNhan = new HashSet<FileTinNhan>();
        }

        public string Id { get; set; }
        public string FkYeuCau { get; set; }
        public string NoiDung { get; set; }
        public DateTime AtCreatedDate { get; set; }
        public string AtCreatedBy { get; set; }
        public DateTime AtLastModifiedDate { get; set; }
        public string AtLastModifiedBy { get; set; }
        public byte[] AtRowversion { get; set; }
        public int AtRowStatus { get; set; }

        public virtual YeuCau FkYeuCauNavigation { get; set; }
        public virtual ICollection<FileTinNhan> FileTinNhan { get; set; }
    }
}
