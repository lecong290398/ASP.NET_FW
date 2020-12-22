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
using static AtDomain.AtSettingDm;
using AtTempleteWeb_API.AtLogic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace AtTempleteWeb_API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : AtBaseApiController
    {
        private readonly AtTempleteWebContext _context;
        private readonly AtSettingLogic _logicSetting;

        public SettingsController(AtTempleteWebContext context, AtSettingLogic logicSetting)
        {
            _context = context;
            _logicSetting = logicSetting;
        }


        /// <summary>
        /// Load danh sách Setting
        /// </summary>
        /// <returns></returns>

        [HttpGet("danh-sach-setting")]
        public async Task<ActionResult<AtResult<List<AtSettingDmListOutput>>>> GetListSetting([FromQuery]int pageNumber = 1)
        {
            try
            {
                var tuple = await _logicSetting.LoadListSetting(UserId, pageNumber);
                return new AtResult<List<AtSettingDmListOutput>>(tuple.Item1) { TotalCount = tuple.Item2, PageSize = tuple.Item3 };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Thêm mới setting
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        [HttpPost("them-moi-setting")]
        public async Task<ActionResult<AtResult<AtSettingDmListOutput>>> CreateSetting([FromBody]AtSettingDmCreateInput input)
        {
            if (await CheckPermission(_context))
            {
                try
                {
                    var output = await _logicSetting.CreateSetting(input, UserId);
                    if (output.Item2 == AtNotify.DuplicateCode)
                    {
                        return new AtResult<AtSettingDmListOutput>(AtNotify.DuplicateCode);

                    }

                    return new AtResult<AtSettingDmListOutput>(output.Item1);

                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            else
            {
                return new AtResult<AtSettingDmListOutput>(AtNotify.KhongCoQuyenTruyCap);
            }
        }

        /// <summary>
        /// Xóa Setting
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        [HttpPost("xoa-setting")]
        public async Task<ActionResult<AtResult<AtSettingDmInputDelete>>> DeleteSetting([FromBody]AtSettingDmInputDelete input)
        {
            if (await CheckPermission(_context))
            {

                try
                {
                    var ouput = await _logicSetting.DeleteSetting(input, UserId);
                    if (ouput == AtNotify.PhienGiaoDichHetHan)
                    {
                        return new AtResult<AtSettingDmInputDelete>(AtNotify.PhienGiaoDichHetHan);
                    }
                    if (ouput == AtNotify.NotFound)
                    {
                        return new AtResult<AtSettingDmInputDelete>(AtNotify.NotFound);

                    }

                    return new AtResult<AtSettingDmInputDelete>(input);
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            else
            {
                return new AtResult<AtSettingDmInputDelete>(AtNotify.KhongCoQuyenTruyCap);
            }
        }


        [HttpPost("get-list-combobox-setting-parent")]
        public async Task<ActionResult<AtResult<List<SettingParent>>>> LoadListComboboxParent()
        {
            try
            {
                var listParent = await _logicSetting.GetListParentSetting();
                return new AtResult<List<SettingParent>>(listParent);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        /// <summary>
        /// Lấy Setting theo Id
        /// </summary>
        /// <param name="idSetting"></param>
        /// <returns></returns>

        [HttpGet("get-setting-edit")]
        public async Task<ActionResult<AtResult<AtSettingDmEditOutput>>> Get_EditSetting(string idSetting)
        {
            try
            {
                var output = await _logicSetting.GetSettingEdit(idSetting);
                if (output.Item2 == AtNotify.NotFound)
                {
                    return new AtResult<AtSettingDmEditOutput>(AtNotify.NotFound);
                }
                return new AtResult<AtSettingDmEditOutput>(output.Item1);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        /// <summary>
        /// Chỉnh sửa setting
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        [HttpPost("chinh-sua-setting")]
        public async Task<ActionResult<AtResult<AtSettingDmEditInput>>> EditSetting([FromBody]AtSettingDmEditInput input)
        {
            if (await CheckPermission(_context))
            {

                try
                {
                    var output = await _logicSetting.EditSetting(input, UserId);
                    if (output == AtNotify.NotFound || output == AtNotify.PhienGiaoDichHetHan)
                    {
                        return new AtResult<AtSettingDmEditInput>(output);
                    }
                    return new AtResult<AtSettingDmEditInput>(input);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                return new AtResult<AtSettingDmEditInput>(AtNotify.KhongCoQuyenTruyCap);

            }
        }
    }
}
