using System;
using System.Collections.Generic;

namespace AtTempleteWeb_API.Entires
{
    public partial class MenuFunctionSubGroup
    {
        public MenuFunctionSubGroup()
        {
            MenuFunction = new HashSet<MenuFunction>();
        }

        public string Id { get; set; }
        public string SubGroupName { get; set; }
        public int SortIndex { get; set; }
        public string Icon { get; set; }
        public string CssClass { get; set; }
        public DateTime CreateDate { get; set; }
        public int Status { get; set; }
        public string FK_MenuGroup { get; set; }

        public virtual MenuFunctionGroup FK_MenuGroupNavigation { get; set; }
        public virtual ICollection<MenuFunction> MenuFunction { get; set; }
    }
}
