﻿using System;
using System.Collections.Generic;

namespace AtTempleteWeb_API.Entires
{
    public partial class View_MenuFunction
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
        public string IdSubGroup { get; set; }
        public string SubGroupName { get; set; }
        public int SortIndexSubGroup { get; set; }
        public string CssClassSubGroup { get; set; }
        public string IconSubGroup { get; set; }
        public string IdGroup { get; set; }
        public string GroupName { get; set; }
        public int SortIndexGroup { get; set; }
        public string CssClassGroup { get; set; }
        public string IconGroup { get; set; }
        public string IdAccount { get; set; }
    }
}
