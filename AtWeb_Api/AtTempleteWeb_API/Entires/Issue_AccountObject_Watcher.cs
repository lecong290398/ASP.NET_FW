using System;
using System.Collections.Generic;

namespace AtTempleteWeb_API.Entires
{
    public partial class Issue_AccountObject_Watcher
    {
        public string FK_Issue { get; set; }
        public string FK_AccountObjetWatcher { get; set; }
        public int Type { get; set; }
        public DateTime AtCreatedDate { get; set; }
        public string AtCreatedBy { get; set; }
        public DateTime AtLastModifiedDate { get; set; }
        public string AtLastModifiedBy { get; set; }
        public byte[] AtRowversion { get; set; }
        public int AtRowStatus { get; set; }

        public virtual AccountObject FK_AccountObjetWatcherNavigation { get; set; }
        public virtual Issue FK_IssueNavigation { get; set; }
    }
}
