using System;
using System.Collections.Generic;

namespace FW_MVC_API.Models
{
    public partial class LoaiYeuCauQuanTrong
    {
        public string FkLoai { get; set; }
        public string FkNguoiDung { get; set; }

        public virtual LoaiYeuCau FkLoaiNavigation { get; set; }
        public virtual AccountObject FkNguoiDungNavigation { get; set; }
    }
}
