using System;
using System.Collections.Generic;

namespace AtTempleteWeb_API.Entires
{
    public partial class View_ListAccountOject_ProjectRole
    {
        public string Id { get; set; }
        public string AccountCode { get; set; }
        public string AccountObjectName { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public DateTime AtCreatedDate { get; set; }
        public string AtCreatedBy { get; set; }
        public DateTime? AtLastModifiedDate { get; set; }
        public string AtLastModifiedBy { get; set; }
        public byte[] AtRowVersion { get; set; }
        public int AtRowStatus { get; set; }
        public string RoleName { get; set; }
        public string Id_Role { get; set; }
        public string FK_Project { get; set; }
    }
}
