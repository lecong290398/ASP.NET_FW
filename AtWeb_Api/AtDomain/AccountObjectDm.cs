using System;
using System.Collections.Generic;
using System.Text;
using static AtDomain.AtDepartmentDm;
using static AtDomain.AtRoleDm;

namespace AtDomain
{
    public class AccountObjectDm
    {
        public class AccountObjectDmOutput
        {
            public string Id { get; set; }
            public string AccountCode { get; set; }
            public string AccountObjectName { get; set; }
            public string UserName { get; set; }
        }

        public class AccountInput
        {

        }

        public class TypeAccount
        {
            public AtEnumAccountType Id { get; set; }
            public string Name { get; set; }
        }
        public class StypeAccount
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class AccountObjectDmListOutput
        {
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
        }

        public class AccountObjectDmOuput_EditOrDetail
        {
            public AccountObjectDmListOutput AccountObject_Edit { get; set; }
            public List<string> ListIdRole { get; set; }
            public List<string> ListIdPhongBan { get; set; }
            public List<AtDepartmentDmComboboxOutput> ListDepartment { get; set; }
            public List<AtRoleDmComboboxOutput> ListRole { get; set; }
        }

        public class AccountObjectDmInput_Create
        {
            public string AccountCode { get; set; }
            public string AccountObjectName { get; set; }
            public string UserName { get; set; }
            public string PassWord { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public List<string> ListIdRole { get; set; }
            public List<string> ListIdDepartment { get; set; }
        }

        public class AccountObjectDmInput_Edit
        {
            public string Id { get; set; }
            public string AccountObjectName { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public byte[] AtRowVersion { get; set; }
            public List<string> ListIdRole { get; set; }
            public List<string> ListIdDepartment { get; set; }
        }



        public class AccountObjectDm_Delete
        {
            public string Id { get; set; }
            public byte[] AtRowVersion { get; set; }
        }
        
        
        public class AccountObjectDm_ResetPassword
        {
            public string Id { get; set; }
            public byte[] AtRowVersion { get; set; }
            public string PassWord { get; set; }
        }

        public class AccountObjectDm_UpdateAccount
        {
            public string AccountObjectName { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
        }
        public class AccountObjectDm_UpdatePass
        {
            public string oldPass { get; set; }
            public string newPass { get; set; }
        }

    }
}
