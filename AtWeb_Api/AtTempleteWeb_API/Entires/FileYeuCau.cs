﻿using System;
using System.Collections.Generic;

namespace AtTempleteWeb_API.Entires
{
    public partial class FileYeuCau
    {
        public string Id { get; set; }
        public string FkYeuCau { get; set; }
        public string Ten { get; set; }
        public string Loai { get; set; }
        public int KichThuoc { get; set; }
        public DateTime AtCreatedDate { get; set; }
        public string AtCreatedBy { get; set; }
        public DateTime AtLastModifiedDate { get; set; }
        public string AtLastModifiedBy { get; set; }
        public byte[] AtRowversion { get; set; }
        public int AtRowStatus { get; set; }

        public virtual YeuCau FkYeuCauNavigation { get; set; }
    }
}
