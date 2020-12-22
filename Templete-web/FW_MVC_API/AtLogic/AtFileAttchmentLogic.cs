using AutoMapper;
using FW_MVC_API.Context;
using FW_MVC_API.Helper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FW_MVC_API.AtLogic
{
    public class AtFileAttchmentLogic : ATLogicBaseHelper
    {
        private static readonly IMapper _mapper;

        public AtFileAttchmentLogic(DataFrameworkWebContext context, IConfiguration config) : base(context, config)
        {
        }

        static AtFileAttchmentLogic()
        {

        }
    }
}
