using System;
using System.Collections.Generic;
using System.Text;

namespace AtDomain
{
    public enum AtNotify
    {

        NotFound,
        PhienGiaoDichHetHan,
        Conectimeout,
       
        //Insert
        InsertFail,
        InsertCompelete,
        DuplicateCode,

        //Update
        UpdateFail,
        UpdateCompelete,

        //Load data
        ReadFail,
        ReadCompelete,

        //Delete
        DeleteComplete,
        DeleteFail,

        //Upload file
        UploadFileFail,
        UploadFileComplete,

        //RemoveFile
        RemoveFileComplete,
        RemoveFileFail,

        //Login
        LoginFail,
        RefreshTokenFail,
        //Quyền
        KhongCoQuyenTruyCap,
        HetHanDangNhap,
        GetComplete,

        //Token
        ResetTokenFail
    }
}
