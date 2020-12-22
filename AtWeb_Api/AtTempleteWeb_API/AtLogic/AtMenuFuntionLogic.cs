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
using static AtDomain.AtMenuFunction_AccountDm;
using static AtDomain.AtMenuFunction_RoleDm;
using static AtDomain.AtMenuFunctionSubGroupDm;
using static AtDomain.AtMenuFuntionDm;

namespace AtTempleteWeb_API.AtLogic
{
    public class AtMenuFuntionLogic : AtBaseLogic
    {

        private static readonly IMapper _mapper;

        public AtMenuFuntionLogic(AtTempleteWebContext context, IConfiguration config) : base(context, config)
        {
        }

        static AtMenuFuntionLogic()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MenuFunction, MenuFunctionDmCreatetInputOrEdit>();
                cfg.CreateMap<MenuFunctionDmCreatetInputOrEdit, MenuFunction>();
                cfg.CreateMap<MenuFunction, MenuFunctionDmOutput>();

            });
            _mapper = config.CreateMapper();
        }

        public async Task<Tuple<List<MenuFunctionDmOutput>,int,int>> GetListMenuFunctionAsyns(string userId, int pageNumber)
        {

            var totalCount = _context.MenuFunction.Count(c => c.Status == (int)AtRowStatus.Normal);

            var listMenuFunction = await _context.MenuFunction.Where(c => c.Status == (int)AtRowStatus.Normal)
                        .Select(c => new MenuFunctionDmOutput
                        {
                            Status = c.Status,
                            AcctionName = c.AcctionName,
                            AcctionNameView = c.AcctionNameView,
                            ControllerName = c.ControllerName,
                            ControllerNameView = c.ControllerNameView,
                            CreateDate = c.CreateDate,
                            CssClass = c.CssClass,
                            Icon = c.Icon,
                            Id = c.Id,
                            IsMenu = c.IsMenu,
                            IsPublic = c.IsPublic,
                            IdMenuSubGroup = c.FK_MenuSubGroup,
                            NameMenuSubGroup = c.FK_MenuSubGroupNavigation.SubGroupName,
                            Note = c.Note,
                            Parrent = c.Parrent,
                            RouteId = c.RouteId,
                            SortIndex = c.SortIndex,
                            Title = c.Title,
                            IdMenuGroup = c.FK_MenuSubGroupNavigation.FK_MenuGroupNavigation.Id,
                            NameMenuGroup = c.FK_MenuSubGroupNavigation.FK_MenuGroupNavigation.GroupName
                        }).OrderBy(h => h.CreateDate)
                        .Skip((pageNumber - 1) * _PageSize).Take(_PageSize).ToListAsync().ConfigureAwait(false);


            await WrtiteAudittingLog(new MSC_AudittingLog { Description = "Load list MenuFunction", UserID = userId }, AtSerialNoConts.CODE_LOG_READ);

            return new Tuple<List<MenuFunctionDmOutput>, int, int>(listMenuFunction, totalCount, _PageSize);
        }
        public async Task<Tuple<MenuFunctionDmEditOutput, AtNotify>> GetEdit_MenuFunctionAsyns(string idMenuFunction, string userId)
        {
            var model = await _context.MenuFunction.Select(c => new MenuFunctionDmEditOutput
            {
                Id = c.Id,
                AcctionName = c.AcctionName,
                AcctionNameView = c.AcctionNameView,
                ControllerName = c.ControllerName,
                ControllerNameView = c.ControllerNameView,
                FK_MenuSubGroup = c.FK_MenuSubGroup,
                IsMenu = c.IsMenu,
                IsPublic = c.IsPublic,
                SortIndex = c.SortIndex,
                Title = c.Title,
                Note = c.Note
            }).FirstOrDefaultAsync(c => c.Id == idMenuFunction);

            if (model == null)
            {
                return new Tuple<MenuFunctionDmEditOutput, AtNotify>(model, AtNotify.NotFound);
            }

            await WrtiteAudittingLog(new MSC_AudittingLog { Description = "Tải dữ liệu chỉnh sửa", UserID = userId }, AtSerialNoConts.CODE_LOG_READ);

            return new Tuple<MenuFunctionDmEditOutput, AtNotify>(model, AtNotify.GetComplete);
        }

        public async Task<List<AtMenuFunction_RoleDm_EditMenuFunction>> GetListListRoleWithIdMenuFuntion(string idMenuFunction,string userId)
        {
            var listMenu_Role = await _context.MenuFunction_Role.Where(c => c.FK_MenuFunction == idMenuFunction)
                               .Select(c => new AtMenuFunction_RoleDm_EditMenuFunction
                               {
                                   Code = c.FK_RoleNavigation.Code,
                                   RoleName = c.FK_RoleNavigation.RoleName
                               }).OrderBy(h => h.Code).ToListAsync().ConfigureAwait(false);

            await WrtiteAudittingLog(new MSC_AudittingLog { Description = "Tải dữ liệu quyền theo ID: "+ idMenuFunction, UserID = userId }, AtSerialNoConts.CODE_LOG_READ);

            return listMenu_Role;
        }

        public async Task<List<AtMenuFunction_AccountDmListEditMenuFunction>> GetListListAccountWithIdMenuFuntion(string idMenuFunction, string userId)
        {
            var listMenu_Account = await _context.MenuFunction_Account.Where(c => c.FK_MenuFunction == idMenuFunction)
                        .Select(c => new AtMenuFunction_AccountDmListEditMenuFunction
                        {
                            Code = c.FK_AccountObjectNavigation.AccountCode,
                            AccountName = c.FK_AccountObjectNavigation.AccountObjectName,
                            Username = c.FK_AccountObjectNavigation.UserName
                        }).OrderBy(h => h.Code).ToListAsync().ConfigureAwait(false);

            await WrtiteAudittingLog(new MSC_AudittingLog { Description = "Tải dữ liệu tài khoản theo ID: " + idMenuFunction, UserID = userId }, AtSerialNoConts.CODE_LOG_READ);

            return listMenu_Account;
        }

        public async Task<AtNotify> DeleteMenuFuntionAsyns(string idMenuFunction, string userId)
        {
            try
            {
                var model = await _context.MenuFunction.FirstOrDefaultAsync(c => c.Id == idMenuFunction);
                if (model == null)
                {
                    return AtNotify.NotFound;
                }
                model.Status = (int)AtRowStatus.Hide;

                await _context.SaveChangesAsync();

                string data_Old = JsonConvert.SerializeObject(model);
                await WrtiteAudittingLog(new MSC_AudittingLog { Description = "Xóa MenuFunction theo ID: " + idMenuFunction, UserID = userId , Data_Old = data_Old }, AtSerialNoConts.CODE_LOG_DELETE);

                return AtNotify.DeleteComplete;
            }
            catch (Exception ex)
            {
                return AtNotify.DeleteFail;
            }
        }

        public async Task<MenuFunctionDmOutput> CreateMenuFuntionAsyns(MenuFunctionDmCreatetInputOrEdit input ,string userId)
        {
            try
            {
                var mapper = _mapper.Map<MenuFunction>(input);
                mapper.Id = await GetSerialCode(AtSerialNoConts.CODE_MENUFUNCTION);
                mapper.CreateDate = GetDateTimeFromServer();
                var result = await _context.MenuFunction.AddAsync(mapper);
                await _context.SaveChangesAsync();
                var output = _mapper.Map<MenuFunctionDmOutput>(result.Entity);

                var subMenu = await _context.MenuFunctionSubGroup.FirstOrDefaultAsync(c => c.Id == result.Entity.FK_MenuSubGroup).ConfigureAwait(false);
                output.NameMenuSubGroup = subMenu.SubGroupName;
                var MenuGroup = await _context.MenuFunctionGroup.FirstOrDefaultAsync(c => c.Id == subMenu.FK_MenuGroup).ConfigureAwait(false);

                output.NameMenuGroup = MenuGroup.GroupName;

                string data_new = JsonConvert.SerializeObject(output);
                await WrtiteAudittingLog(new MSC_AudittingLog { Description = "Thêm mới MenuFunction " + output.Id, UserID = userId, Data_New = data_new }, AtSerialNoConts.CODE_LOG_CREATE);
                return output;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<AtNotify> EditMenuFuntionAsyns(MenuFunctionDmCreatetInputOrEdit input, string userId)
        {
            try
            {
                var model = await _context.MenuFunction.FirstOrDefaultAsync(c => c.Id == input.Id);
                if (model == null)
                {
                    return AtNotify.NotFound;
                }

                string data_old = JsonConvert.SerializeObject(model);

                var mapper = _mapper.Map(input, model);

                await _context.SaveChangesAsync();

                string data_new = JsonConvert.SerializeObject(mapper);

                await WrtiteAudittingLog(new MSC_AudittingLog { Description = "Chỉnh sửa MenuFunction " + input.Id, UserID = userId, Data_New = data_new , Data_Old = data_old }, AtSerialNoConts.CODE_LOGC_UPDATE);
                return AtNotify.UpdateCompelete;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<MenuRoleOuput> GetListMenuRoleAsync(string idRole)
        {
            var listMenuGroud = await _context.MenuFunctionGroup.Where(c => c.Status == (int)AtRowStatus.Normal).AsNoTracking().ToListAsync();
            var listMenu = await _context.MenuFunctionSubGroup.Where(c => c.Status == (int)AtRowStatus.Normal).Select(c =>
                                          new ListSubMenuFuntion
                                          {

                                              IdMenuGroud = c.FK_MenuGroup,
                                              NameGroup = c.FK_MenuGroupNavigation.GroupName,

                                              IdSubMenu = c.Id,
                                              NameSubName = c.SubGroupName,

                                              MenuFunctions = c.MenuFunction.Where(c => c.Status == (int)AtRowStatus.Normal).Select(lc => new ListMenuFuntion
                                              {
                                                  IdMenuFuntion = lc.Id,
                                                  NameMenuFuntion = lc.Title,
                                                  IsCheck = false
                                              }).ToList()

                                          }).AsNoTracking()
                                        .ToListAsync();

            var MenuFuntion = new MenuRoleOuput();
            MenuFuntion.ListMenuChucNang = new List<GroupMenuChucNang>();

            foreach (var itemGroud in listMenuGroud)
            {
                var menuFuntion = new GroupMenuChucNang();
                menuFuntion.IdMenuGroud = itemGroud.Id;
                menuFuntion.NameGroup = itemGroud.GroupName;
                menuFuntion.SubFunctions = listMenu.Where(c => c.IdMenuGroud == itemGroud.Id).ToList();
                MenuFuntion.ListMenuChucNang.Add(menuFuntion);
            }

            var list_MenuFuntionRole = await _context.MenuFunction_Role
                                                .Where(c => c.FK_Role == idRole)
                                                .Select(c => c.FK_MenuFunction)
                                                .AsNoTracking()
                                                .ToListAsync();

            foreach (var item in list_MenuFuntionRole)
            {
                foreach (var itemMenu in MenuFuntion.ListMenuChucNang)
                {
                    foreach (var itemSub in itemMenu.SubFunctions)
                    {
                        foreach (var itemFun in itemSub.MenuFunctions)
                        {
                            if (item == itemFun.IdMenuFuntion)
                            {
                                itemFun.IsCheck = true;
                            }
                        }
                    }
                }
            }

            MenuFuntion.IdRole = idRole;

            var roleName = await _context.Role.FirstOrDefaultAsync(c => c.Id == idRole);
            MenuFuntion.NameRole = roleName.RoleName;

            return MenuFuntion;
        }



        public async Task<string> SaveMenuRoleAsync(MenuRoleInput input,string userId)
        {
            try
            {
                var now = GetDateTimeFromServer();

                var list_MenuFuntionRole = await _context.MenuFunction_Role
                                                    .Where(c => c.FK_Role == input.IdRole)
                                                    .AsNoTracking()
                                                    .ToListAsync();

                var listFuntion = new List<ListMenuFuntion>();
                var newItem = new List<MenuFunction_Role>();
                foreach (var item in input.ListMenuChucNang)
                {
                    foreach (var itemSub in item.SubFunctions)
                    {
                        if (itemSub.MenuFunctions != null)
                        {
                            listFuntion.AddRange(itemSub.MenuFunctions.Where(c => c.IsCheck == true).ToList());
                        }

                    }
                }



                foreach (var itemfunAccount in list_MenuFuntionRole)
                {
                    var check = listFuntion.FirstOrDefault(c => c.IdMenuFuntion == itemfunAccount.FK_MenuFunction);
                    if (check == null)
                    {
                        _context.MenuFunction_Role.Remove(itemfunAccount);
                    }
                }


                foreach (var itemFuntion in listFuntion)
                {
                    var check = list_MenuFuntionRole
                        .FirstOrDefault(c => c.FK_Role == input.IdRole
                        && c.FK_MenuFunction == itemFuntion.IdMenuFuntion);

                    if (check == null)
                    {
                        newItem.Add(new MenuFunction_Role
                        {
                            FK_Role = input.IdRole,
                            FK_MenuFunction = itemFuntion.IdMenuFuntion,
                            CreateDate = now,
                        });
                    }
                }
                await _context.MenuFunction_Role.AddRangeAsync(newItem);

                await _context.SaveChangesAsync();

                await WrtiteAudittingLog(new MSC_AudittingLog { Description = "Phân quyền theo ROLE ID: " + input.IdRole, UserID = userId}, AtSerialNoConts.CODE_LOGC_UPDATE);
                
                return input.IdRole;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<MenuAccountOuput> GetListMenuAccountAsync(string idAccount)
        {
            try
            {

                var listMenuGroud = await _context.MenuFunctionGroup.Where(c=>c.Status == (int)AtRowStatus.Normal).AsNoTracking().ToListAsync();
                var listMenu = await _context.MenuFunctionSubGroup.Where(c => c.Status == (int)AtRowStatus.Normal).Select(c =>
                                            new ListSubMenuFuntion
                                            {

                                                IdMenuGroud = c.FK_MenuGroup,
                                                NameGroup = c.FK_MenuGroupNavigation.GroupName,

                                                IdSubMenu = c.Id,
                                                NameSubName = c.SubGroupName,

                                                MenuFunctions = c.MenuFunction.Where(c => c.Status == (int)AtRowStatus.Normal).Select(lc => new ListMenuFuntion
                                                {
                                                    IdMenuFuntion = lc.Id,
                                                    NameMenuFuntion = lc.Title,
                                                    IsCheck = false
                                                }).ToList()

                                            }).AsNoTracking()
                                            .ToListAsync();

                var MenuFuntion = new MenuAccountOuput();
                MenuFuntion.ListMenuChucNang = new List<GroupMenuChucNang>();

                foreach (var itemGroud in listMenuGroud)
                {
                    var menuFuntion = new GroupMenuChucNang();
                    menuFuntion.IdMenuGroud = itemGroud.Id;
                    menuFuntion.NameGroup = itemGroud.GroupName;
                    menuFuntion.SubFunctions = listMenu.Where(c => c.IdMenuGroud == itemGroud.Id).ToList();
                    MenuFuntion.ListMenuChucNang.Add(menuFuntion);
                }

                var listID_MenuFuntionAccount = await _context.MenuFunction_Account
                                                    .Where(c => c.FK_AccountObject == idAccount)
                                                    .Select(c => c.FK_MenuFunction)
                                                    .AsNoTracking()
                                                    .ToListAsync();

                foreach (var item in listID_MenuFuntionAccount)
                {
                    foreach (var itemMenu in MenuFuntion.ListMenuChucNang)
                    {
                        foreach (var itemSub in itemMenu.SubFunctions)
                        {
                            foreach (var itemFun in itemSub.MenuFunctions)
                            {
                                if (item == itemFun.IdMenuFuntion)
                                {
                                    itemFun.IsCheck = true;
                                }
                            }
                        }
                    }
                }


                MenuFuntion.IdAccount = idAccount;
                return MenuFuntion;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public async Task<string> SaveMenuAccountAsync(MenuAccountInput input, string userId)
        {
            try
            {
                var now = GetDateTimeFromServer();

                var list_MenuFuntionAccount = await _context.MenuFunction_Account
                                                    .Where(c => c.FK_AccountObject == input.IdAccount)
                                                    .AsNoTracking()
                                                    .ToListAsync();

                var listFuntion = new List<ListMenuFuntion>();
                var newItem = new List<MenuFunction_Account>();
                foreach (var item in input.ListMenuChucNang)
                {
                    foreach (var itemSub in item.SubFunctions)
                    {
                        if (itemSub.MenuFunctions != null)
                        {
                            listFuntion.AddRange(itemSub.MenuFunctions.Where(c => c.IsCheck == true).ToList());
                        }
                    }
                }



                foreach (var itemfunAccount in list_MenuFuntionAccount)
                {
                    var check = listFuntion.FirstOrDefault(c => c.IdMenuFuntion == itemfunAccount.FK_MenuFunction);
                    if (check == null)
                    {
                        _context.MenuFunction_Account.Remove(itemfunAccount);
                    }
                }


                foreach (var itemFuntion in listFuntion)
                {
                    var check = list_MenuFuntionAccount
                        .FirstOrDefault(c => c.FK_AccountObject == input.IdAccount
                        && c.FK_MenuFunction == itemFuntion.IdMenuFuntion);

                    if (check == null)
                    {
                        newItem.Add(new MenuFunction_Account
                        {
                            FK_AccountObject = input.IdAccount,
                            FK_MenuFunction = itemFuntion.IdMenuFuntion,
                            CreateDate = now,
                        });
                    }
                }
                await _context.MenuFunction_Account.AddRangeAsync(newItem);

                await _context.SaveChangesAsync();
                await WrtiteAudittingLog(new MSC_AudittingLog { Description = "Phân quyền theo Account ID: " + input.IdAccount, UserID = userId }, AtSerialNoConts.CODE_LOGC_UPDATE);

                return input.IdAccount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
