using AtDomain;
using AtTempleteWeb_API.Context;
using AtTempleteWeb_API.Entires;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AtDomain.AtSettingDm;

namespace AtTempleteWeb_API.AtLogic
{
    public class AtSettingLogic : AtBaseLogic
    {
        private static readonly IMapper _mapper;
        public AtSettingLogic(AtTempleteWebContext context, IConfiguration config) : base(context, config)
        {
        }
        static AtSettingLogic()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AtSettingDmListOutput, Setting>();
                cfg.CreateMap<Setting, AtSettingDmListOutput>();

            });
            _mapper = config.CreateMapper();
        }

        /// <summary>
        /// Load Danh sách setting
        /// </summary>
        /// <returns></returns>
        public async Task<Tuple<List<AtSettingDmListOutput>, int, int>> LoadListSetting(string userId, int pageNumber)
        {
            var totalCount = _context.Setting.Count(c => c.RowStatus == (int)AtRowStatus.Normal);

            var listSetting = await _context.Setting.AsNoTracking()
                  .Select(c => new AtSettingDmListOutput
                  {
                      Id = c.Id,
                      Description = c.Description,
                      IdParent = c.IdParent,
                      ImageSlug = c.ImageSlug,
                      IsManual = c.IsManual,
                      RowStatus = c.RowStatus,
                      RowVersion = c.RowVersion,
                      Value = c.Value,
                      NameParentGroup = c.IdParent
                  })
                  .Where(c => c.RowStatus == (int)AtRowStatus.Normal)
                  .OrderBy(h => h.Id).Skip((pageNumber - 1) * _PageSize).Take(_PageSize)
                  .ToListAsync().ConfigureAwait(false);

            await WrtiteAudittingLog(new MSC_AudittingLog { Description = "Load list Setting ", UserID = userId }, AtSerialNoConts.CODE_LOG_READ);


            return new Tuple<List<AtSettingDmListOutput>, int, int>(listSetting, totalCount, _PageSize);
        }


        /// <summary>
        /// Thêm mới SETTING
        /// </summary>
        /// <param name="input"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<Tuple<AtSettingDmListOutput, AtNotify>> CreateSetting(AtSettingDmCreateInput input, string userId)
        {
            try
            {
                var checkIdSetting = await _context.Setting.AnyAsync(c => c.Id == input.Id);
                if (!checkIdSetting)
                {
                    var model = new Setting
                    {
                        Id = input.Id,
                        Value = input.Value,
                        Description = input.Description,
                        IdParent = input.IdParent
                    };

                    var result = await _context.Setting.AddAsync(model);
                    await _context.SaveChangesAsync();

                    var Output = _mapper.Map<AtSettingDmListOutput>(result.Entity);

                    string data_Old = JsonConvert.SerializeObject(result.Entity);

                    await WrtiteAudittingLog(new MSC_AudittingLog { Description = "Thêm mới Setting ", UserID = userId, Data_New = data_Old }, AtSerialNoConts.CODE_LOG_CREATE);


                    return new Tuple<AtSettingDmListOutput, AtNotify>(Output, AtNotify.InsertCompelete);

                }
                else
                {
                    return new Tuple<AtSettingDmListOutput, AtNotify>(null, AtNotify.DuplicateCode);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public async Task<List<SettingParent>> GetListParentSetting()
        {
            try
            {
                var list = await _context.Setting.Where(c => c.IdParent == null && c.RowStatus == (int)AtRowStatus.Normal)
                   .Select(c => new SettingParent
                   {
                       Id = c.Id,
                       Value = c.Value
                   })
                  .OrderBy(h => h.Id)
                  .ToListAsync().ConfigureAwait(false);

                return new List<SettingParent>(list);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Xóa setting
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AtNotify> DeleteSetting(AtSettingDmInputDelete input, string userId)
        {
            try
            {
                var model = await _context.Setting.FirstOrDefaultAsync(c => c.Id == input.Id);
                if (!model.RowVersion.SequenceEqual(input.RowVersion))
                {
                    return AtNotify.PhienGiaoDichHetHan;
                }
                else if (model == null)
                {
                    return AtNotify.NotFound;

                }
                string data_Old = JsonConvert.SerializeObject(model);

                model.RowStatus = (int)AtRowStatus.Hide;
                string data_New = JsonConvert.SerializeObject(model);

                await WrtiteAudittingLog(new MSC_AudittingLog { Description = "Xóa Setting ID: " + model.Id, UserID = userId, Data_Old = data_Old, Data_New = data_New }, AtSerialNoConts.CODE_LOG_DELETE);

                await _context.SaveChangesAsync();
                return AtNotify.DeleteComplete;
            }
            catch (Exception ex)
            {
                return AtNotify.DeleteFail;
            }
        }


        /// <summary>
        /// Lấy dữ liệu setting đề phục vụ chức năng edit
        /// </summary>
        /// <param name="idSetting"></param>
        /// <returns></returns>
        public async Task<Tuple<AtSettingDmEditOutput, AtNotify>> GetSettingEdit(string idSetting)
        {
            var model = await _context.Setting.FirstOrDefaultAsync(c => c.Id == idSetting);
            if (model == null)
            {
                return new Tuple<AtSettingDmEditOutput, AtNotify>(null, AtNotify.NotFound);
            }


            var output = new AtSettingDmEditOutput
            {
                Id = model.Id,
                Description = model.Description,
                IdParent = model.IdParent,
                Value = model.Value,
                RowVersion = model.RowVersion
            };

            return new Tuple<AtSettingDmEditOutput, AtNotify>(output, AtNotify.GetComplete);
        }


        /// <summary>
        /// Chỉnh sửa Setting
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AtNotify> EditSetting(AtSettingDmEditInput input, string userId)
        {
            try
            {
                var model = await _context.Setting.FirstOrDefaultAsync(c => c.Id == input.Id);

                string data_Old = JsonConvert.SerializeObject(model);

                if (model == null)
                {
                    return AtNotify.NotFound;
                }
                if (!model.RowVersion.SequenceEqual(input.RowVersion))
                {
                    return AtNotify.PhienGiaoDichHetHan;
                }

                model.Value = input.Value;
                model.Description = input.Description;
                model.IdParent = input.IdParent;

                await _context.SaveChangesAsync();

                string data_New = JsonConvert.SerializeObject(model);

                await WrtiteAudittingLog(new MSC_AudittingLog { Description = "Chỉnh sửa Setting ", UserID = userId, Data_Old = data_Old, Data_New = data_New }, AtSerialNoConts.CODE_LOG_CREATE);

                return AtNotify.UpdateCompelete;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
