using System;
using System.Collections.Generic;

namespace FW_MVC_API.Models
{
    public partial class NhomYeuCau
    {
        public NhomYeuCau()
        {
            LoaiYeuCau = new HashSet<LoaiYeuCau>();
            NhomYeuCau_Account = new HashSet<NhomYeuCau_Account>();
            Role_NhomYeuCau = new HashSet<Role_NhomYeuCau>();
        }

        public string Id { get; set; }
        public string Code { get; set; }
        public string Ten { get; set; }
        public DateTime AtCreatedDate { get; set; }
        public string AtCreatedBy { get; set; }
        public DateTime? AtLastModifiedDate { get; set; }
        public string AtLastModifiedBy { get; set; }
        public byte[] AtRowVersion { get; set; }
        public int AtRowStatus { get; set; }

        public virtual ICollection<LoaiYeuCau> LoaiYeuCau { get; set; }
        public virtual ICollection<NhomYeuCau_Account> NhomYeuCau_Account { get; set; }
        public virtual ICollection<Role_NhomYeuCau> Role_NhomYeuCau { get; set; }
    }
}
