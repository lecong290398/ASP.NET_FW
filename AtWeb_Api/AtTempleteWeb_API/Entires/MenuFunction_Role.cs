﻿using System;
using System.Collections.Generic;

namespace AtTempleteWeb_API.Entires
{
    public partial class MenuFunction_Role
    {
        public string FK_Role { get; set; }
        public string FK_MenuFunction { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual MenuFunction FK_MenuFunctionNavigation { get; set; }
        public virtual Role FK_RoleNavigation { get; set; }
    }
}
