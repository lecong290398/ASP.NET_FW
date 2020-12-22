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
using AtTempleteWeb_API.AtLogic;
using static AtDomain.AtDepartmentDm;

namespace AtTempleteWeb_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : AtBaseApiController
    {
        private readonly AtTempleteWebContext _context;
        private readonly AtDepartmentLogic _logicDepartment;

        public DepartmentsController(AtTempleteWebContext context , AtDepartmentLogic logicDepartment)
        {
            _context = context;
            _logicDepartment = logicDepartment;
        }

        /// <summary>
        /// Load combobox Departments
        /// </summary>
        /// <returns></returns>
        [HttpPost("load-cb-departments")]
        public async Task<ActionResult<AtResult<List<AtDepartmentDmComboboxOutput>>>> GetListComboboxRole()
        {
            var listDepartment = await _logicDepartment.GetListCombobox_DepartmentAsyns();
            return new AtResult<List<AtDepartmentDmComboboxOutput>>(listDepartment);
        }


    }
}
