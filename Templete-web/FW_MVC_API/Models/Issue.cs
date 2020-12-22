using System;
using System.Collections.Generic;

namespace FW_MVC_API.Models
{
    public partial class Issue
    {
        public Issue()
        {
            FileIssue = new HashSet<FileIssue>();
            IssueActivity = new HashSet<IssueActivity>();
            Issue_AccountObject_Watcher = new HashSet<Issue_AccountObject_Watcher>();
        }

        public string Id { get; set; }
        public int STT { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int FK_Status { get; set; }
        public int FK_Tracker { get; set; }
        public int DonePercent { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualDueDate { get; set; }
        public int? EstimationHour { get; set; }
        public string FK_Project { get; set; }
        public string FK_AccountObject { get; set; }
        public DateTime AtCreatedDate { get; set; }
        public string AtCreatedBy { get; set; }
        public DateTime AtLastModifiedDate { get; set; }
        public string AtLastModifiedBy { get; set; }
        public byte[] AtRowversion { get; set; }
        public int AtRowStatus { get; set; }

        public virtual AccountObject AtCreatedByNavigation { get; set; }
        public virtual AccountObject FK_AccountObjectNavigation { get; set; }
        public virtual Project FK_ProjectNavigation { get; set; }
        public virtual IssueStatus FK_StatusNavigation { get; set; }
        public virtual Tracker FK_TrackerNavigation { get; set; }
        public virtual ICollection<FileIssue> FileIssue { get; set; }
        public virtual ICollection<IssueActivity> IssueActivity { get; set; }
        public virtual ICollection<Issue_AccountObject_Watcher> Issue_AccountObject_Watcher { get; set; }
    }
}
