using System;
using System.Collections.Generic;

namespace FW_MVC_API.Models
{
    public partial class MenuFunctionGroup
    {
        public MenuFunctionGroup()
        {
            MenuFunctionSubGroup = new HashSet<MenuFunctionSubGroup>();
        }

        public string Id { get; set; }
        public string GroupName { get; set; }
        public string Icon { get; set; }
        public string CssClass { get; set; }
        public int SortIndex { get; set; }
        public DateTime CreateDate { get; set; }
        public int Status { get; set; }

        public virtual ICollection<MenuFunctionSubGroup> MenuFunctionSubGroup { get; set; }
    }
}
