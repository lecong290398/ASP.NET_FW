using System;
using System.Collections.Generic;

namespace FW_MVC_API.Models
{
    public partial class FileTinNhan
    {
        public string Id { get; set; }
        public string FkTinNhan { get; set; }
        public string Ten { get; set; }
        public string Loai { get; set; }
        public int KichThuoc { get; set; }
        public DateTime AtCreatedDate { get; set; }
        public string AtCreatedBy { get; set; }
        public DateTime AtLastModifiedDate { get; set; }
        public string AtLastModifiedBy { get; set; }
        public byte[] AtRowversion { get; set; }
        public int AtRowStatus { get; set; }

        public virtual TinNhanYeuCau FkTinNhanNavigation { get; set; }
    }
}
