using AtDomain;
using AtTempleteWeb_API.Context;
using AtTempleteWeb_API.Entires;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AtDomain.AtDepartmentDm;

namespace AtTempleteWeb_API.AtLogic
{
    public class AtDepartmentLogic : AtBaseLogic
    {
        public AtDepartmentLogic(AtTempleteWebContext context, IConfiguration config) : base(context, config)
        {
        }

        public async Task<List<AtDepartmentDmComboboxOutput>> GetListCombobox_DepartmentAsyns()
        {
            var listDepartment = await _context.Department.Where(c => c.AtRowStatus == (int)AtRowStatus.Normal)
                                    .Select(c => new AtDepartmentDmComboboxOutput
                                    {
                                        Id = c.Id,
                                        Code = c.Code,
                                        DepartmentName = c.DepartmentName
                                    }).OrderBy(h => h.Code) .ToListAsync().ConfigureAwait(false);

            return new List<AtDepartmentDmComboboxOutput>(listDepartment);
        }
    }
}
