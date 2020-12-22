using System;
using System.Collections.Generic;

namespace AtTempleteWeb_API.Entires
{
    public partial class LoaiYeuCau
    {
        public LoaiYeuCau()
        {
            LoaiYeuCauQuanTrong = new HashSet<LoaiYeuCauQuanTrong>();
            LoaiYeuCau_DuAn = new HashSet<LoaiYeuCau_DuAn>();
            MasterNguoiDuyet = new HashSet<MasterNguoiDuyet>();
            YeuCau = new HashSet<YeuCau>();
        }

        public string Id { get; set; }
        public string Code { get; set; }
        public int CurrentValue { get; set; }
        public string FK_NhomYeuCau { get; set; }
        public string Ten { get; set; }
        public int KieuDuyet { get; set; }
        public bool ChonNguoiDuyet { get; set; }
        public DateTime AtCreatedDate { get; set; }
        public string AtCreatedBy { get; set; }
        public DateTime? AtLastModifiedDate { get; set; }
        public string AtLastModifiedBy { get; set; }
        public byte[] AtRowVersion { get; set; }
        public int AtRowStatus { get; set; }
        public bool QuanTrong { get; set; }
        public int Lenght { get; set; }

        public virtual NhomYeuCau FK_NhomYeuCauNavigation { get; set; }
        public virtual ICollection<LoaiYeuCauQuanTrong> LoaiYeuCauQuanTrong { get; set; }
        public virtual ICollection<LoaiYeuCau_DuAn> LoaiYeuCau_DuAn { get; set; }
        public virtual ICollection<MasterNguoiDuyet> MasterNguoiDuyet { get; set; }
        public virtual ICollection<YeuCau> YeuCau { get; set; }
    }
}
