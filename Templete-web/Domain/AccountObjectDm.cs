using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class AccountObjectDm
    {
        public class AccountObjectDmOuput
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
            public EnumAccountType Id { get; set; }
            public string Name { get; set; }
        }
        public class StypeAccount
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
