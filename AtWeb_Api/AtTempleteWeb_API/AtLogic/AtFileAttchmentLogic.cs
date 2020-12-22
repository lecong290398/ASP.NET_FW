using AtTempleteWeb_API.Context;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AtTempleteWeb_API.AtLogic
{
    public class AtFileAttchmentLogic : AtBaseLogic
    {
        private static readonly IMapper _mapper;

        public AtFileAttchmentLogic(AtTempleteWebContext context, IConfiguration config) : base(context, config)
        {
        }

        static AtFileAttchmentLogic()
        {

        }
    }
}
