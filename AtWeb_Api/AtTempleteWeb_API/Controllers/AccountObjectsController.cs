using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AtTempleteWeb_API.Context;
using AtTempleteWeb_API.Entires;
using AtDomain;
using static AtDomain.AccountObjectDm;
using AtTempleteWeb_API.AtLogic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AutoMapper;

namespace AtTempleteWeb_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountObjectsController : AtBaseApiController
    {
        private readonly AtTempleteWebContext _context;
        private readonly AtAccountObjectLogic _logicAccountObj;
        private readonly AtRoleLogic _logicRole;
        private readonly AtDepartmentLogic _logicDepartment;
        private static readonly IMapper _mapper;

        public AccountObjectsController(AtTempleteWebContext context, AtAccountObjectLogic logicAccountObj
            , AtRoleLogic logicRole, AtDepartmentLogic logicDepartment)
        {
            _context = context;
            _logicAccountObj = logicAccountObj;
            _logicRole = logicRole;
            _logicDepartment = logicDepartment;
        }

        static AccountObjectsController()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AccountObject, AccountObjectDmListOutput>();
            });
            _mapper = config.CreateMapper();
        }

        /// <summary>
        /// Load danh sách AccountObject
        /// </summary>
        /// <returns></returns>
        // GET: api/AccountObjects
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("danh-sach-tai-khoan")]
        public async Task<ActionResult<AtResult<List<AccountObjectDmListOutput>>>> GetAccountObject([FromQuery]int pageNumber = 1)
        {
            if (await CheckPermission(_context))
            {
                // Cho nay chinh sua lai ham phai tra ve total count, ve chi tra ve du lieu cua 1 page tuong ung
                var tupleAcccount = await _logicAccountObj.LoadListAccountObject(UserId, pageNumber);

                return new AtResult<List<AccountObjectDmListOutput>>(tupleAcccount.Item1) { TotalCount = tupleAcccount.Item2 ,PageSize = tupleAcccount.Item3};
            }
            else
            {
                return new AtResult<List<AccountObjectDmListOutput>>(AtNotify.KhongCoQuyenTruyCap);

            }
        }

        /// <summary>
        /// Tải dữ liệu tài khoản dùng đề EDIT
        /// </summary>
        /// <param name="idAccount"></param>
        /// <returns></returns>
        // GET: api/AccountObjects/chinh-sua-tai-khoan/?idAccount=5
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("tai-sua-tai-khoan-edit")]
        public async Task<ActionResult<AtResult<AccountObjectDmOuput_EditOrDetail>>> GetAccountObject_Edit(string idAccount)
        {

            if (await CheckPermission(_context))
            {
                try
                {
                    var accountObject = await _logicAccountObj.GetAccountObject_Edit(idAccount, UserId);
                    if (accountObject == null)
                    {
                        return new AtResult<AccountObjectDmOuput_EditOrDetail>(AtNotify.NotFound);
                    }

                    var listRole = await _logicRole.GetListCombobox_RoleAsyns();
                    var listDepartment = await _logicDepartment.GetListCombobox_DepartmentAsyns();
                    accountObject.ListDepartment = new List<AtDepartmentDm.AtDepartmentDmComboboxOutput>(listDepartment);
                    accountObject.ListRole = new List<AtRoleDm.AtRoleDmComboboxOutput>(listRole);
                    return new AtResult<AccountObjectDmOuput_EditOrDetail>(accountObject);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                return new AtResult<AccountObjectDmOuput_EditOrDetail>(AtNotify.KhongCoQuyenTruyCap);
            }
        }


        /// <summary>
        /// Tải dữ liệu tài khoản DETAIL
        /// </summary>
        /// <param name="idAccount"></param>
        /// <returns></returns>
        // GET: api/AccountObjects/tai-du-lieu-tai-khoan/?idAccount=5
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        [HttpGet("tai-du-lieu-tai-khoan-detail")]
        public async Task<ActionResult<AtResult<AccountObjectDmOuput_EditOrDetail>>> GetAccountObject_Detail(string idAccount)
        {
            if (await CheckPermission(_context))
            {

                try
                {
                    var accountObject = await _logicAccountObj.GetAccountObject_Edit(idAccount, UserId);
                    if (accountObject == null)
                    {
                        return new AtResult<AccountObjectDmOuput_EditOrDetail>(AtNotify.NotFound);
                    }

                    var listRole = await _logicRole.GetListCombobox_RoleAsyns();
                    var listDepartment = await _logicDepartment.GetListCombobox_DepartmentAsyns();

                    accountObject.ListRole = new List<AtRoleDm.AtRoleDmComboboxOutput>();
                    accountObject.ListDepartment = new List<AtDepartmentDm.AtDepartmentDmComboboxOutput>();

                    foreach (var item in accountObject.ListIdRole)
                    {
                        var role = listRole.FirstOrDefault(c => c.Id == item);

                        if (role != null)
                        {
                            accountObject.ListRole.Add(role);
                        }

                    }

                    foreach (var itemD in accountObject.ListIdPhongBan)
                    {
                        var phongBan = listDepartment.FirstOrDefault(c => c.Id == itemD);
                        if (phongBan != null)
                        {
                            accountObject.ListDepartment.Add(phongBan);
                        }
                    }

                    return new AtResult<AccountObjectDmOuput_EditOrDetail>(accountObject);
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            else
            {
                return new AtResult<AccountObjectDmOuput_EditOrDetail>(AtNotify.KhongCoQuyenTruyCap);
            }

        }

        /// <summary>
        /// load dữ liệu cá nhân
        /// </summary>
        /// <param name="idAccount"></param>
        /// <returns></returns>
        // GET: api/AccountObjects/load-du-lieu-tai-khoan-ca-nhan/?idAccount=5
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        [HttpGet("load-du-lieu-tai-khoan-ca-nhan")]
        public async Task<ActionResult<AtResult<AccountObjectDmOuput_EditOrDetail>>> LayThongTinCaNhan(string idAccount)
        {


            try
            {
                var accountObject = await _logicAccountObj.GetAccountObject_Edit(idAccount, UserId);
                if (accountObject == null)
                {
                    return new AtResult<AccountObjectDmOuput_EditOrDetail>(AtNotify.NotFound);
                }

                var listRole = await _logicRole.GetListCombobox_RoleAsyns();
                var listDepartment = await _logicDepartment.GetListCombobox_DepartmentAsyns();

                accountObject.ListRole = new List<AtRoleDm.AtRoleDmComboboxOutput>();
                accountObject.ListDepartment = new List<AtDepartmentDm.AtDepartmentDmComboboxOutput>();

                foreach (var item in accountObject.ListIdRole)
                {
                    var role = listRole.FirstOrDefault(c => c.Id == item);

                    if (role != null)
                    {
                        accountObject.ListRole.Add(role);
                    }

                }

                foreach (var itemD in accountObject.ListIdPhongBan)
                {
                    var phongBan = listDepartment.FirstOrDefault(c => c.Id == itemD);
                    if (phongBan != null)
                    {
                        accountObject.ListDepartment.Add(phongBan);
                    }
                }

                return new AtResult<AccountObjectDmOuput_EditOrDetail>(accountObject);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        /// <summary>
        /// Thêm tài khoản mới
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("them-moi-tai-khoan")]
        public async Task<ActionResult<AtResult<AccountObjectDmListOutput>>> AccountObject_Create([FromBody] AccountObjectDmInput_Create input)
        {
            if (await CheckPermission(_context))
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        return new AtResult<AccountObjectDmListOutput>(AtNotify.InsertFail);
                    }
                    var account = new AccountObject
                    {
                        Id = input.UserName,
                        AccountCode = input.UserName,
                        UserName = input.UserName,
                        AccountObjectName = input.AccountObjectName,
                        PassWord = input.PassWord,
                        Phone = input.Phone,
                        Email = input.Email,
                        AtCreatedBy = UserId,
                        AtRowStatus = (int)AtRowStatus.Normal

                    };
                    var ouput = await _logicAccountObj.CreateAccountObject(account, input.ListIdDepartment, input.ListIdRole, UserId);

                    if (ouput.Item2 == AtNotify.DuplicateCode)
                    {
                        return new AtResult<AccountObjectDmListOutput>(ouput.Item2);
                    }
                    else
                    {
                        var mapper = _mapper.Map<AccountObjectDmListOutput>(ouput.Item1);
                        return new AtResult<AccountObjectDmListOutput>(mapper);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                return new AtResult<AccountObjectDmListOutput>(AtNotify.KhongCoQuyenTruyCap);
            }

        }

        /// <summary>
        /// Chỉnh sửa tài khoản
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        [HttpPost("chinh-sua-tai-khoan")]
        public async Task<ActionResult<AtResult<AccountObjectDmInput_Edit>>> AccountObject_Edit([FromBody]   AccountObjectDmInput_Edit input)
        {

            if (await CheckPermission(_context))
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        return new AtResult<AccountObjectDmInput_Edit>(AtNotify.UpdateFail);
                    }
                    var ouput = await _logicAccountObj.EditAccountObject(input, UserId);

                    if (ouput == AtNotify.PhienGiaoDichHetHan || ouput == AtNotify.NotFound)
                    {
                        return new AtResult<AccountObjectDmInput_Edit>(ouput);
                    }

                    return new AtResult<AccountObjectDmInput_Edit>(input);
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            else
            {
                return new AtResult<AccountObjectDmInput_Edit>(AtNotify.KhongCoQuyenTruyCap);
            }


        }


        /// <summary>
        /// Đặt lại mật khẩu
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        [HttpPost("reset-password")]
        public async Task<ActionResult<AtResult<AccountObjectDm_ResetPassword>>> AccountObject_ResetPassword([FromBody]   AccountObjectDm_ResetPassword input)
        {

            if (await CheckPermission(_context))
            {
                try
                {
                    var ouput = await _logicAccountObj.ResetPasswordAccountObject(input, UserId);

                    if (ouput == AtNotify.PhienGiaoDichHetHan || ouput == AtNotify.NotFound)
                    {
                        return new AtResult<AccountObjectDm_ResetPassword>(ouput);
                    }

                    return new AtResult<AccountObjectDm_ResetPassword>(input);
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            else
            {
                return new AtResult<AccountObjectDm_ResetPassword>(AtNotify.KhongCoQuyenTruyCap);
            }

        }
        /// <summary>
        /// Chỉnh sửa hồ sơ cá nhân
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        [HttpPost("chinh-sua-thong-tin-ca-nhan")]
        public async Task<ActionResult<AtResult<AccountObjectDm_UpdateAccount>>> AccountObject_Update([FromBody]   AccountObjectDm_UpdateAccount input)
        {

            if (await CheckPermission(_context))
            {
                try
                {
                    var ouput = await _logicAccountObj.UpdateHoSoCaNhan(input, UserId);

                    if (ouput == AtNotify.PhienGiaoDichHetHan || ouput == AtNotify.NotFound)
                    {
                        return new AtResult<AccountObjectDm_UpdateAccount>(ouput);
                    }

                    return new AtResult<AccountObjectDm_UpdateAccount>(input);
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            else
            {
                return new AtResult<AccountObjectDm_UpdateAccount>(AtNotify.KhongCoQuyenTruyCap);
            }

        }

        /// <summary>
        /// Chỉnh sửa mật khẩu cá nhân
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        [HttpPost("doi-mat-khau-ca-nhan")]
        public async Task<ActionResult<AtResult<AccountObjectDm_UpdatePass>>> AccountObject_UpdatePass([FromBody]   AccountObjectDm_UpdatePass input)
        {

            if (await CheckPermission(_context))
            {
                try
                {
                    var ouput = await _logicAccountObj.DoiMatKhauCaNhan(input, UserId);

                    if (ouput == AtNotify.PhienGiaoDichHetHan || ouput == AtNotify.NotFound)
                    {
                        return new AtResult<AccountObjectDm_UpdatePass>(ouput);
                    }

                    return new AtResult<AccountObjectDm_UpdatePass>(input);
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            else
            {
                return new AtResult<AccountObjectDm_UpdatePass>(AtNotify.KhongCoQuyenTruyCap);
            }

        }


        /// <summary>
        /// Xóa tài khoản
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("xoa-tai-khoan")]
        public async Task<ActionResult<AtResult<AccountObjectDm_Delete>>> AccountObject_Delete([FromBody] AccountObjectDm_Delete input)
        {


            if (await CheckPermission(_context))
            {
                if (input == null)
                {
                    return new AtResult<AccountObjectDm_Delete>(AtNotify.NotFound);
                }

                try
                {

                    var output = await _logicAccountObj.AccountObject_Delete(input, UserId);
                    if (output == AtNotify.PhienGiaoDichHetHan || (output == AtNotify.DeleteFail))
                    {
                        return new AtResult<AccountObjectDm_Delete>(output);
                    }
                    else
                    {
                        return new AtResult<AccountObjectDm_Delete>(input);
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            else
            {
                return new AtResult<AccountObjectDm_Delete>(AtNotify.KhongCoQuyenTruyCap);
            }
        }
    }
}
