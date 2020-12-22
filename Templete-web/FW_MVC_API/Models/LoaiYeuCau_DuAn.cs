using System;
using System.Collections.Generic;

namespace FW_MVC_API.Models
{
    public partial class LoaiYeuCau_DuAn
    {
        public string FkLoai { get; set; }
        public string FkDuAn { get; set; }
        public int AtRowStatus { get; set; }
        public DateTime AtCreatedDate { get; set; }
        public string AtCreatedBy { get; set; }
        public DateTime? AtLastModifiedDate { get; set; }
        public string AtLastModifiedBy { get; set; }
        public byte[] AtRowVersion { get; set; }

        public virtual Project FkDuAnNavigation { get; set; }
        public virtual LoaiYeuCau FkLoaiNavigation { get; set; }
    }
}
