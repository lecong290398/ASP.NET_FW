using System;
using System.Collections.Generic;

namespace AtTempleteWeb_API.Entires
{
    public partial class InfomationUser
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Fk_AccountObject { get; set; }
        public string FistName { get; set; }
        public string LastName { get; set; }
        public int Loai { get; set; }
        public int Kieu { get; set; }
        public bool IsInactive { get; set; }
        public decimal? DiemSo { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateDate { get; set; }
        public byte[] RowVesion { get; set; }

        public virtual AccountObject Fk_AccountObjectNavigation { get; set; }
    }
}
