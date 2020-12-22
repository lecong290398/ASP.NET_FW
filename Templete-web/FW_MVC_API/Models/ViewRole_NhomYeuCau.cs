using System;
using System.Collections.Generic;

namespace FW_MVC_API.Models
{
    public partial class ViewRole_NhomYeuCau
    {
        public string IdNhomYeuCau { get; set; }
        public string TenNhomYeuCau { get; set; }
        public string CodeNhomYeuCau { get; set; }
        public string FK_Role { get; set; }
        public string FK_NhomYeuCau { get; set; }
        public DateTime AtCreatedDate { get; set; }
        public string AtCreatedBy { get; set; }
        public DateTime AtLastModifiedDate { get; set; }
        public string AtLastModifiedBy { get; set; }
        public byte[] AtRowversion { get; set; }
        public int AtRowStatus { get; set; }
    }
}
