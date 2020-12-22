using System;
using System.Collections.Generic;

namespace AtTempleteWeb_API.Entires
{
    public partial class IssueActivity
    {
        public IssueActivity()
        {
            FileIssueActivity = new HashSet<FileIssueActivity>();
        }

        public string Id { get; set; }
        public string FK_Issue { get; set; }
        public DateTime ActivityDate { get; set; }
        public string FK_AccountObject { get; set; }
        public string Comment { get; set; }
        public bool IsChangeStatus { get; set; }
        public string ActivityContent { get; set; }
        public string ValueAfter { get; set; }
        public string ValueBefore { get; set; }

        public virtual Issue FK_IssueNavigation { get; set; }
        public virtual ICollection<FileIssueActivity> FileIssueActivity { get; set; }
    }
}
