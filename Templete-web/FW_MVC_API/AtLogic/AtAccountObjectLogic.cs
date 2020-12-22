using AutoMapper;
using Domain;
using FW_MVC_API.Context;
using FW_MVC_API.Helper;
using FW_MVC_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Domain.AccountObjectDm;
using static Domain.InformationUserDm;

namespace FW_MVC_API.AtLogic
{
    public partial class AtAccountObjectLogic : ATLogicBaseHelper
    {
        private static readonly IMapper _mapper;

        static AtAccountObjectLogic()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<InformationUserDmInput, InfomationUser>();
                cfg.CreateMap<InfomationUser, InformationUserDmInput>();
            });
            _mapper = config.CreateMapper();
        }

        public AtAccountObjectLogic(DataFrameworkWebContext context, IConfiguration config) : base(context, config)
        {
        }

        public async Task<List<AccountObjectDmOuput>> GetListCombobox_AccountObjectAsyns()
        {
            return await _context.AccountObject.Where(a => a.AtRowStatus == (int)AtRowStatus.Normal).Select(c => new AccountObjectDmOuput
            {
                Id = c.Id,
                AccountCode = c.AccountCode,
                AccountObjectName = c.AccountObjectName,
                UserName = c.UserName
            }).ToListAsync();
        }
    }
}
