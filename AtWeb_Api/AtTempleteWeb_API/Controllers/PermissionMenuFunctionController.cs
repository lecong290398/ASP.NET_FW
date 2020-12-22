using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AtDomain;
using AtTempleteWeb_API.AtLogic;
using AtTempleteWeb_API.Context;
using AtTempleteWeb_API.Entires;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static AtDomain.AtMenuFuntionDm;

namespace AtTempleteWeb_API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionMenuFunctionController : AtBaseApiController
    {
        private static AtPermissionMenuFunctionLogic _logic;
        private static AtTempleteWebContext _context;
        public PermissionMenuFunctionController(AtTempleteWebContext context, AtPermissionMenuFunctionLogic logic)
        {
            _context = context;
            _logic = logic;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("load-listMenu-left")]
        public async Task<ActionResult<AtResult<GetMenuFuntionDmOutput>>> GetLeftMenu()
        {
            try
            {
                var model = await _logic.GetLeftMenu(UserId);
                return new AtResult<GetMenuFuntionDmOutput>(model);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet("/*get-permission*/")]
        public async Task<ActionResult<AtResult<List<MenuHelper_MenuFunctionPermissonOutput>>>> GetListPermission()
        {
            try
            {
                var model = await _logic.GetListMenuFuntionPermission(UserId);
                return new AtResult<List<MenuHelper_MenuFunctionPermissonOutput>>(model);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}