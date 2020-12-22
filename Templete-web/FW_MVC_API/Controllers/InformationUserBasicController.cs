using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using FW_MVC_API.AtLogic;
using FW_MVC_API.Context;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static Domain.AccountObjectDm;
using static Domain.InformationUserDm;

namespace FW_MVC_API.Controllers
{
    [Authorize]
    public class InformationUserBasicController : AtBaseController
    {
        private static DataFrameworkWebContext _context;
        private static AtInformationUserLogic _logicInformation;
        private static AtAccountObjectLogic _logicAccountObject;

        public InformationUserBasicController(DataFrameworkWebContext context, AtInformationUserLogic logicInformation, AtAccountObjectLogic logicAccountObject)
        {
            _context = context;
            _logicInformation = logicInformation;
            _logicAccountObject = logicAccountObject;
        }

        // GET: InformationUser
        public ActionResult DanhSachInformation_Basic()
        {

            return View();
        }

        public async Task<IActionResult> Information_Read([DataSourceRequest] DataSourceRequest request)
        {
            var listInfromation = await _logicInformation.GetListInfromatiomUserAsync();
            return Json(listInfromation.ToDataSourceResult(request));
        }


        public async Task<JsonResult> ListCombobox_AccountObjectAsyns()
        {
            return Json(await _logicAccountObject.GetListCombobox_AccountObjectAsyns());
        }
        public async Task<JsonResult> ListCombobox_TypeAccountObjectAsyns()
        {
            var list = new List<TypeAccount>();
            list.Add(new TypeAccount
            {
                Id = EnumAccountType.UuTienCao,
                Name = "Ưu Tiên Cao",
            });

            list.Add(new TypeAccount
            {
                Id = EnumAccountType.UuTienThap,
                Name = "Ưu Tiên Thấp",
            });

            return Json(list);
        }
        public async Task<JsonResult> ListCombobox_StypeAccountObjectAsyns()
        {
            var list = new List<StypeAccount>();
            list.Add(new StypeAccount
            {
                Id = 1,
                Name = "Vip Bạc",
            });

            list.Add(new StypeAccount
            {
                Id = 2,
                Name = "Vip Vàng",
            });

            list.Add(new StypeAccount
            {
                Id = 2,
                Name = "Vip Bạch Kim",
            });

            return Json(list);

        }

        // GET: InformationUser/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: InformationUser/Create
        public async Task<IActionResult> CreateInformation_Basic()
        {
            ViewData["Fk_AccountObject"] = new SelectList(await _logicAccountObject.GetListCombobox_AccountObjectAsyns(), "Id", "AccountObjectName");
            return View();
        }

        // POST: InformationUser/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateInformation_Basic(InformationUserDmInput input)
        {
            try
            {
                var output = await _logicInformation.CreateInfromationUser(input);
                if (output != null)
                {
                    return RedirectToAction(nameof(DanhSachInformation_Basic));
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// Hàm tạo Infromation bằng cách call AJAX
        /// </summary>
        /// <param name="input"> Là Domain được truyền từ View  </param>
        /// <returns></returns>
        [HttpPost("tao-moi-thongtin-user")]
        public async Task<ActionResult<AtResult<InformationUserDmOutput>>> CreateInformation_AjaxCall([FromBody]InformationUserDmInput input)
        {
            try
            {
                var output = await _logicInformation.CreateInfromationUser(input);

                if (output != null)
                {
                    return new AtResult<InformationUserDmOutput>(output);
                }

                return new AtResult<InformationUserDmOutput>(Notify.InsertFail); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        // GET: InformationUser/Edit/5
        [HttpPost("Get-Data-Edit")]
        public async Task<ActionResult<AtResult<InformationUserDmInput>>> GetDataEdit(string idInformation)
        {
            if (!string.IsNullOrEmpty(idInformation))
            {
                return new AtResult<InformationUserDmInput>(await _logicInformation.GetInfromatiomUserAsync(idInformation)); 
            }
            return new AtResult<InformationUserDmInput>(Notify.NotFound);
        }

        [HttpPost("Edit-Information")]
        public async Task<ActionResult<AtResult<List<InformationUserDmOutput>>>> EditInfromation([FromBody]InformationUserDmInput input)
        {
            if (input == null)
            {
                return new AtResult<List<InformationUserDmOutput>> (Notify.NotFound);
            }

            var tupperInformation = await _logicInformation.UpdateInfromatiomUserAsync(input);

            if (tupperInformation.Item2 == Notify.PhienGiaoDichHetHan)
            {
                return new AtResult<List<InformationUserDmOutput>>(Notify.PhienGiaoDichHetHan);
            }
            else if (tupperInformation.Item2 == Notify.NotFound)
            {
                return new AtResult<List<InformationUserDmOutput>>(Notify.NotFound);
            }
            else
            {
                return new AtResult<List<InformationUserDmOutput>>(tupperInformation.Item1);
            }
        }

        /// <summary>
        /// Hàm xóa thông tin tài  khoản call bằng ajax
        /// </summary>
        /// <param name="input_Delete"></param>
        /// <returns></returns>
        [HttpPost("DeleteInformationAjax")]
        public async Task<ActionResult<AtResult<InformationUserDmInput_Delete>>> DeleteInfromationUser_CallAjax([FromBody]InformationUserDmInput_Delete input_Delete)
        {
            if (input_Delete != null)
            {
                var notify = await _logicInformation.DeleteInfromationUser(input_Delete);
                if (notify == Notify.PhienGiaoDichHetHan)
                {
                    return new AtResult<InformationUserDmInput_Delete>(Notify.PhienGiaoDichHetHan);
                }
                return new AtResult<InformationUserDmInput_Delete>(input_Delete);
            }
            else
            {
                return new AtResult<InformationUserDmInput_Delete>(Notify.NotFound);
            }
        }
    }
}
