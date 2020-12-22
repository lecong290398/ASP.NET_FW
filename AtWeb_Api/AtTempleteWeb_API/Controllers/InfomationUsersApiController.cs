using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AtDomain;
using AtTempleteWeb_API.AtLogic;
using AtTempleteWeb_API.Context;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using static AtDomain.AccountObjectDm;
using static AtDomain.InformationUserDm;

namespace AtTempleteWeb_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class InfomationUsersAPIController : AtBaseApiController
    {

        private static AtInformationUserLogic _logicInformation;
        private static AtAccountObjectLogic _logicAccountObject;
        private static AtBaseLogic _LogicATLogicBaseHelper;
        private static AtTempleteWebContext _context;

        public InfomationUsersAPIController(AtTempleteWebContext context, AtInformationUserLogic logicInformation
          , AtAccountObjectLogic logicAccountObject, AtBaseLogic LogicATLogicBaseHelper)
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("load-list-information")]
        public async Task<ActionResult<AtResult<List<InformationUserDmOutput>>>> GetListInformation(int indexPage)
        {
            if (await CheckPermission(_context))
            {

                var listInfromation = await _logicInformation.GetListInfromatiomUserAsync(indexPage);
                return new AtResult<List<InformationUserDmOutput>>(listInfromation);
            }
            else
            {
                return new AtResult<List<InformationUserDmOutput>>(AtNotify.KhongCoQuyenTruyCap);
            }
        }

        /// <summary>
        /// Load danh sách List AccountObject
        /// </summary>
        /// <returns> List AccountObjectDmOuput</returns>
        /// POST: api/InfomationUsersAPI/load-list-account-object
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("load-list-account-object")]
        public async Task<ActionResult<AtResult<List<AccountObjectDmOutput>>>> ListCombobox_AccountObjectAsyns()
        {

            if (await CheckPermission(_context))
            {

                var list = await _logicAccountObject.GetListCombobox_AccountObjectAsyns();
                return new AtResult<List<AccountObjectDmOutput>>(list);
            }
            else
            {
                return new AtResult<List<AccountObjectDmOutput>>(AtNotify.KhongCoQuyenTruyCap);
            }

        }

        /// <summary>
        /// Load danh sách loại tài khoản.
        /// </summary>
        /// <returns>List TypeAccount</returns>
        /// POST: api/InfomationUsersAPI/load-list-type
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("load-list-type")]
        public async Task<ActionResult<AtResult<List<TypeAccount>>>> ListCombobox_TypeAccountObjectAsyns()
        {
            var list = new List<TypeAccount>();
            list.Add(new TypeAccount
            {
                Id = AtEnumAccountType.UuTienCao,
                Name = "Ưu Tiên Cao",
            });

            list.Add(new TypeAccount
            {
                Id = AtEnumAccountType.UuTienThap,
                Name = "Ưu Tiên Thấp",
            });

            return new AtResult<List<TypeAccount>>(list);
        }

        /// <summary>
        /// Load danh sách kiểu tài khoản
        /// </summary>
        /// <returns> List StypeAccount</returns>
        /// POST: api/InfomationUsersAPI/load-list-stype
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("get-information-with-id")]
        public async Task<ActionResult<AtResult<InformationUserDmInput>>> GetInfomationUser(string idInformation)
        {

            if (await CheckPermission(_context))
            {

                var model = await _logicInformation.GetInfromatiomUserAsync(idInformation);

                if (model == null)
                {
                    return new AtResult<InformationUserDmInput>(AtNotify.NotFound);
                }

                return new AtResult<InformationUserDmInput>(model);
            }
            else
            {
                return new AtResult<InformationUserDmInput>(AtNotify.KhongCoQuyenTruyCap);
            }
        }

        /// <summary>
        /// Tạo mới Information 
        /// </summary>
        /// <param name="input"> InformationUserDmInput </param>
        /// <returns></returns>
        /// POST: api/InfomationUsersAPI/create-information-api
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("create-information-api")]
        public async Task<ActionResult<AtResult<InformationUserDmOutput>>> CreateInformation_API([FromBody]InformationUserDmInput input)
        {

            if (await CheckPermission(_context))
            {

                try
                {
                    var output = await _logicInformation.CreateInfromationUser(input);

                    if (output != null)
                    {
                        return new AtResult<InformationUserDmOutput>(output);
                    }

                    return new AtResult<InformationUserDmOutput>(AtNotify.InsertFail);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                return new AtResult<InformationUserDmOutput>(AtNotify.KhongCoQuyenTruyCap);
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("edit-information-api")]
        public async Task<ActionResult<AtResult<InformationUserDmInput>>> EditInfromation([FromBody]InformationUserDmInput input)
        {
            if (await CheckPermission(_context))
            {
                if (input == null)
                {
                    return new AtResult<InformationUserDmInput>(AtNotify.NotFound);
                }

                var tupperInformation = await _logicInformation.UpdateInfromatiomUserAsync(input);

                if (tupperInformation.Item2 == AtNotify.PhienGiaoDichHetHan)
                {
                    return new AtResult<InformationUserDmInput>(AtNotify.PhienGiaoDichHetHan);
                }
                else if (tupperInformation.Item2 == AtNotify.NotFound)
                {
                    return new AtResult<InformationUserDmInput>(AtNotify.NotFound);
                }
                else
                {
                    return new AtResult<InformationUserDmInput>(input);
                }
            }
            else
            {
                return new AtResult<InformationUserDmInput>(AtNotify.KhongCoQuyenTruyCap);
            }
        }

        /// <summary>
        /// Hàm xóa information 
        /// </summary>
        /// <param name="input_Delete">InformationUserDmInput_Delete</param>
        /// <returns>InformationUserDmInput_Delete</returns>
        /// POST: api/InfomationUsersAPI/delete-informatio-api
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("delete-informatio-api")]
        public async Task<ActionResult<AtResult<InformationUserDmInput_Delete>>> DeleteInfromationUser_CallAjax([FromBody]InformationUserDmInput_Delete inputData)
        {
            if (await CheckPermission(_context))
            {
                if (inputData != null)
                {
                    var notify = await _logicInformation.DeleteInfromationUser(inputData);
                    if (notify == AtNotify.PhienGiaoDichHetHan)
                    {

                        return new AtResult<InformationUserDmInput_Delete>(AtNotify.PhienGiaoDichHetHan);
                    }
                    return new AtResult<InformationUserDmInput_Delete>(inputData);
                }
                else
                {
                    return new AtResult<InformationUserDmInput_Delete>(AtNotify.NotFound);
                }
            }
            else
            {
                return new AtResult<InformationUserDmInput_Delete>(AtNotify.KhongCoQuyenTruyCap);
            }

        }
    }
}