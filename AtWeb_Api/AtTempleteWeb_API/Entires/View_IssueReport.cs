using System;
using System.Collections.Generic;

namespace AtTempleteWeb_API.Entires
{
    public partial class View_IssueReport
    {
        public string Id { get; set; }
        public int STT { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int FK_Status { get; set; }
        public int FK_Tracker { get; set; }
        public int DonePercent { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualDueDate { get; set; }
        public int? EstimationHour { get; set; }
        public string FK_Project { get; set; }
        public string FK_AccountObject { get; set; }
        public DateTime AtCreatedDate { get; set; }
        public string AtCreatedBy { get; set; }
        public DateTime AtLastModifiedDate { get; set; }
        public string AtLastModifiedBy { get; set; }
        public byte[] AtRowversion { get; set; }
        public int AtRowStatus { get; set; }
        public string ProjectName { get; set; }
        public string ProjectCode { get; set; }
        public string StatusName { get; set; }
        public string Name { get; set; }
        public string AccountCode { get; set; }
        public string AccountObjectName { get; set; }
        public string FK_AccountObjetWatcher { get; set; }
    }
}
