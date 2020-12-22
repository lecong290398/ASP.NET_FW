using System;
using System.Collections.Generic;
using System.Text;

namespace AtDomain
{
    public class AtFileAttachmentDm
    {
        public class FileAttachmentDmInput
        {
            public string AttachmentID { get; set; }
            public string RefID { get; set; }
            public string FileName { get; set; }
            public string CreatedBy { get; set; }
        }
    }
}
