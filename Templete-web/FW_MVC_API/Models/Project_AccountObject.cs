using System;
using System.Collections.Generic;

namespace FW_MVC_API.Models
{
    public partial class Project_AccountObject
    {
        public string FK_AccountObject { get; set; }
        public string FK_Project { get; set; }
        public string FK_ProjectRole { get; set; }
        public DateTime AtCreatedDate { get; set; }
        public string AtCreatedBy { get; set; }
        public DateTime? AtLastModifiedDate { get; set; }
        public string AtLastModifiedBy { get; set; }
        public byte[] AtRowversion { get; set; }
        public int AtRowStatus { get; set; }

        public virtual AccountObject FK_AccountObjectNavigation { get; set; }
        public virtual Project FK_ProjectNavigation { get; set; }
        public virtual Role FK_ProjectRoleNavigation { get; set; }
    }
}
