using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class FileAttachmentDm
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
