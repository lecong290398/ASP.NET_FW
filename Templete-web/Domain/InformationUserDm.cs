using System;
using System.Collections.Generic;
using System.Text;
using static Domain.FileAttachmentDm;

namespace Domain
{
    public class InformationUserDm
    {
        public class InformationUserDmOutput
        {
            public string Id { get; set; }
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
        }

        public class InformationUserDmInput
        {
            public string Id { get; set; }
            public string Fk_AccountObject { get; set; }
            public string FistName { get; set; }
            public string LastName { get; set; }
            public int Loai { get; set; }
            public int Kieu { get; set; }
            public decimal? DiemSo { get; set; }
            public DateTime? NgaySinh { get; set; }
            public bool IsInactive { get; set; }
            public byte[] RowVesion { get; set; }

            //Đặc biệt với các model có chức năng upload file
            public List<string> listFileIds { get; set; }
            public List<string> listFileNames { get; set; }
            public List<FileAttachmentDmInput> ListFileAttchment { get; set; }
            public List<string> ListFileRemove { get; set; }

        }
        public class InformationUserDmInput_Delete
        {
            public string Id { get; set; }
            public byte[] RowVesion { get; set; }
        }

    }
}
