using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FW_MVC_API.Context;
using FW_MVC_API.Models;
using FW_MVC_API.AtLogic;
using Domain;
using static Domain.InformationUserDm;
using static Domain.AccountObjectDm;
using Microsoft.AspNetCore.Authorization;
using MimeKit;
using FW_MVC_API.Helper;
using static Domain.FileAttachmentDm;

namespace FW_MVC_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfomationUsersAPIController : AtBaseApiController
    {
        private readonly DataFrameworkWebContext _context;
        private static AtInformationUserLogic _logicInformation;
        private static AtAccountObjectLogic _logicAccountObject;
        private static ATLogicBaseHelper _LogicATLogicBaseHelper;
        public InfomationUsersAPIController(DataFrameworkWebContext context, AtInformationUserLogic logicInformation
            , AtAccountObjectLogic logicAccountObject, ATLogicBaseHelper LogicATLogicBaseHelper)
        {
            _context = context;
            _logicInformation = logicInformation;
            _logicAccountObject = logicAccountObject;
            _LogicATLogicBaseHelper = LogicATLogicBaseHelper;
        }

        /// <summary>
        /// Load danh sách InformationUser
        /// </summary>
        /// <returns>Danh sách đối tượng InformationUserDmOutput </returns>
        /// GET : api/InfomationUsersAPI/load-list-information
        [HttpGet("load-list-information")]
        public async Task<ActionResult<AtResult<List<InformationUserDmOutput>>>> GetListInformation()
        {
            var listInfromation = await _logicInformation.GetListInfromatiomUserAsync();
            return new AtResult<List<InformationUserDmOutput>>(listInfromation);
        }

        /// <summary>
        /// Load danh sách List AccountObject
        /// </summary>
        /// <returns> List AccountObjectDmOuput</returns>
        /// POST: api/InfomationUsersAPI/load-list-account-object
        [HttpPost("load-list-account-object")]
        public async Task<ActionResult<AtResult<List<AccountObjectDmOuput>>>> ListCombobox_AccountObjectAsyns()
        {
            return new AtResult<List<AccountObjectDmOuput>>(await _logicAccountObject.GetListCombobox_AccountObjectAsyns());
        }

        /// <summary>
        /// Load danh sách loại tài khoản.
        /// </summary>
        /// <returns>List TypeAccount</returns>
        /// POST: api/InfomationUsersAPI/load-list-type
        [HttpPost("load-list-type")]
        public async Task<ActionResult<AtResult<List<TypeAccount>>>> ListCombobox_TypeAccountObjectAsyns()
        {
            var list = new List<TypeAccount>();
            list.Add(new TypeAccount
            {
                Id = EnumAccountType.UuTienCao,
                Name = "Ưu Tiên Cao",
            });

            list.Add(new TypeAccount
            {
                Id = EnumAccountType.UuTienThap,
                Name = "Ưu Tiên Thấp",
            });

            return new AtResult<List<TypeAccount>>(list);
        }

        /// <summary>
        /// Load danh sách kiểu tài khoản
        /// </summary>
        /// <returns> List StypeAccount</returns>
        /// POST: api/InfomationUsersAPI/load-list-stype
        [HttpPost("load-list-stype")]
        public async Task<ActionResult<AtResult<List<StypeAccount>>>> ListCombobox_StypeAccountObjectAsyns()
        {
            var list = new List<StypeAccount>();
            list.Add(new StypeAccount
            {
                Id = 1,
                Name = "Vip Bạc",
            });

            list.Add(new StypeAccount
            {
                Id = 2,
                Name = "Vip Vàng",
            });

            list.Add(new StypeAccount
            {
                Id = 2,
                Name = "Vip Bạch Kim",
            });
            return new AtResult<List<StypeAccount>>(list);
        }

        /// <summary>
        /// Load dữ liệu Information 
        /// </summary>
        /// <param name="id">string</param>
        /// <returns> InformationUserDmInput </returns>
        /// GET: api/InfomationUsersAPI/get-information-with-id/?idInformation = 5
        [HttpGet("get-information-with-id")]
        public async Task<ActionResult<AtResult<InformationUserDmInput>>> GetInfomationUser(string idInformation)
        {
            if (!string.IsNullOrEmpty(idInformation))
            {
                return new AtResult<InformationUserDmInput>(await _logicInformation.GetInfromatiomUserAsync(idInformation));
            }
            return new AtResult<InformationUserDmInput>(Notify.NotFound);
        }

        /// <summary>
        /// Tạo mới Information 
        /// </summary>
        /// <param name="input"> InformationUserDmInput </param>
        /// <returns></returns>
        /// POST: api/InfomationUsersAPI/create-information-api
        [HttpPost("create-information-api")]
        public async Task<ActionResult<AtResult<InformationUserDmOutput>>> CreateInformation_API(InformationUserDmInput input)
        {
            try
            {
                var output = await _logicInformation.CreateInfromationUser(input);

                if (output != null)
                {
                    return new AtResult<InformationUserDmOutput>(output);
                }

                return new AtResult<InformationUserDmOutput>(Notify.InsertFail); 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Tải file đính kèm
        /// </summary>
        /// <param name="idFile"></param>
        /// <returns></returns>
        /// api/InfomationUsersAPI/tai-file-dinh-kem/5s7123
        [HttpGet("tai-file-dinh-kem")]
        public async Task<IActionResult> TaiFileDinhKem(string idFile)
        {
            var file = await _LogicATLogicBaseHelper.DowloadFileLogic(idFile);

            var stream = System.IO.File.OpenRead(file.Item2);

            return File(stream, MimeTypes.GetMimeType(file.Item3.AttachmentID + "_" + file.Item3.FileName), file.Item3.AttachmentID + "_" + file.Item3.FileName);
        }

        /// <summary>
        /// Cập nhập dữ liệu information
        /// </summary>
        /// <param name="input">InformationUserDmInput</param>
        /// <returns>Danh sách list Infromation đã được cập nhập</returns>
        /// POST: api/InfomationUsersAPI/edit-information-api
        [HttpPost("edit-information-api")]
        public async Task<ActionResult<AtResult<List<InformationUserDmOutput>>>> EditInfromation([FromBody]InformationUserDmInput input)
        {
            if (input == null)
            {
                return new AtResult<List<InformationUserDmOutput>>(Notify.NotFound);
            }

            var tupperInformation = await _logicInformation.UpdateInfromatiomUserAsync(input);

            if (tupperInformation.Item2 == Notify.PhienGiaoDichHetHan)
            {
                return new AtResult<List<InformationUserDmOutput>>(Notify.PhienGiaoDichHetHan);
            }
            else if (tupperInformation.Item2 == Notify.NotFound)
            {
                return new AtResult<List<InformationUserDmOutput>>(Notify.NotFound);
            }
            else
            {
                return new AtResult<List<InformationUserDmOutput>>(tupperInformation.Item1);
            }
        }

        /// <summary>
        /// Hàm xóa information 
        /// </summary>
        /// <param name="input_Delete">InformationUserDmInput_Delete</param>
        /// <returns>InformationUserDmInput_Delete</returns>
        /// POST: api/InfomationUsersAPI/delete-informatio-api
        [HttpPost("delete-informatio-api")]
        public async Task<ActionResult<AtResult<InformationUserDmInput_Delete>>> DeleteInfromationUser_CallAjax([FromBody]InformationUserDmInput_Delete input_Delete)
        {
            if (input_Delete != null)
            {
                var notify = await _logicInformation.DeleteInfromationUser(input_Delete);
                if (notify == Notify.PhienGiaoDichHetHan)
                {
                    return new AtResult<InformationUserDmInput_Delete>(Notify.PhienGiaoDichHetHan);
                }
                return new AtResult<InformationUserDmInput_Delete>(input_Delete);
            }
            else
            {
                return new AtResult<InformationUserDmInput_Delete>(Notify.NotFound);
            }
        }
    }
}
