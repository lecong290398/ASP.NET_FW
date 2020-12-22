using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public enum Notify
    {

        NotFound,
        Conectimeout,
        PhienGiaoDichHetHan,
        //Insert
        InsertFail,
        InsertCompelete,
       
        //Update
        UpdateFail,
        UpdateCompelete,

        //Load data
        ReadFail,
        ReadCompelete,

        //Delete
        DeleteComplete,

        //Upload file
        UploadFileFail,
        UploadFileComplete,

        //RemoveFile
        RemoveFileComplete,
        RemoveFileFail,
    }
}
