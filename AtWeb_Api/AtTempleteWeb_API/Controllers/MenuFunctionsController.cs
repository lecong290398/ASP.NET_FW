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
using static AtDomain.AtMenuFuntionDm;
using AtTempleteWeb_API.AtLogic;
using static AtDomain.AtMenuFunction_RoleDm;
using static AtDomain.AtMenuFunction_AccountDm;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace AtTempleteWeb_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuFunctionsController : AtBaseApiController
    {
        private readonly AtTempleteWebContext _context;
        private readonly AtMenuFuntionLogic _logicMenu;

        public MenuFunctionsController(AtTempleteWebContext context, AtMenuFuntionLogic logicMenu)
        {
            _context = context;
            _logicMenu = logicMenu;
        }

        /// <summary>
        /// Load danh sách MenuFunction
        /// </summary>
        /// <returns></returns>
        // GET: api/MenuFunctions
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        [HttpGet("danh-sach-menu-function")]
        public async Task<ActionResult<AtResult<List<MenuFunctionDmOutput>>>> GetListMenuFunction([FromQuery]int pageNumber = 1)
        {

            if (await CheckPermission(_context))
            {

                var tupleMenu = await _logicMenu.GetListMenuFunctionAsyns(UserId, pageNumber);

                return new AtResult<List<MenuFunctionDmOutput>>(tupleMenu.Item1) { TotalCount = tupleMenu.Item2, PageSize = tupleMenu.Item3 };

            }
            else
            {
                return new AtResult<List<MenuFunctionDmOutput>>(AtNotify.KhongCoQuyenTruyCap);
            }
        }

        /// <summary>
        /// Load dữ liệu Menufunction theo ID
        /// </summary>
        /// <param name="idMenuFunction"></param>
        /// <returns> Tupper Item = MenuFunctionDmEditOutput và Item == AtNotify  trạng thái trả về   </returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        [HttpGet("get-menu-function-edit")]
        public async Task<ActionResult<AtResult<MenuFunctionDmEditOutput>>> GetMenuFunctionEdit(string idMenuFunction)
        {
            if (await CheckPermission(_context))
            {

                var model = await _logicMenu.GetEdit_MenuFunctionAsyns(idMenuFunction, UserId);
                if (model.Item2 == AtNotify.NotFound)
                {
                    return new AtResult<MenuFunctionDmEditOutput>(AtNotify.NotFound);
                }
                return new AtResult<MenuFunctionDmEditOutput>(model.Item1); ;
            }
            else
            {
                return new AtResult<MenuFunctionDmEditOutput>(AtNotify.KhongCoQuyenTruyCap);
            }
           
        }


        /// <summary>
        /// Chỉnh sửa MenuFunction
        /// </summary>
        /// <param name="input">MenuFunctionDmCreatetInputOrEdit</param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("chinh-sua-menu-function")]
        public async Task<ActionResult<AtResult<MenuFunctionDmCreatetInputOrEdit>>> EditMenuFunction(MenuFunctionDmCreatetInputOrEdit input)
        {
            if (await CheckPermission(_context))
            {
                try
                {
                    var output = await _logicMenu.EditMenuFuntionAsyns(input, UserId);
                    if (output == AtNotify.NotFound)
                    {
                        return new AtResult<MenuFunctionDmCreatetInputOrEdit>(AtNotify.NotFound);
                    }
                    return new AtResult<MenuFunctionDmCreatetInputOrEdit>(input);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                return new AtResult<MenuFunctionDmCreatetInputOrEdit>(AtNotify.KhongCoQuyenTruyCap);
            }
            
        }


        /// <summary>
        /// Them mới MenuFunction
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        [HttpPost("them-moi-menu-function")]
        public async Task<ActionResult<AtResult<MenuFunctionDmOutput>>> CreateMenuFunction(MenuFunctionDmCreatetInputOrEdit input)
        {
            if (await CheckPermission(_context))
            {
                try
                {
                    var model = await _logicMenu.CreateMenuFuntionAsyns(input, UserId);
                    return new AtResult<MenuFunctionDmOutput>(model);
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            else
            {
                return new AtResult<MenuFunctionDmOutput>(AtNotify.KhongCoQuyenTruyCap);
            }
        }


        /// <summary>
        /// Delete MenuFuntion
        /// </summary>
        /// <param name="idMenuFunction"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        [HttpPost("delete-menu-function")]
        public async Task<ActionResult<AtResult<string>>> DeleteMenuFunction(string idMenuFunction)
        {
            if (await CheckPermission(_context))
            {
                try
                {
                    var output = await _logicMenu.DeleteMenuFuntionAsyns(idMenuFunction, UserId);
                    if (output == AtNotify.DeleteFail)
                    {
                        return new AtResult<string>(AtNotify.DeleteFail);
                    }
                    else if (output == AtNotify.NotFound)
                    {
                        return new AtResult<string>(AtNotify.NotFound);

                    }
                    return new AtResult<string>(idMenuFunction);
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            else
            {
                return new AtResult<string>(AtNotify.KhongCoQuyenTruyCap);
            }
        }


        /// <summary>
        /// Load danh sách Role theo Id MenuFunction
        /// </summary>
        /// <param name="idMenuFunction"></param>
        /// <returns></returns>
        [HttpGet("danh-sach-role")]
        public async Task<ActionResult<AtResult<List<AtMenuFunction_RoleDm_EditMenuFunction>>>> GetListRoleMenuFuntion(string idMenuFunction)
        {
            if (await CheckPermission(_context))
            {
                var list = await _logicMenu.GetListListRoleWithIdMenuFuntion(idMenuFunction, UserId);
                return new AtResult<List<AtMenuFunction_RoleDm_EditMenuFunction>>(list);
            }
            else
            {
                return new AtResult<List<AtMenuFunction_RoleDm_EditMenuFunction>>(AtNotify.KhongCoQuyenTruyCap);
            }
        }

        /// <summary>
        /// Load Danh sách Account theo Id MenuFunction
        /// </summary>
        /// <param name="idMenuFunction"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("danh-sach-tai-khoan")]
        public async Task<ActionResult<AtResult<List<AtMenuFunction_AccountDmListEditMenuFunction>>>> GetListAccountMenuFuntion(string idMenuFunction)
        {
            if (await CheckPermission(_context))
            {
                var list = await _logicMenu.GetListListAccountWithIdMenuFuntion(idMenuFunction, UserId);
                return new AtResult<List<AtMenuFunction_AccountDmListEditMenuFunction>>(list);
            }
            else
            {
                return new AtResult<List<AtMenuFunction_AccountDmListEditMenuFunction>>(AtNotify.KhongCoQuyenTruyCap);
            }
        }


        /// <summary>
        /// Get danh sách menufunction
        /// </summary>
        /// <param name="idRole"></param>
        /// <returns></returns>

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("menu-funtion-role")]
        public async Task<ActionResult<AtResult<MenuRoleOuput>>> GetListMenuChucNang_Role(string idRole)
        {
            if (await CheckPermission(_context))
            {
                try
                {
                    var MenuFuntion = await _logicMenu.GetListMenuRoleAsync(idRole);
                    return new AtResult<MenuRoleOuput>(MenuFuntion);
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            else
            {
                return new AtResult<MenuRoleOuput>(AtNotify.KhongCoQuyenTruyCap);
            }
        }

        /// <summary>
        /// Lưu Menu theo Role (Phân quyền theo role)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("save-menu-role")]
        public async Task<ActionResult<AtResult<string>>> SaveMenuRole([FromBody]MenuRoleInput input)
        {
            if (await CheckPermission(_context))
            {
                try
                {
                    var output = await _logicMenu.SaveMenuRoleAsync(input, UserId);
                    return new AtResult<string>(output);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                return new AtResult<string>(AtNotify.KhongCoQuyenTruyCap);
            }
        }


        /// <summary>
        /// Load danh sach Menu theo Account
        /// </summary>
        /// <param name="IdAccount"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("menu-funtion-account")]
        public async Task<ActionResult<AtResult<MenuAccountOuput>>> GetListMenuChucNang_Account(string IdAccount)
        {
            if (await CheckPermission(_context))
            {
                try
                {
                    var listMenu = await _logicMenu.GetListMenuAccountAsync(IdAccount);
                    return new AtResult<MenuAccountOuput>(listMenu);
                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }
            else
            {
                return new AtResult<MenuAccountOuput>(AtNotify.KhongCoQuyenTruyCap);
            }
        }


        /// <summary>
        /// Save Menu theo account (Phân quyền Account)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("save-menu-account")]
        public async Task<ActionResult<AtResult<string>>> SaveMenuAccount([FromBody]MenuAccountInput input)
        {
            if (await CheckPermission(_context))
            {
                try
                {
                    var output = await _logicMenu.SaveMenuAccountAsync(input, UserId);
                    return new AtResult<string>(output);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                return new AtResult<string>(AtNotify.KhongCoQuyenTruyCap);
            }
        }


    }
}
