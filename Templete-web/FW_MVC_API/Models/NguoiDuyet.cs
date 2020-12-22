using System;
using System.Collections.Generic;

namespace FW_MVC_API.Models
{
    public partial class NguoiDuyet
    {
        public string FkYeuCau { get; set; }
        public string FkNguoiDung { get; set; }
        public int BuocDuyet { get; set; }
        public int KieuDuyet { get; set; }
        public int KieuDuyetNhom { get; set; }
        public DateTime? NgayDuyet { get; set; }
        public int TrangThai { get; set; }
        public string PhienChuyenTiep { get; set; }
        public int? ThuTuChuyenTiep { get; set; }
        public string NoiDung { get; set; }
        public DateTime AtCreatedDate { get; set; }
        public string AtCreatedBy { get; set; }
        public DateTime? AtLastModifiedDate { get; set; }
        public string AtLastModifiedBy { get; set; }
        public byte[] AtRowVersion { get; set; }
        public int AtRowStatus { get; set; }

        public virtual YeuCau FkYeuCauNavigation { get; set; }
    }
}
