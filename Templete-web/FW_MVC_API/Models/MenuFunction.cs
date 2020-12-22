using System;
using System.Collections.Generic;

namespace FW_MVC_API.Models
{
    public partial class MenuFunction
    {
        public MenuFunction()
        {
            MenuFunction_Account = new HashSet<MenuFunction_Account>();
            MenuFunction_Role = new HashSet<MenuFunction_Role>();
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public string ControllerName { get; set; }
        public string AcctionName { get; set; }
        public string ControllerNameView { get; set; }
        public string AcctionNameView { get; set; }
        public string RouteId { get; set; }
        public bool IsMenu { get; set; }
        public bool IsPublic { get; set; }
        public string Icon { get; set; }
        public string CssClass { get; set; }
        public string Parrent { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }
        public int SortIndex { get; set; }
        public string FK_MenuSubGroup { get; set; }
        public string Note { get; set; }

        public virtual MenuFunctionSubGroup FK_MenuSubGroupNavigation { get; set; }
        public virtual ICollection<MenuFunction_Account> MenuFunction_Account { get; set; }
        public virtual ICollection<MenuFunction_Role> MenuFunction_Role { get; set; }
    }
}
