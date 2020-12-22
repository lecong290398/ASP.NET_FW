using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AtTempleteWeb_API.Context;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;

namespace AtTempleteWeb_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtBaseApiController : ControllerBase
    {

        protected async Task<bool> CheckPermission(AtTempleteWebContext _context)
        {
            string controllerName = ControllerContext.ActionDescriptor.ControllerName;
            string acctionName = ControllerContext.ActionDescriptor.ActionName;
            // Kiểm tra xem MenuFunction có yêu cầu bắt buộc để kiểm tra phân quyền hay không? Hoặc trường hợp IsPublic=True
            var funcMenu = await _context.MenuFunction.FirstOrDefaultAsync(u => u.ControllerName.Equals(controllerName) && u.AcctionName.Equals(acctionName) && u.IsPublic == false ).ConfigureAwait(false);
            if (funcMenu == null || UserId == "system")
            {
                // Chưa có bắt buộc phân quyền cho Acction của Controller này, vì vậy lúc nào cũng được phép
                return true;
            }
            else
            {
                // Chức năng này đang bắt buộc phải kiểm tra quyền theo từng người dùng
                var funcAccount = await _context.MenuFunction_Account.FirstOrDefaultAsync(u => u.FK_AccountObject == UserId && u.FK_MenuFunction == funcMenu.Id).ConfigureAwait(false);
                if (funcAccount == null)
                {
                    // Kiểm tra xem user thuộc Role có quyền hay không
                    bool isPermiss = false;
                    var listRoleId = User.Claims.Where(h => h.Type == ClaimTypes.Role).Select(h => h.Value).ToList();
                    foreach (var item in listRoleId)
                    {
                        var checkPermiss = await _context.MenuFunction_Role.FirstOrDefaultAsync(u => u.FK_MenuFunction == funcMenu.Id && u.FK_Role == item).ConfigureAwait(false);
                        if (checkPermiss != null)
                        {
                            isPermiss = true;
                            break;
                        }
                    }
                    return isPermiss;
                }
                else
                {
                    return true;
                }
            }
        }



        public static  string UserId { get; set; }

    }
}