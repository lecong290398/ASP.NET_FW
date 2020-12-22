using System;
using System.Collections.Generic;
using System.Text;

namespace AtDomain
{
   public  class AtRoleDm
    {
        public class AtRoleDmComboboxOutput
        {
            public string Id { get; set; }
            public string Code { get; set; }
            public string RoleName { get; set; }
        }

        public class AtRoleDmListOutput
        {
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
        }

        public class AtRoleDmInputCreate
        {
            public string Code { get; set; }
            public string RoleName { get; set; }
            public int Prioty { get; set; }
        }

        public class AtRoleDmInputDelete
        {
            public string Id { get; set; }
            public byte[] AtRowversion { get; set; }
        }


        public class AtRoleDmInputEdit
        {
            public string Id { get; set; }
            public string RoleName { get; set; }
            public byte[] AtRowversion { get; set; }
        }
    }
}
