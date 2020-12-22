using AtDomain;
using AtTempleteWeb_API.Context;
using AtTempleteWeb_API.Entires;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AtDomain.AtMenuFuntionDm;

namespace AtTempleteWeb_API.AtLogic
{
    public class AtPermissionMenuFunctionLogic : AtBaseLogic
    {
        public AtPermissionMenuFunctionLogic(AtTempleteWebContext context, IConfiguration config) : base(context, config)
        {
        }

        public async Task<GetMenuFuntionDmOutput> GetLeftMenu(string UserId)
        {
            // Cái này lấy all menu chưa phân quyền
            List<View_MenuFunction> listMenu = await _context.View_MenuFunction.Where(u => u.IdAccount == UserId && u.Status == (int)AtRowStatus.Normal).ToListAsync().ConfigureAwait(false);
            GetMenuFuntionDmOutput modelOutPut = new GetMenuFuntionDmOutput();
            modelOutPut.listGroup = new List<MenuHelper_MenuFunctionGroupOutput>();
            modelOutPut.listSubGroup = new List<MenuHelper_MenuFunctionSubGroupOutput>();
            modelOutPut.listMenu = new List<MenuHelper_MenuFunctionOutput>();
            foreach (var item in listMenu)
            {
                // Check MenuFunction nếu không tồn tại thì thêm vào
                if (!modelOutPut.listMenu.Exists(u => u.Id == item.Id))
                {
                    modelOutPut.listMenu.Add(new MenuHelper_MenuFunctionOutput
                    {
                        Id = item.Id,
                        Title = item.Title,
                        ControllerName = item.ControllerName,
                        AcctionName = item.AcctionName,
                        ControllerNameView = item.ControllerNameView,
                        AcctionNameView = item.AcctionNameView,
                        RouteId = item.RouteId,
                        IsMenu = item.IsMenu,
                        IsPublic = item.IsPublic,
                        Icon = item.Icon,
                        CssClass = item.CssClass,
                        Parrent = item.Parrent,
                        Status = item.Status,
                        CreateDate = item.CreateDate,
                        SortIndex = item.SortIndex,
                        FK_MenuSubGroup = item.FK_MenuSubGroup,
                        Note = item.Note
                    });
                }
                // Check MenuFunctionSubGroup nếu không tồn tại thì thêm vào
                if (!modelOutPut.listSubGroup.Exists(u => u.Id == item.IdSubGroup))
                {
                    modelOutPut.listSubGroup.Add(new MenuHelper_MenuFunctionSubGroupOutput
                    {
                        Id = item.IdSubGroup,
                        SubGroupName = item.SubGroupName,
                        SortIndex = item.SortIndexSubGroup,
                        Icon = item.IconSubGroup,
                        CssClass = item.CssClassSubGroup,
                        CreateDate = DateTime.Now,
                        Status = 0,
                        FK_MenuGroup = item.IdGroup
                    });
                }
                // Check MenuFunctionGroup
                if (!modelOutPut.listGroup.Exists(u => u.Id == item.IdGroup))
                {
                    modelOutPut.listGroup.Add(new MenuHelper_MenuFunctionGroupOutput
                    {
                        Id = item.IdGroup,
                        GroupName = item.GroupName,
                        Icon = item.IconGroup,
                        CssClass = item.CssClassGroup,
                        SortIndex = item.SortIndexGroup,
                        CreateDate = DateTime.Now,
                        Status = 0
                    });
                }
            }

            return modelOutPut;
        }

        public async Task<List<MenuHelper_MenuFunctionPermissonOutput>> GetListMenuFuntionPermission(string UserId)
        {
            List<View_PermissionFunction> listPermission = await _context.View_PermissionFunction.Where(u => u.IdAccount == UserId).ToListAsync().ConfigureAwait(false);
            List<MenuHelper_MenuFunctionPermissonOutput> listPermisson = new List<MenuHelper_MenuFunctionPermissonOutput>();
            foreach (var item in listPermission)
            {
                // Check MenuFunction nếu không tồn tại thì thêm vào
                if (!listPermisson.Exists(u => u.Id == item.Id))
                {
                    listPermisson.Add(new MenuHelper_MenuFunctionPermissonOutput
                    {
                        Id = item.Id,
                        Title = item.Title,
                        ControllerName = item.ControllerName,
                        AcctionName = item.AcctionName,
                        ControllerNameView = item.ControllerNameView,
                        AcctionNameView = item.AcctionNameView,
                        RouteId = item.RouteId
                    });
                }
            }
            return listPermisson;
        }
    }
}
