using System;
using System.Collections.Generic;

namespace FW_MVC_API.Models
{
    public partial class View_LoaiYeuCau_DuAN
    {
        public string FkLoai { get; set; }
        public string FkDuAn { get; set; }
        public int AtRowStatus { get; set; }
        public DateTime AtCreatedDate { get; set; }
        public string AtCreatedBy { get; set; }
        public DateTime? AtLastModifiedDate { get; set; }
        public string AtLastModifiedBy { get; set; }
        public byte[] AtRowVersion { get; set; }
        public string Id_LoaiYeuCau { get; set; }
        public string TenLoaiYeuCau { get; set; }
        public string MaYeuCau { get; set; }
        public string TenNhom { get; set; }
    }
}
