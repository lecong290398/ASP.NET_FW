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
using static AtDomain.AtRoleDm;

namespace AtTempleteWeb_API.AtLogic
{
    public class AtRoleLogic : AtBaseLogic
    {

        private static readonly IMapper _mapper;
        public AtRoleLogic(AtTempleteWebContext context, IConfiguration config) : base(context, config)
        {
        }

        static AtRoleLogic()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AtRoleDmInputCreate, Role>();
                cfg.CreateMap<Role, AtRoleDmInputCreate>();
                cfg.CreateMap<Role, AtRoleDmListOutput>();
                cfg.CreateMap<AtRoleDmInputEdit, Role>();
            });
            _mapper = config.CreateMapper();
        }

        public async Task<List<AtRoleDmComboboxOutput>> GetListCombobox_RoleAsyns()
        {
            var listRole = await _context.Role.Where(c => c.AtRowStatus == (int)AtRowStatus.Normal)
                                .Select(a => new AtRoleDmComboboxOutput
                                {
                                    Id = a.Id,
                                    Code = a.Code,
                                    RoleName = a.RoleName
                                })
                                .OrderBy(h => h.Code).ToListAsync().ConfigureAwait(false);


            return new List<AtRoleDmComboboxOutput>(listRole);
        }

        /// <summary>
        /// Lấy danh sách quyền với diều kiện AtRowStatus == (int)AtRowStatus.Normal
        /// </summary>
        /// <returns></returns>
        public async Task<Tuple<List<Role>, int , int>> GetListRoleAsyns(string userId, int pageNumber)
        {

            var totalCount = _context.Role.Count(c => c.AtRowStatus == (int)AtRowStatus.Normal);

            var listRole = await _context.Role.Where(c => c.AtRowStatus == (int)AtRowStatus.Normal)
                            .Select(a => new Role
                            {
                                Id = a.Id,
                                AtRowStatus = a.AtRowStatus,
                                AtCreatedBy = _context.AccountObject.FirstOrDefault(c => c.Id == a.AtCreatedBy).AccountObjectName,
                                AtCreatedDate = a.AtCreatedDate,
                                AtLastModifiedBy = _context.AccountObject.FirstOrDefault(c => c.Id == a.AtLastModifiedBy).AccountObjectName,
                                AtLastModifiedDate = a.AtLastModifiedDate,
                                AtRowversion = a.AtRowversion,
                                Code = a.Code,
                                Prioty = a.Prioty,
                                RoleName = a.RoleName
                            }).OrderBy(h => h.Code).Skip((pageNumber - 1) * _PageSize).Take(_PageSize).ToListAsync().ConfigureAwait(false);
            await WrtiteAudittingLog(new MSC_AudittingLog { Description = "Load list ROLE", UserID = userId }, AtSerialNoConts.CODE_LOG_READ);
           
            return new Tuple<List<Role>, int, int>(listRole, totalCount,_PageSize);
        }


        /// <summary>
        /// THêm mới quyền
        /// </summary>
        /// <param name="input"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<Tuple<Role, AtNotify>> CreateRoleAsyns(Role input, string userId)
        {
            try
            {
                var checkRole = await _context.Role.AnyAsync(c => c.Code == input.Code);

                if (!checkRole)
                {
                    input.AtCreatedDate = GetDateTimeFromServer();
                    var output = await _context.Role.AddAsync(input);
                    await _context.SaveChangesAsync();


                   

                    string data_New = JsonConvert.SerializeObject(output.Entity);

                    await WrtiteAudittingLog(new MSC_AudittingLog { Description = "Thêm mới ROLE ID: " + output.Entity.Id, UserID = userId, Data_New = data_New }, AtSerialNoConts.CODE_LOG_CREATE);
                    return new Tuple<Role, AtNotify>(output.Entity, AtNotify.InsertCompelete);
                }
                else
                {
                    return new Tuple<Role, AtNotify>(null, AtNotify.DuplicateCode);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Xóa quyền
        /// </summary>
        /// <param name="input"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<AtNotify> DeleteRoleAsyns(AtRoleDmInputDelete input, string userId)
        {
            try
            {
                var model = await _context.Role.FirstOrDefaultAsync(c => c.Id == input.Id);
                if (!model.AtRowversion.SequenceEqual(input.AtRowversion))
                {
                    return AtNotify.PhienGiaoDichHetHan;
                }
                else if (model == null)
                {
                    return AtNotify.NotFound;
                }

                model.AtRowStatus = (int)AtRowStatus.Hide;
                model.AtLastModifiedBy = userId;
                model.AtLastModifiedDate = GetDateTimeFromServer();

                await _context.SaveChangesAsync();
                string data_Old = JsonConvert.SerializeObject(model);
                await WrtiteAudittingLog(new MSC_AudittingLog { Description = "Xóa ROLE ID: " + model.Id, UserID = userId, Data_Old = data_Old }, AtSerialNoConts.CODE_LOG_DELETE);

                return AtNotify.DeleteComplete;
            }
            catch (Exception ex)
            {
                return AtNotify.DeleteFail;
            }
        }


        /// <summary>
        /// Chỉnh sủa role
        /// </summary>
        /// <param name="input"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<AtNotify> EditRoleAsyns(Role input, string userId)
        {
            try
            {
                var model = await _context.Role.FirstOrDefaultAsync(c => c.Id == input.Id);

                if (model == null)
                {
                    return AtNotify.NotFound;
                }
                if (!model.AtRowversion.SequenceEqual(input.AtRowversion))
                {
                    return AtNotify.PhienGiaoDichHetHan;
                }

                string data_Old = JsonConvert.SerializeObject(model);


                model.RoleName = input.RoleName;
                model.AtRowversion = input.AtRowversion;
                model.AtLastModifiedBy = userId;
                model.AtLastModifiedDate = GetDateTimeFromServer();
                await _context.SaveChangesAsync();

                string data_New = JsonConvert.SerializeObject(model);



                await WrtiteAudittingLog(new MSC_AudittingLog { Description = "Chỉnh sửa  ROLE ID: " + model.Id, UserID = userId, Data_Old = data_Old, Data_New = data_New }, AtSerialNoConts.CODE_LOGC_UPDATE);


                return AtNotify.UpdateCompelete;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<AtRoleDmInputEdit> GetRoleAsyns(string idRole)
        {
            try
            {
                var model = await _context.Role.Select(c => new AtRoleDmInputEdit
                {
                    Id = c.Id,
                    AtRowversion = c.AtRowversion,
                    RoleName = c.RoleName
                }).FirstOrDefaultAsync(c => c.Id == idRole);

                return model;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
