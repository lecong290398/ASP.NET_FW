using System;
using System.Collections.Generic;

namespace AtTempleteWeb_API.Entires
{
    public partial class FileAttachment
    {
        public string AttachmentID { get; set; }
        public string RefID { get; set; }
        public string FileName { get; set; }
        public int? FileSize { get; set; }
        public string FileExtension { get; set; }
        public string FileMIMEType { get; set; }
        public byte[] FileContent { get; set; }
        public string DocumentName { get; set; }
        public string Description { get; set; }
        public int? SortOrder { get; set; }
        public byte[] EditVersion { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
