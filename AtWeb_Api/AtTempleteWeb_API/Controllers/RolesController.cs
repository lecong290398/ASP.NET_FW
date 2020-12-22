using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AtTempleteWeb_API.Context;
using AtTempleteWeb_API.Entires;
using AtTempleteWeb_API.AtLogic;
using AtDomain;
using static AtDomain.AtRoleDm;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AutoMapper;

namespace AtTempleteWeb_API.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : AtBaseApiController
    {
        private readonly AtTempleteWebContext _context;
        private readonly AtRoleLogic _logicRole;
        private static readonly IMapper _mapper;



        public RolesController(AtTempleteWebContext context, AtRoleLogic logicRole)
        {
            _context = context;
            _logicRole = logicRole;

        }

        static RolesController()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Role, AtRoleDmListOutput>();
            });
            _mapper = config.CreateMapper();
        }


        /// <summary>
        /// Load combobox ROLE
        /// </summary>
        /// <returns></returns>

        [HttpPost("load-cb-Role")]
        public async Task<ActionResult<AtResult<List<AtRoleDmComboboxOutput>>>> GetListComboboxRole()
        {
            var listRole = await _logicRole.GetListCombobox_RoleAsyns();
            return new AtResult<List<AtRoleDmComboboxOutput>>(listRole);
        }

        /// <summary>
        /// Lấy danh sách Quyền
        /// </summary>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("danh-sach-quyen")]
        public async Task<ActionResult<AtResult<List<AtRoleDmListOutput>>>> GetListRole([FromQuery]int pageNumber = 1)
        {
            if (await CheckPermission(_context))
            {

                var tuple = await _logicRole.GetListRoleAsyns(UserId, pageNumber);
                var mapper = _mapper.Map<List<AtRoleDmListOutput>> (tuple.Item1);
                return new AtResult<List<AtRoleDmListOutput>>(mapper) { TotalCount = tuple.Item2, PageSize = tuple.Item3 };
            }
            else
            {
                return new AtResult<List<AtRoleDmListOutput>>(AtNotify.KhongCoQuyenTruyCap);
            }
        }


        /// <summary>
        /// Thêm quyền mới
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("them-moi-quyen")]
        public async Task<ActionResult<AtResult<AtRoleDmListOutput>>> CreateRole([FromBody]AtRoleDmInputCreate input)
        {
            if (await CheckPermission(_context))
            {
                if (!ModelState.IsValid)
                {
                    return new AtResult<AtRoleDmListOutput>(AtNotify.InsertFail);
                }
                var role = new Role
                {
                    Id = input.Code,
                    Code = input.Code,
                    RoleName = input.RoleName,
                    AtCreatedBy = UserId,
                    Prioty = input.Prioty,
                };

                try
                {
                    var model = await _logicRole.CreateRoleAsyns(role, UserId);

                    if (model.Item2 == AtNotify.DuplicateCode)
                    {
                        return new AtResult<AtRoleDmListOutput>(AtNotify.DuplicateCode);
                    }
                    else
                    {
                        var mapper = _mapper.Map<AtRoleDmListOutput>(model.Item1);
                        return new AtResult<AtRoleDmListOutput>(mapper);
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            else
            {
                return new AtResult<AtRoleDmListOutput>(AtNotify.KhongCoQuyenTruyCap);
            }
        }

        /// <summary>
        /// Xóa quyền 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        [HttpPost("xoa-quyen")]
        public async Task<ActionResult<AtResult<AtRoleDmInputDelete>>> DeleteRole([FromBody]   AtRoleDmInputDelete input)
        {
            if (await CheckPermission(_context))
            {
                try
                {

                    if (input == null)
                    {
                        return new AtResult<AtRoleDmInputDelete>(AtNotify.UpdateFail);
                    }

                    var ouput = await _logicRole.DeleteRoleAsyns(input, UserId);

                    if (ouput == AtNotify.PhienGiaoDichHetHan || ouput == AtNotify.NotFound)
                    {
                        return new AtResult<AtRoleDmInputDelete>(ouput);
                    }

                    return new AtResult<AtRoleDmInputDelete>(input);
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            else
            {
                return new AtResult<AtRoleDmInputDelete>(AtNotify.KhongCoQuyenTruyCap);
            }
        }

        /// <summary>
        /// Chỉnh sửa quyền
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("chinh-sua-quyen")]
        public async Task<ActionResult<AtResult<AtRoleDmInputEdit>>> EditRole([FromBody]   AtRoleDmInputEdit input)
        {
            if (await CheckPermission(_context))
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        return new AtResult<AtRoleDmInputEdit>(AtNotify.UpdateFail);
                    }
                    var role = new Role
                    {
                        Id = input.Id,
                        RoleName = input.RoleName,
                        AtCreatedBy = UserId,
                        AtRowversion = input.AtRowversion,
                    };
                    var ouput = await _logicRole.EditRoleAsyns(role, UserId);

                    if (ouput == AtNotify.PhienGiaoDichHetHan || ouput == AtNotify.NotFound)
                    {
                        return new AtResult<AtRoleDmInputEdit>(ouput);
                    }
                    return new AtResult<AtRoleDmInputEdit>(input);
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            else
            {
                return new AtResult<AtRoleDmInputEdit>(AtNotify.KhongCoQuyenTruyCap);
            }
        }


        /// <summary>
        /// lấy thông tin quyền dùng để chỉnh sửa
        /// </summary>
        /// <param name="idRole"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("du-lieu-chinhsua-quyen")]
        public async Task<ActionResult<AtResult<AtRoleDmInputEdit>>> GetRole(string idRole)
        {
            if (await CheckPermission(_context))
            {
                try
                {
                    var model = await _logicRole.GetRoleAsyns(idRole);
                    if (model == null)
                    {
                        return new AtResult<AtRoleDmInputEdit>(AtNotify.NotFound);

                    }
                    return new AtResult<AtRoleDmInputEdit>(model);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                return new AtResult<AtRoleDmInputEdit>(AtNotify.KhongCoQuyenTruyCap);
            }
        }
    }
}
