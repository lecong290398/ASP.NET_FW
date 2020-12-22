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
using static AtDomain.AtMenuFunctionSubGroupDm;
using AtTempleteWeb_API.AtLogic;

namespace AtTempleteWeb_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuFunctionSubGroupsController : AtBaseApiController
    {
        private readonly AtTempleteWebContext _context;
        private readonly AtMenuFunctionSubGroupLogic _logicSubMenu;

        public MenuFunctionSubGroupsController(AtTempleteWebContext context, AtMenuFunctionSubGroupLogic logicSubMenu)
        {
            _context = context;
            _logicSubMenu = logicSubMenu;
        }

        /// <summary>
        /// Load combobox MenuFunctionSub
        /// </summary>
        /// <returns></returns>
        [HttpGet("danh-sach-menu-function")]
        public async Task<ActionResult<AtResult<List<AtMenuFunctionSubGroupDm_Combobox>>>> GetComboboxSubMenuFunction()
        {
            try
            {
                var listSubMenu = await _logicSubMenu.GetListCombobox_AccountObjectAsyns();
                return new AtResult<List<AtMenuFunctionSubGroupDm_Combobox>>(listSubMenu);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
