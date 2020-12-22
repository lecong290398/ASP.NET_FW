using System;
using System.Collections.Generic;

namespace FW_MVC_API.Models
{
    public partial class Project
    {
        public Project()
        {
            Issue = new HashSet<Issue>();
            LoaiYeuCau_DuAn = new HashSet<LoaiYeuCau_DuAn>();
            Project_AccountObject = new HashSet<Project_AccountObject>();
            Wiki = new HashSet<Wiki>();
        }

        public string Id { get; set; }
        public string Code { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public DateTime AtCreatedDate { get; set; }
        public string AtCreatedBy { get; set; }
        public DateTime? AtLastModifiedDate { get; set; }
        public string AtLastModifiedBy { get; set; }
        public byte[] AtRowVersion { get; set; }
        public int AtRowStatus { get; set; }

        public virtual ICollection<Issue> Issue { get; set; }
        public virtual ICollection<LoaiYeuCau_DuAn> LoaiYeuCau_DuAn { get; set; }
        public virtual ICollection<Project_AccountObject> Project_AccountObject { get; set; }
        public virtual ICollection<Wiki> Wiki { get; set; }
    }
}
