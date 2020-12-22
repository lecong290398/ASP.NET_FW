using System;
using System.Collections.Generic;

namespace AtTempleteWeb_API.Entires
{
    public partial class MenuFunction_Account
    {
        public string FK_AccountObject { get; set; }
        public string FK_MenuFunction { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual AccountObject FK_AccountObjectNavigation { get; set; }
        public virtual MenuFunction FK_MenuFunctionNavigation { get; set; }
    }
}
