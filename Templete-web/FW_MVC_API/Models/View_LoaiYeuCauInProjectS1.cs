using System;
using System.Collections.Generic;

namespace FW_MVC_API.Models
{
    public partial class View_LoaiYeuCauInProjectS1
    {
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
        public string AccountId { get; set; }
        public string ProjectId { get; set; }
    }
}
