using AtDomain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AtTempleteWeb.Controllers
{
    public partial class UploadController : AtBaseController
    {
        

        public UploadController(IConfiguration config) : base(config)
        {

        }

        public ActionResult AsyncUpload()
        {
            return View();
        }

        public async Task<ActionResult> Async_Save(IEnumerable<IFormFile> files, List<string> fileIds)
        {
            // The Name of the Upload component is "files"
            if (files != null)
            {
                //kiểm tra thư mục tồn tại
                if (!System.IO.Directory.Exists(_config["TempUploadFoler"]))
                {
                    System.IO.Directory.CreateDirectory(_config["TempUploadFoler"]);
                }

                var indexFileIds = -1;
                foreach (var file in files)
                {
                    indexFileIds++;
                    var fileId = fileIds[indexFileIds];

                    var fileContent = ContentDispositionHeaderValue.Parse(file.ContentDisposition);

                    // Some browsers send file names with full path.
                    // We are only interested in the file name.
                    var fileName = Path.GetFileName(fileContent.FileName.ToString().Trim('"'));
                    var physicalPath = Path.Combine(_config["TempUploadFoler"], fileId + "_" + fileName);

                    // The files are not actually saved in this demo
                    using (var fileStream = new FileStream(physicalPath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }
            }

            // Return an empty string to signify success
            return Content("");
        }

        public ActionResult Async_Remove(string[] fileNames, List<string> fileIds)
        {
            // The parameter of the Remove action must be called "fileNames"

            if (fileNames != null)
            {
                var indexFileIds = -1;
                foreach (var fullName in fileNames)
                {
                    indexFileIds++;
                    var fileId = fileIds[indexFileIds];
                    var fileName = Path.GetFileName(fullName);
                    var physicalPath = Path.Combine(_config["TempUploadFoler"], fileId + "_" + fileName);

                    // TODO: Verify user permissions

                    if (System.IO.File.Exists(physicalPath))
                    {
                        // The files are not actually removed in this demo
                        System.IO.File.Delete(physicalPath);
                    }
                }
            }

            // Return an empty string to signify success
            return Content("");
        }

        public async Task<ActionResult> Async_SaveEdit(IEnumerable<IFormFile> files_edit, List<string> fileIds)
        {
            // The Name of the Upload component is "files"
            if (files_edit != null)
            {
                //kiểm tra thư mục tồn tại
                if (!System.IO.Directory.Exists(_config["TempUploadFoler"]))
                {
                    System.IO.Directory.CreateDirectory(_config["TempUploadFoler"]);
                }

                var indexFileIds = -1;
                foreach (var file in files_edit)
                {
                    indexFileIds++;
                    var fileId = fileIds[indexFileIds];

                    var fileContent = ContentDispositionHeaderValue.Parse(file.ContentDisposition);

                    // Some browsers send file names with full path.
                    // We are only interested in the file name.
                    var fileName = Path.GetFileName(fileContent.FileName.ToString().Trim('"'));
                    var physicalPath = Path.Combine(_config["TempUploadFoler"], fileId + "_" + fileName);

                    // The files are not actually saved in this demo
                    using (var fileStream = new FileStream(physicalPath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }
            }

            // Return an empty string to signify success
            return Content("");
        }

        public ActionResult Async_RemoveEdit(string[] fileNames, List<string> fileIds)
        {
            // The parameter of the Remove action must be called "fileNames"

            if (fileNames != null)
            {
                var indexFileIds = -1;
                foreach (var fullName in fileNames)
                {
                    indexFileIds++;
                    var fileId = fileIds[indexFileIds];
                    var fileName = Path.GetFileName(fullName);
                    var physicalPath = Path.Combine(_config["TempUploadFoler"], fileId + "_" + fileName);

                    // TODO: Verify user permissions

                    if (System.IO.File.Exists(physicalPath))
                    {
                        // The files are not actually removed in this demo
                        System.IO.File.Delete(physicalPath);
                    }
                }
            }

            // Return an empty string to signify success
            return Content("");
        }

    }
}