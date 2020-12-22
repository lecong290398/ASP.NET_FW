using System;
using System.Collections.Generic;

namespace AtTempleteWeb_API.Entires
{
    public partial class Department
    {
        public Department()
        {
            Department_NhomYeuCau = new HashSet<Department_NhomYeuCau>();
        }

        public string Id { get; set; }
        public string Code { get; set; }
        public string DepartmentName { get; set; }
        public DateTime AtCreatedDate { get; set; }
        public string AtCreatedBy { get; set; }
        public DateTime? AtLastModifiedDate { get; set; }
        public string AtLastModifiedBy { get; set; }
        public byte[] AtRowversion { get; set; }
        public int AtRowStatus { get; set; }

        public virtual ICollection<Department_NhomYeuCau> Department_NhomYeuCau { get; set; }
    }
}
