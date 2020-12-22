using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FW_MVC_API.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;

namespace FW_MVC_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtBaseApiController : ControllerBase
    {
        static string[] _arrTiengViet = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
    "đ",
    "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
    "í","ì","ỉ","ĩ","ị",
    "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
    "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
    "ý","ỳ","ỷ","ỹ","ỵ",};
        static string[] _arrNoUnicode = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
    "d",
    "e","e","e","e","e","e","e","e","e","e","e",
    "i","i","i","i","i",
    "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
    "u","u","u","u","u","u","u","u","u","u","u",
    "y","y","y","y","y",};

        static string[] _arrTiengVietUpper;
        static string[] _arrNoUnicodeUpper;

        static AtBaseApiController()
        {
            _arrTiengVietUpper = _arrTiengViet.Select(h => h.ToUpper()).ToArray();
            _arrNoUnicodeUpper = _arrNoUnicode.Select(h => h.ToUpper()).ToArray();
            //System.Reflection.Assembly.GetExecutingAssembly().FullName;
        }

        protected static string RemoveUnicode(string text)
        {
            for (int i = 0; i < _arrTiengViet.Length; i++)
            {
                text = text.Replace(_arrTiengViet[i], _arrNoUnicode[i]);
                text = text.Replace(_arrTiengVietUpper[i], _arrNoUnicodeUpper[i]);
            }
            return text;
        }
        protected async Task<bool> CheckPermission(DataFrameworkWebContext _context)
        {
            string controllerName = ControllerContext.ActionDescriptor.ControllerName;
            string acctionName = ControllerContext.ActionDescriptor.ActionName;
            // Kiểm tra xem MenuFunction có yêu cầu bắt buộc để kiểm tra phân quyền hay không? Hoặc trường hợp IsPublic=True
            var funcMenu = await _context.MenuFunction.FirstOrDefaultAsync(u => u.ControllerName.Equals(controllerName) && u.AcctionName.Equals(acctionName) && u.IsPublic == false).ConfigureAwait(false);
            if (funcMenu == null)
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


        public string UserId => GetAtUserToken();

        protected string GetAtUserToken()
        {
            if (HttpContext.Request.Headers.TryGetValue("AtUserToken", out StringValues value))
            {
                return value.ToString();
            }
            else
            {
                return "System";
            }
        }
    }
}