using System;
using System.Collections.Generic;

namespace AtTempleteWeb_API.Entires
{
    public partial class AccountObject
    {
        public AccountObject()
        {
            InfomationUser = new HashSet<InfomationUser>();
            IssueAtCreatedByNavigation = new HashSet<Issue>();
            IssueFK_AccountObjectNavigation = new HashSet<Issue>();
            Issue_AccountObject_Watcher = new HashSet<Issue_AccountObject_Watcher>();
            LoaiYeuCauQuanTrong = new HashSet<LoaiYeuCauQuanTrong>();
            MenuFunction_Account = new HashSet<MenuFunction_Account>();
            NhomYeuCau_Account = new HashSet<NhomYeuCau_Account>();
            Project_AccountObject = new HashSet<Project_AccountObject>();
            Role_AccountObject = new HashSet<Role_AccountObject>();
        }

        public string Id { get; set; }
        public string AccountCode { get; set; }
        public string AccountObjectName { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime AtCreatedDate { get; set; }
        public string AtCreatedBy { get; set; }
        public DateTime? AtLastModifiedDate { get; set; }
        public string AtLastModifiedBy { get; set; }
        public byte[] AtRowVersion { get; set; }
        public int AtRowStatus { get; set; }

        public virtual ICollection<InfomationUser> InfomationUser { get; set; }
        public virtual ICollection<Issue> IssueAtCreatedByNavigation { get; set; }
        public virtual ICollection<Issue> IssueFK_AccountObjectNavigation { get; set; }
        public virtual ICollection<Issue_AccountObject_Watcher> Issue_AccountObject_Watcher { get; set; }
        public virtual ICollection<LoaiYeuCauQuanTrong> LoaiYeuCauQuanTrong { get; set; }
        public virtual ICollection<MenuFunction_Account> MenuFunction_Account { get; set; }
        public virtual ICollection<NhomYeuCau_Account> NhomYeuCau_Account { get; set; }
        public virtual ICollection<Project_AccountObject> Project_AccountObject { get; set; }
        public virtual ICollection<Role_AccountObject> Role_AccountObject { get; set; }
    }
}
