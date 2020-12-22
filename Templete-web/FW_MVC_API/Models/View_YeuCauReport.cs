using System;
using System.Collections.Generic;

namespace FW_MVC_API.Models
{
    public partial class View_YeuCauReport
    {
        public string Id { get; set; }
        public string Fk_DuAn { get; set; }
        public string TenYeuCau { get; set; }
        public string NoiDung { get; set; }
        public DateTime? ThoiHan { get; set; }
        public double SoTien { get; set; }
        public string DiaDiem { get; set; }
        public int TrangThai { get; set; }
        public int BuocDuyet { get; set; }
        public int TongBuocDuyet { get; set; }
        public DateTime AtCreatedDate { get; set; }
        public string AtCreatedBy { get; set; }
        public DateTime? AtLastModifiedDate { get; set; }
        public string AtLastModifiedBy { get; set; }
        public byte[] AtRowVersion { get; set; }
        public int AtRowStatus { get; set; }
        public string MaLoaiYeuCau { get; set; }
        public string TenLoaiYeuCau { get; set; }
        public string MaNhomYeuCau { get; set; }
        public string TenNhomYeuCau { get; set; }
        public string MaNguoiDuyet { get; set; }
        public string TenNguoiDuyet { get; set; }
    }
}
