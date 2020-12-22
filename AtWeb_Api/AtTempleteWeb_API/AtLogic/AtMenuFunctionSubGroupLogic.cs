using AtTempleteWeb_API.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AtDomain.AtMenuFunctionSubGroupDm;

namespace AtTempleteWeb_API.AtLogic
{
    public class AtMenuFunctionSubGroupLogic : AtBaseLogic
    {
        public AtMenuFunctionSubGroupLogic(AtTempleteWebContext context, IConfiguration config) : base(context, config)
        {
        }

        public async Task<List<AtMenuFunctionSubGroupDm_Combobox>> GetListCombobox_AccountObjectAsyns()
        {
            return await _context.MenuFunctionSubGroup.Select(c => new AtMenuFunctionSubGroupDm_Combobox
            {
                Id = c.Id,
                SubGroupName = c.SubGroupName,
                IdMenuGroup = c.FK_MenuGroupNavigation.Id,
                MenuGroupName = c.FK_MenuGroupNavigation.GroupName
            }).OrderBy(h => h.Id).ToListAsync().ConfigureAwait(false);
        }
    }
}
