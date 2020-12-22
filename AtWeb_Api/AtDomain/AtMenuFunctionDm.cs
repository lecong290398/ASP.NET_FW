using System;
using System.Collections.Generic;
using System.Text;
using static AtDomain.AtMenuFunction_AccountDm;
using static AtDomain.AtMenuFunction_RoleDm;
using static AtDomain.AtMenuFunctionSubGroupDm;

namespace AtDomain
{
    public class AtMenuFuntionDm
    {
        public class GetMenuFuntionDmOutput
        {
            public List<MenuHelper_MenuFunctionOutput> listMenu { get; set; }
            public List<MenuHelper_MenuFunctionSubGroupOutput> listSubGroup { get; set; }
            public List<MenuHelper_MenuFunctionGroupOutput> listGroup { get; set; }
        }

        public class MenuHelper_MenuFunctionPermissonOutput
        {
            public string Id { get; set; }
            public string Title { get; set; }
            public string ControllerName { get; set; }
            public string AcctionName { get; set; }
            public string ControllerNameView { get; set; }
            public string AcctionNameView { get; set; }
            public string RouteId { get; set; }
        }

        public class MenuFunctionDmOutput
        {
            public string Id { get; set; }
            public string Title { get; set; }
            public string ControllerName { get; set; }
            public string AcctionName { get; set; }
            public string ControllerNameView { get; set; }
            public string AcctionNameView { get; set; }
            public int SortIndex { get; set; }
            public string RouteId { get; set; }
            public bool IsMenu { get; set; }
            public bool IsPublic { get; set; }
            public string Icon { get; set; }
            public string CssClass { get; set; }
            public DateTime CreateDate { get; set; }
            public string IdMenuSubGroup { get; set; }
            public string NameMenuSubGroup { get; set; }
            public string IdMenuGroup { get; set; }
            public string NameMenuGroup { get; set; }
            public string Parrent { get; set; }
            public int Status { get; set; }
            public string Note { get; set; }

        }

        public class MenuHelper_MenuFunctionOutput
        {
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
        }

        public class MenuHelper_MenuFunctionSubGroupOutput
        {
            public string Id { get; set; }
            public string SubGroupName { get; set; }
            public int SortIndex { get; set; }
            public string Icon { get; set; }
            public string CssClass { get; set; }
            public DateTime CreateDate { get; set; }
            public int Status { get; set; }
            public string FK_MenuGroup { get; set; }
        }

        public class MenuHelper_MenuFunctionGroupOutput
        {
            public string Id { get; set; }
            public string GroupName { get; set; }
            public string Icon { get; set; }
            public string CssClass { get; set; }
            public int SortIndex { get; set; }
            public DateTime CreateDate { get; set; }
            public int Status { get; set; }
        }

        public class MenuFunctionDmEditOutput
        {
            public string Id { get; set; }
            public string Title { get; set; }
            public string ControllerName { get; set; }
            public string AcctionName { get; set; }
            public string ControllerNameView { get; set; }
            public string AcctionNameView { get; set; }
            public int SortIndex { get; set; }
            public bool IsMenu { get; set; }
            public bool IsPublic { get; set; }
            public string FK_MenuSubGroup { get; set; }
            public string Note { get; set; }
        }

        public class MenuFunctionDmCreatetInputOrEdit
        {
            public string Id { get; set; }
            public string Title { get; set; }
            public string ControllerName { get; set; }
            public string AcctionName { get; set; }
            public string ControllerNameView { get; set; }
            public string AcctionNameView { get; set; }
            public int SortIndex { get; set; }
            public bool IsMenu { get; set; }
            public bool IsPublic { get; set; }
            public string FK_MenuSubGroup { get; set; }
            public string Note { get; set; }
        }


        public class MenuRoleOuput
        {
            public string IdRole { get; set; }
            public string NameRole { get; set; }
            public List<GroupMenuChucNang> ListMenuChucNang { get; set; }
        }

        public class MenuRoleInput
        {
            public string IdRole { get; set; }
            public List<GroupMenuChucNang> ListMenuChucNang { get; set; }
        }

        public class MenuAccountOuput
        {
            public string IdAccount { get; set; }
            public List<GroupMenuChucNang> ListMenuChucNang { get; set; }
        }

        public class MenuAccountInput
        {
            public string IdAccount { get; set; }
            public List<GroupMenuChucNang> ListMenuChucNang { get; set; }
        }

        public class GroupMenuChucNang
        {
            public string IdMenuGroud { get; set; }
            public string NameGroup { get; set; }
            public List<ListSubMenuFuntion> SubFunctions { get; set; }
        }

        public class ListSubMenuFuntion
        {
            public string IdMenuGroud { get; set; }
            public string NameGroup { get; set; }
            public string IdSubMenu { get; set; }
            public string NameSubName { get; set; }
            public List<ListMenuFuntion> MenuFunctions { get; set; }

        }
        public class ListMenuFuntion
        {
            public string IdMenuFuntion { get; set; }
            public string NameMenuFuntion { get; set; }
            public bool IsCheck { get; set; }
        }
    }
}
