using System;
using System.Collections.Generic;

namespace AtTempleteWeb_API.Entires
{
    public partial class NguoiTheoDoi
    {
        public string FkYeuCau { get; set; }
        public string FkNguoiDung { get; set; }
        public DateTime AtCreatedDate { get; set; }
        public string AtCreatedBy { get; set; }
        public DateTime? AtLastModifiedDate { get; set; }
        public string AtLastModifiedBy { get; set; }
        public byte[] AtRowVersion { get; set; }
        public int AtRowStatus { get; set; }

        public virtual YeuCau FkYeuCauNavigation { get; set; }
    }
}
