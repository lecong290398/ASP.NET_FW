using System;
using System.Collections.Generic;

namespace FW_MVC_API.Models
{
    public partial class YeuCau
    {
        public YeuCau()
        {
            FileYeuCau = new HashSet<FileYeuCau>();
            HoatDongYeuCau = new HashSet<HoatDongYeuCau>();
            NguoiDuyet = new HashSet<NguoiDuyet>();
            NguoiTheoDoi = new HashSet<NguoiTheoDoi>();
            TinNhanYeuCau = new HashSet<TinNhanYeuCau>();
        }

        public string Id { get; set; }
        public string Code { get; set; }
        public string FK_LoaiYeuCau { get; set; }
        public string Fk_DuAn { get; set; }
        public string Ten { get; set; }
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

        public virtual LoaiYeuCau FK_LoaiYeuCauNavigation { get; set; }
        public virtual ICollection<FileYeuCau> FileYeuCau { get; set; }
        public virtual ICollection<HoatDongYeuCau> HoatDongYeuCau { get; set; }
        public virtual ICollection<NguoiDuyet> NguoiDuyet { get; set; }
        public virtual ICollection<NguoiTheoDoi> NguoiTheoDoi { get; set; }
        public virtual ICollection<TinNhanYeuCau> TinNhanYeuCau { get; set; }
    }
}
