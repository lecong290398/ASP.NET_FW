using System;
using System.Collections.Generic;

namespace AtTempleteWeb_API.Entires
{
    public partial class Wiki
    {
        public Wiki()
        {
            FileWiki = new HashSet<FileWiki>();
        }

        public string Id { get; set; }
        public string WikiCode { get; set; }
        public string WikiName { get; set; }
        public string Description { get; set; }
        public string Fk_Project { get; set; }
        public DateTime AtCreatedDate { get; set; }
        public string AtCreatedBy { get; set; }
        public DateTime AtLastModifiedDate { get; set; }
        public string AtLastModifiedBy { get; set; }
        public byte[] AtRowversion { get; set; }
        public int AtRowStatus { get; set; }
        public bool? IsApproved { get; set; }
        public string WikiNameDemo { get; set; }
        public string DescriptionDemo { get; set; }
        public bool? IsApprovedDemo { get; set; }
        public string FK_AccountApproved { get; set; }
        public DateTime? ApprovedDate { get; set; }

        public virtual Project Fk_ProjectNavigation { get; set; }
        public virtual ICollection<FileWiki> FileWiki { get; set; }
    }
}
