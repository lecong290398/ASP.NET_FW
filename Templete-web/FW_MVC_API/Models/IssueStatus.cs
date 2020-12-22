using System;
using System.Collections.Generic;

namespace FW_MVC_API.Models
{
    public partial class IssueStatus
    {
        public IssueStatus()
        {
            Issue = new HashSet<Issue>();
        }

        public int Id { get; set; }
        public string StatusName { get; set; }
        public bool IsDelete { get; set; }

        public virtual ICollection<Issue> Issue { get; set; }
    }
}
