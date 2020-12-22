using System;
using System.Collections.Generic;

namespace AtTempleteWeb_API.Entires
{
    public partial class YeuCauActivity
    {
        public string Id { get; set; }
        public string FK_YeuCau { get; set; }
        public DateTime ActivityDate { get; set; }
        public string FK_AccountObject { get; set; }
        public int? Loai { get; set; }
        public string NoiDung { get; set; }
        public string Comment { get; set; }
        public bool IsChangeStatus { get; set; }
        public string ActivityContent { get; set; }
    }
}
