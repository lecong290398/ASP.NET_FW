using System;
using System.Collections.Generic;

namespace AtTempleteWeb_API.Entires
{
    public partial class MSC_AudittingLog
    {
        public string EventID { get; set; }
        public string LoginName { get; set; }
        public DateTime? Time { get; set; }
        public string Description { get; set; }
        public string UserID { get; set; }
        public string Data_Old { get; set; }
        public string Data_New { get; set; }
    }
}
