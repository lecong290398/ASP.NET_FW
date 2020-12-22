using AtDomain;
using AtTempleteWeb_API.Context;
using AtTempleteWeb_API.Entires;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AtTempleteWeb_API.AtLogic
{
    public class AtBaseLogic
    {
        protected readonly AtTempleteWebContext _context;
        protected readonly int _PageSize;
        public readonly IConfiguration _config;
        public AtBaseLogic(AtTempleteWebContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
            _PageSize = Convert.ToInt32(_config["PageSize"]);
        }


        /// <summary>
        /// Lấy giờ hiện tại trên server
        /// </summary>
        /// <returns>DateTime</returns>
        public DateTime GetDateTimeFromServer()
        {
            var resultParameter = new SqlParameter("@result", SqlDbType.DateTime);
            resultParameter.Size = 50;
            resultParameter.Direction = ParameterDirection.Output;
            _context.Database.ExecuteSqlCommand("set @result =  GETDATE();", resultParameter);
            return Convert.ToDateTime(resultParameter.Value);
        }

        /// <summary>
        /// Gen code tự động
        /// </summary>
        /// <param name="type"></param>
        /// <returns> Code </returns>
        public async Task<string> GetSerialCode(string type)
        {
            string code = type.ToString();
            var mscTableCode = await _context.MSC_TableCode.FirstOrDefaultAsync(h => h.TableCode == type.ToString());
            if (mscTableCode == null)
            {
                code = "NONE";
            }
            else
            {
                // Tăng giá trị lên 1
                mscTableCode.CurrentValue++;
            }
            code = mscTableCode.Prefix;
            code += mscTableCode.CurrentValue.ToString().PadLeft((int)mscTableCode.Lenght, '0');
            await _context.SaveChangesAsync();
            return code;
        }

        /// <summary>
        /// Upload file 
        /// </summary>
        /// <param name="refID"> ID sở hữu file</param>
        /// <param name="listFileIds"> List Id của FileUpload </param>
        /// <param name="listFileNames"> List Name của FileUpload </param>
        /// <param name="createBy">  Người tạo </param>
        /// <returns> Danh sách file đính kèm</returns>
        public async Task<List<FileAttachment>> UploadFileLogic(string refID, List<string> listFileIds, List<string> listFileNames, string createBy)
        {
            string sourcePath = System.IO.Path.Combine(_config["TempUploadFoler"]);

            var listFileAttachment = new List<FileAttachment>();
            for (int i = 0; i < listFileIds.Count; i++)
            {
                string targetPath = System.IO.Path.Combine(_config["UploadFoler"], refID);

                if (!System.IO.Directory.Exists(targetPath))
                {
                    System.IO.Directory.CreateDirectory(targetPath);
                }

                var sourceFile = System.IO.Path.Combine(sourcePath, listFileIds[i] + "_" + listFileNames[i]);
                var destFile = System.IO.Path.Combine(targetPath, listFileIds[i] + "_" + listFileNames[i]);

                //Copy file 
                if (!System.IO.Directory.Exists(sourceFile))
                {
                    System.IO.File.Copy(sourceFile, destFile, true);
                }

                var stream = System.IO.File.OpenRead(sourceFile);

                var attachFiles = new FileAttachment
                {
                    AttachmentID = listFileIds[i],
                    FileName = listFileNames[i],
                    RefID = refID,
                    CreatedBy = createBy,
                    CreatedDate = GetDateTimeFromServer()
                };

                await _context.FileAttachment.AddAsync(attachFiles);
                listFileAttachment.Add(attachFiles);
            }

            return listFileAttachment;
        }


        /// <summary>
        /// Dowload FileUpload
        /// </summary>
        /// <param name="idFile"></param>
        /// <param name="RefId"></param>
        /// <returns></returns>
        public async Task<Tuple<AtNotify, string, FileAttachment>> DowloadFileLogic(string idFile)
        {
            var dbFile = await _context.FileAttachment.AsNoTracking().FirstOrDefaultAsync(h => h.AttachmentID == idFile).ConfigureAwait(false);

            if (dbFile == null)
            {
                return new Tuple<AtNotify, string, FileAttachment>(AtNotify.UploadFileFail, null, null);
            }

            var path = System.IO.Path.Combine(_config["UploadFoler"], dbFile.RefID, dbFile.AttachmentID + "_" + dbFile.FileName);

            if (!System.IO.File.Exists(path))
            {
                return new Tuple<AtNotify, string, FileAttachment>(AtNotify.UploadFileFail, null, null);
            }

            return new Tuple<AtNotify, string, FileAttachment>(AtNotify.UploadFileComplete, path, dbFile);
        }


        /// <summary>
        /// Xóa FileAttchment 
        /// </summary>
        /// <param name="listFileIdRemove"> Danh sách id file muốn xóa </param>
        /// <param name="refId"> Id sở hữu file </param>
        /// <returns> Notify </returns>
        public async Task<AtNotify> RemoveFileLogic(List<string> listFileIdRemove, string refId)
        {
            if (listFileIdRemove.Count() < 0 || string.IsNullOrEmpty(refId))
            {
                return AtNotify.RemoveFileFail;
            }
            foreach (var idFile in listFileIdRemove)
            {
                var fileAttachment = await _context.FileAttachment.FirstOrDefaultAsync(c => c.AttachmentID == idFile && c.RefID == refId);
                _context.FileAttachment.Remove(fileAttachment);
            }

            return AtNotify.RemoveFileComplete;
        }

        public async Task<AtNotify> WrtiteAudittingLog( MSC_AudittingLog input ,string  SeriCode)
        {
            try
            {
                if (input == null || string.IsNullOrEmpty(SeriCode))
                {
                    return AtNotify.InsertFail;
                }

                input.LoginName = input.UserID;
                input.Time = GetDateTimeFromServer();
                input.EventID = await GetSerialCode(SeriCode);
                await _context.MSC_AudittingLog.AddAsync(input);
                await _context.SaveChangesAsync();
                return AtNotify.InsertCompelete;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
