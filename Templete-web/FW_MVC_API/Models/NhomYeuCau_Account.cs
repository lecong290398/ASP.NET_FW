﻿using System;
using System.Collections.Generic;

namespace FW_MVC_API.Models
{
    public partial class NhomYeuCau_Account
    {
        public string FK_AccountObject { get; set; }
        public string FK_NhomYeuCau { get; set; }
        public DateTime AtCreatedDate { get; set; }
        public string AtCreatedBy { get; set; }
        public DateTime AtLastModifiedDate { get; set; }
        public string AtLastModifiedBy { get; set; }
        public byte[] AtRowversion { get; set; }
        public int AtRowStatus { get; set; }

        public virtual AccountObject FK_AccountObjectNavigation { get; set; }
        public virtual NhomYeuCau FK_NhomYeuCauNavigation { get; set; }
    }
}
