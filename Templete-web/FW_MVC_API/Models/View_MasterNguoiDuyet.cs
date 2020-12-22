using System;
using System.Collections.Generic;

namespace FW_MVC_API.Models
{
    public partial class View_MasterNguoiDuyet
    {
        public string FkLoaiYeuCau { get; set; }
        public string FkVaiTro { get; set; }
        public int BuocDuyet { get; set; }
        public int KieuDuyet { get; set; }
        public int KieuDuyetNhom { get; set; }
        public DateTime AtCreatedDate { get; set; }
        public string AtCreatedBy { get; set; }
        public DateTime AtLastModifiedDate { get; set; }
        public string AtLastModifiedBy { get; set; }
        public byte[] AtRowversion { get; set; }
        public int AtRowStatus { get; set; }
    }
}
