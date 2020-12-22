using System;
using System.Collections.Generic;

namespace FW_MVC_API.Models
{
    public partial class Role
    {
        public Role()
        {
            MenuFunction_Role = new HashSet<MenuFunction_Role>();
            Project_AccountObject = new HashSet<Project_AccountObject>();
            Role_AccountObject = new HashSet<Role_AccountObject>();
            Role_NhomYeuCau = new HashSet<Role_NhomYeuCau>();
        }

        public string Id { get; set; }
        public string Code { get; set; }
        public string RoleName { get; set; }
        public DateTime AtCreatedDate { get; set; }
        public string AtCreatedBy { get; set; }
        public DateTime? AtLastModifiedDate { get; set; }
        public string AtLastModifiedBy { get; set; }
        public byte[] AtRowversion { get; set; }
        public int AtRowStatus { get; set; }
        public int Prioty { get; set; }

        public virtual ICollection<MenuFunction_Role> MenuFunction_Role { get; set; }
        public virtual ICollection<Project_AccountObject> Project_AccountObject { get; set; }
        public virtual ICollection<Role_AccountObject> Role_AccountObject { get; set; }
        public virtual ICollection<Role_NhomYeuCau> Role_NhomYeuCau { get; set; }
    }
}
