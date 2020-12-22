using AtDomain;
using AtTempleteWeb_API.Context;
using AtTempleteWeb_API.Entires;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NETCore.Encrypt;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using static AtDomain.AccountObjectDm;
using static AtDomain.InformationUserDm;

namespace AtTempleteWeb_API.AtLogic
{
    public partial class AtAccountObjectLogic : AtBaseLogic
    {
        private static readonly IMapper _mapper;

        static AtAccountObjectLogic()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<InformationUserDmInput, InfomationUser>();
                cfg.CreateMap<InfomationUser, InformationUserDmInput>();
                cfg.CreateMap<AccountObjectDmInput_Create, AccountObject>();
                cfg.CreateMap<AccountObject, AccountObjectDmInput_Create>();
                cfg.CreateMap<AccountObjectDmInput_Edit, AccountObject>();
                cfg.CreateMap<AccountObject, AccountObjectDmInput_Edit>();
                cfg.CreateMap<AccountObject, AccountObjectDmListOutput>();
            });
            _mapper = config.CreateMapper();
        }

        public AtAccountObjectLogic(AtTempleteWebContext context, IConfiguration config) : base(context, config)
        {
        }

        /// <summary>
        /// Load combobox AccountObject
        /// </summary>
        /// <returns></returns>
        public async Task<List<AccountObjectDmOutput>> GetListCombobox_AccountObjectAsyns()
        {
            return await _context.AccountObject.Where(a => a.AtRowStatus == (int)AtRowStatus.Normal).Select(c => new AccountObjectDmOutput
            {
                Id = c.Id,
                AccountCode = c.AccountCode,
                AccountObjectName = c.AccountObjectName,
                UserName = c.UserName
            }).OrderBy(h => h.AccountCode)
                  .ToListAsync().ConfigureAwait(false);
        }


        /// <summary>
        /// Load danh sách tài khoản
        /// </summary>
        /// <returns></returns>

        public async Task<Tuple<List<AccountObjectDmListOutput>, int, int>> LoadListAccountObject(string userId, int pageNumber)
        {

            var totalCount = _context.AccountObject.Count(c => c.AtRowStatus == (int)AtRowStatus.Normal);

            var listAccount = await _context.AccountObject.AsNoTracking()
                  .Where(w => w.AtRowStatus == (int)AtRowStatus.Normal)
                  .Distinct()
                  .Select(c => new AccountObjectDmListOutput
                  {
                      Id = c.Id,
                      AccountCode = c.AccountCode,
                      AccountObjectName = c.AccountObjectName,
                      AtCreatedBy = _context.AccountObject.FirstOrDefault(a => a.Id == c.AtCreatedBy).AccountObjectName,
                      AtCreatedDate = c.AtCreatedDate,
                      AtLastModifiedBy = _context.AccountObject.FirstOrDefault(a => a.Id == c.AtCreatedBy).AccountObjectName,
                      AtLastModifiedDate = c.AtLastModifiedDate,
                      AtRowStatus = c.AtRowStatus,
                      AtRowVersion = c.AtRowVersion,
                      Email = c.Email,
                      PassWord = c.PassWord,
                      Phone = c.Phone,
                      UserName = c.UserName
                  })
                  .OrderBy(h => h.AccountCode).Skip((pageNumber - 1) * _PageSize).Take(_PageSize)
                  .ToListAsync().ConfigureAwait(false);

            await WrtiteAudittingLog(new MSC_AudittingLog { Description = "Load list AccountObject", UserID = userId }, AtSerialNoConts.CODE_LOG_READ);

            return new Tuple<List<AccountObjectDmListOutput>, int, int>(listAccount, totalCount,_PageSize);
        }


        /// <summary>
        /// Get Danh sách AccountObject theo Id Account phục vụ cho tiền trình chỉnh sửa tài khoản
        /// </summary>
        /// <param name="idAccount"></param>
        /// <returns></returns>
        public async Task<AccountObjectDmOuput_EditOrDetail> GetAccountObject_Edit(string idAccount, string userId)
        {
            var account = await _context.AccountObject.AsNoTracking()
             .Select(c => new AccountObjectDmListOutput
             {
                 Id = c.Id,
                 AccountCode = c.AccountCode,
                 AccountObjectName = c.AccountObjectName,
                 AtCreatedBy = _context.AccountObject.FirstOrDefault(a => a.Id == c.AtCreatedBy).AccountObjectName,
                 AtCreatedDate = c.AtCreatedDate,
                 AtLastModifiedBy = _context.AccountObject.FirstOrDefault(a => a.Id == c.AtCreatedBy).AccountObjectName,
                 AtLastModifiedDate = c.AtLastModifiedDate,
                 AtRowStatus = c.AtRowStatus,
                 AtRowVersion = c.AtRowVersion,
                 Email = c.Email,
                 Phone = c.Phone,
                 UserName = c.UserName
             })
             .FirstOrDefaultAsync(w => w.AtRowStatus == (int)AtRowStatus.Normal && w.Id == idAccount).ConfigureAwait(false);
            if (account != null)
            {
                var listIdRole = await _context.Role_AccountObject.Where(a => a.FK_AccountObject == account.Id).Select(c => c.FK_Role).ToListAsync();
                var listIdPhongBan = await _context.Department_Account.Where(a => a.FK_AccountObject == account.Id).Select(c => c.FK_Department).ToListAsync();

                await WrtiteAudittingLog(new MSC_AudittingLog { Description = "Tãi dữ liệu chỉnh sửa AccountObject với ID: " + idAccount, UserID = userId }, AtSerialNoConts.CODE_LOG_READ);

                return new AccountObjectDmOuput_EditOrDetail
                {
                    AccountObject_Edit = account,
                    ListIdPhongBan = new List<string>(listIdPhongBan),
                    ListIdRole = new List<string>(listIdRole)
                };
            }
            return null;
        }


        /// <summary>
        /// Xóa tài khoản
        /// </summary>
        /// <param name="input"> thông tin tài khoản xóa </param>
        /// <param name="userId"> Id User đang thao tác </param>
        /// <returns></returns>
        public async Task<AtNotify> AccountObject_Delete(AccountObjectDm_Delete input, string userId)
        {
            try
            {
                var model = await _context.AccountObject.FirstOrDefaultAsync(c => c.Id == input.Id);
                if (model == null)
                {
                    return AtNotify.NotFound;
                }
                else if (!model.AtRowVersion.SequenceEqual(input.AtRowVersion))
                {
                    return AtNotify.PhienGiaoDichHetHan;
                }

                model.AtRowStatus = (int)AtRowStatus.Hide;
                model.AtLastModifiedBy = userId;
                model.AtLastModifiedDate = GetDateTimeFromServer();

                await _context.SaveChangesAsync();

                string data_Old = JsonConvert.SerializeObject(model);

                await WrtiteAudittingLog(new MSC_AudittingLog { Description = "Xóa AccountObject với ID:" + input.Id, UserID = userId, Data_Old = data_Old }, AtSerialNoConts.CODE_LOG_DELETE);
                return AtNotify.DeleteComplete;
            }
            catch (Exception ex)
            {
                return AtNotify.DeleteFail;
            }
        }

        public async Task<Tuple<AccountObject, AtNotify>> CreateAccountObject(AccountObject input, List<string> ListIdDepartment, List<string> ListIdRole, string userId)
        {
            try
            {
                var checkAccount = await _context.AccountObject.AnyAsync(c => c.UserName == input.UserName);

                if (!checkAccount)
                {

                    input.PassWord = EncryptProvider.Sha1(input.PassWord);
                    input.AtCreatedDate = GetDateTimeFromServer();

                    var model = await _context.AccountObject.AddAsync(input);

                    foreach (var itemD in ListIdDepartment)
                    {
                        var model_DA = new Department_Account
                        {
                            AtCreatedBy = userId,
                            AtCreatedDate = GetDateTimeFromServer(),
                            AtLastModifiedBy = userId,
                            AtLastModifiedDate = GetDateTimeFromServer(),
                            AtRowStatus = (int)AtRowStatus.Normal,
                            FK_AccountObject = input.Id,
                            FK_Department = itemD
                        };
                        await _context.Department_Account.AddAsync(model_DA);
                    }

                    foreach (var itemR in ListIdRole)
                    {
                        var model_RA = new Role_AccountObject
                        {
                            AtCreatedBy = userId,
                            AtCreatedDate = GetDateTimeFromServer(),
                            AtLastModifiedBy = userId,
                            AtLastModifiedDate = GetDateTimeFromServer(),
                            AtRowStatus = (int)AtRowStatus.Normal,
                            FK_AccountObject = input.Id,
                            FK_Role = itemR,
                        };
                        await _context.Role_AccountObject.AddAsync(model_RA);
                    }

                    await _context.SaveChangesAsync();

                    await WrtiteAudittingLog(new MSC_AudittingLog { Description = "Thêm mới AccountObject với ID: " + model.Entity.Id, UserID = userId }, AtSerialNoConts.CODE_LOG_CREATE);
                    return new Tuple<AccountObject, AtNotify>(model.Entity, AtNotify.InsertCompelete);
                }
                else
                {
                    return new Tuple<AccountObject, AtNotify>(null, AtNotify.DuplicateCode);

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public async Task<AtNotify> EditAccountObject(AccountObjectDmInput_Edit input, string userId)
        {
            try
            {
                var model = await _context.AccountObject.FirstOrDefaultAsync(c => c.Id == input.Id);

                if (model == null)
                {
                    return AtNotify.NotFound;
                }
                if (!model.AtRowVersion.SequenceEqual(input.AtRowVersion))
                {
                    return AtNotify.PhienGiaoDichHetHan;
                }
                model.AtLastModifiedBy = userId;
                model.AtLastModifiedDate = GetDateTimeFromServer();

                //Lưu data Old
                var data_old = new AccountObjectDmInput_Edit
                {
                    Id = model.Id,
                    AccountObjectName = model.AccountObjectName,
                    AtRowVersion = model.AtRowVersion,
                    Email = model.Email,
                    Phone = model.Phone,
                };


                var mapper = _mapper.Map(input, model);

                var data_new = new AccountObjectDmInput_Edit
                {
                    Id = mapper.Id,
                    AccountObjectName = mapper.AccountObjectName,
                    AtRowVersion = mapper.AtRowVersion,
                    Email = mapper.Email,
                    Phone = mapper.Phone,
                    ListIdDepartment = new List<string>(),
                    ListIdRole = new List<string>()
                };


                //Xóa cái củ

                var getListRoleAccount = await _context.Role_AccountObject.Where(c => c.FK_AccountObject == mapper.Id).ToListAsync();

                //lưu list role cũ
                data_old.ListIdRole = new List<string>(getListRoleAccount.Select(c => c.FK_Role).ToList());
                _context.Role_AccountObject.RemoveRange(getListRoleAccount);

                var getListDepartment = await _context.Department_Account.Where(c => c.FK_AccountObject == mapper.Id).ToListAsync();

                //Luu list Department cũ
                data_old.ListIdDepartment = new List<string>(getListDepartment.Select(c => c.FK_Department).ToList());
                _context.Department_Account.RemoveRange(getListDepartment);




                //Thêm mới 
                foreach (var itemD in input.ListIdDepartment)
                {
                    var model_DA = new Department_Account
                    {
                        AtCreatedBy = userId,
                        AtCreatedDate = GetDateTimeFromServer(),
                        AtLastModifiedBy = userId,
                        AtLastModifiedDate = GetDateTimeFromServer(),
                        AtRowStatus = (int)AtRowStatus.Normal,
                        FK_AccountObject = mapper.Id,
                        FK_Department = itemD
                    };
                    await _context.Department_Account.AddAsync(model_DA);

                    data_new.ListIdDepartment.Add(model_DA.FK_Department);
                }

                foreach (var itemR in input.ListIdRole)
                {
                    var model_RA = new Role_AccountObject
                    {
                        AtCreatedBy = userId,
                        AtCreatedDate = GetDateTimeFromServer(),
                        AtLastModifiedBy = userId,
                        AtLastModifiedDate = GetDateTimeFromServer(),
                        AtRowStatus = (int)AtRowStatus.Normal,
                        FK_AccountObject = mapper.Id,
                        FK_Role = itemR,
                    };
                    await _context.Role_AccountObject.AddAsync(model_RA);

                    data_new.ListIdRole.Add(model_RA.FK_Role);
                }

                string json_DataNew = JsonConvert.SerializeObject(data_new);
                string json_DataOld = JsonConvert.SerializeObject(data_old);


                await WrtiteAudittingLog(new MSC_AudittingLog { Description = "Chỉnh sửa AccountObject với ID: " + mapper.Id, UserID = userId, Data_New = json_DataNew, Data_Old = json_DataOld }, AtSerialNoConts.CODE_LOGC_UPDATE);

                await _context.SaveChangesAsync();

                return AtNotify.UpdateCompelete;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<AtNotify> ResetPasswordAccountObject(AccountObjectDm_ResetPassword input, string userId)
        {
            try
            {
                var model = await _context.AccountObject.FirstOrDefaultAsync(c => c.Id == input.Id);

                if (model == null)
                {
                    return AtNotify.NotFound;
                }
                if (!model.AtRowVersion.SequenceEqual(input.AtRowVersion))
                {
                    return AtNotify.PhienGiaoDichHetHan;
                }

                var data_old = JsonConvert.SerializeObject(new AccountObjectDm_ResetPassword
                {
                    Id = model.Id,
                    AtRowVersion = model.AtRowVersion,
                    PassWord = model.PassWord
                });


                model.PassWord = EncryptProvider.Sha1(input.PassWord);
                model.AtLastModifiedBy = userId;
                model.AtLastModifiedDate = GetDateTimeFromServer();
                await _context.SaveChangesAsync();


                var data_new = JsonConvert.SerializeObject(new AccountObjectDm_ResetPassword
                {
                    Id = model.Id,
                    AtRowVersion = model.AtRowVersion,
                    PassWord = model.PassWord
                });

                await WrtiteAudittingLog(new MSC_AudittingLog { Description = "Đổi mật khẩu AccountObject với ID: " + model.Id, UserID = userId, Data_New = data_new, Data_Old = data_old }, AtSerialNoConts.CODE_LOG_CREATE);
                return AtNotify.UpdateCompelete;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<AtNotify> UpdateHoSoCaNhan(AccountObjectDm_UpdateAccount input, string userId)
        {
            try
            {
                var model = await _context.AccountObject.FirstOrDefaultAsync(c => c.Id == userId);

                if (model == null)
                {
                    return AtNotify.NotFound;
                }

                var data_old = JsonConvert.SerializeObject(new AccountObjectDm_UpdateAccount
                {
                    AccountObjectName = model.AccountObjectName,
                    Phone = model.Phone,
                    Email = model.Email
                });

                if (!string.IsNullOrWhiteSpace(input.AccountObjectName))
                {
                    model.AccountObjectName = input.AccountObjectName;
                }
                if (!string.IsNullOrWhiteSpace(input.Email))
                {
                    model.Email = input.Email;
                }
                if (!string.IsNullOrWhiteSpace(input.Phone))
                {
                    model.Phone = input.Phone;
                }

                model.AtLastModifiedBy = userId;
                model.AtLastModifiedDate = GetDateTimeFromServer();
                await _context.SaveChangesAsync();


                var data_new = JsonConvert.SerializeObject(new AccountObjectDm_ResetPassword
                {
                    Id = model.Id,
                    AtRowVersion = model.AtRowVersion,
                    PassWord = model.PassWord
                });

                await WrtiteAudittingLog(new MSC_AudittingLog { Description = "Sửa hồ sơ cá nhân với ID: " + model.Id, UserID = userId, Data_New = data_new, Data_Old = data_old }, AtSerialNoConts.CODE_LOG_CREATE);
                return AtNotify.UpdateCompelete;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<AtNotify> DoiMatKhauCaNhan(AccountObjectDm_UpdatePass input, string userId)
        {
            try
            {
                var model = await _context.AccountObject.FirstOrDefaultAsync(c => c.Id == userId);

                if (model == null)
                {
                    return AtNotify.NotFound;
                }
                var data_old = JsonConvert.SerializeObject(new AccountObjectDm_ResetPassword
                {
                    Id = model.Id,
                    AtRowVersion = model.AtRowVersion,
                    PassWord = model.PassWord
                });
                if (!string.IsNullOrWhiteSpace(input.newPass) && !string.IsNullOrWhiteSpace(input.oldPass))
                {
                    if (EncryptProvider.Sha1(input.oldPass) == model.PassWord)
                    {
                        model.PassWord = EncryptProvider.Sha1(input.newPass);
                        model.AtLastModifiedBy = userId;
                        model.AtLastModifiedDate = GetDateTimeFromServer();
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        return AtNotify.UpdateFail;
                    }
                }

                var data_new = JsonConvert.SerializeObject(new AccountObjectDm_ResetPassword
                {
                    Id = model.Id,
                    AtRowVersion = model.AtRowVersion,
                    PassWord = model.PassWord
                });

                await WrtiteAudittingLog(new MSC_AudittingLog { Description = "Sửa mật khẩu cá nhân với ID: " + model.Id, UserID = userId, Data_New = data_new, Data_Old = data_old }, AtSerialNoConts.CODE_LOG_CREATE);
                return AtNotify.UpdateCompelete;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
