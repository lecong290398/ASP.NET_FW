using AutoMapper;
using Domain;
using FW_MVC_API.Context;
using FW_MVC_API.Helper;
using FW_MVC_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Domain.FileAttachmentDm;
using static Domain.InformationUserDm;

namespace FW_MVC_API.AtLogic
{
    public partial class AtInformationUserLogic : ATLogicBaseHelper
    {
        private static readonly IMapper _mapper;

        static AtInformationUserLogic()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<InformationUserDmInput, InfomationUser>();
                cfg.CreateMap<InfomationUser, InformationUserDmInput>();
            });
            _mapper = config.CreateMapper();
        }

        public AtInformationUserLogic(DataFrameworkWebContext context, IConfiguration config) : base(context, config)
        {
        }




        /// <summary>
        /// Load dữ liệu InformationUser theo Id
        /// </summary>
        /// <param name="idInformation"> Id </param>
        /// <returns> InformationUserDmInput </returns>
        public async Task<InformationUserDmInput> GetInfromatiomUserAsync(string idInformation)
        {

            var listFileAttchment = await _context.FileAttachment.Where(c => c.RefID == idInformation).ToListAsync().ConfigureAwait(true);
            var fileAttachment = new List<FileAttachmentDmInput>();
            foreach (var file in listFileAttchment)
            {
                fileAttachment.Add(new FileAttachmentDmInput
                {
                       RefID = file.RefID,
                       AttachmentID  = file.AttachmentID,
                       CreatedBy = file.CreatedBy,
                       FileName = file.FileName
                });
            }

            return await _context.InfomationUser.Select(c => new InformationUserDmInput
            {
                Loai = c.Loai,
                Id = c.Id,
                DiemSo = c.DiemSo,
                FistName = c.FistName,
                Fk_AccountObject = c.Fk_AccountObject,
                IsInactive = c.IsInactive,
                Kieu = c.Kieu,
                LastName = c.LastName,
                NgaySinh = c.NgaySinh,
                RowVesion = c.RowVesion,
                ListFileAttchment = fileAttachment
            }).FirstOrDefaultAsync(a => a.Id == idInformation);
        }


        /// <summary>
        /// Hàm update Informationuser
        /// </summary>
        /// <param name="input"> InformationUserDmInput </param>
        /// <returns> Tuper gồm danh sách Infromation được cập nhập và  Enum Notify là không báo lỗi </returns>
        public async Task<Tuple<List<InformationUserDmOutput>, Notify>> UpdateInfromatiomUserAsync(InformationUserDmInput input)
        {
            try
            {
                if (input != null)
                {
                    var model = await _context.InfomationUser.FirstOrDefaultAsync(c => c.Id == input.Id);
                    if (!model.RowVesion.SequenceEqual(input.RowVesion))
                    {
                        return new Tuple<List<InformationUserDmOutput>, Notify>(null, Notify.PhienGiaoDichHetHan);
                    }

                    //xóa file
                    if (input.ListFileRemove.Count() > 0)
                    {
                        await RemoveFileLogic(input.ListFileRemove, input.Id);

                    }

                    //Them mới file
                    if (input.listFileIds.Count() > 0)
                    {
                        await UploadFileLogic(input.Id, input.listFileIds, input.listFileNames, model.CreateBy);
                    }

                    model.FistName = input.FistName;
                    model.LastName = input.LastName;
                    model.Kieu = input.Kieu;
                    model.Loai = input.Loai;
                    model.DiemSo = input.DiemSo;
                    model.NgaySinh = input.NgaySinh;
                    model.IsInactive = input.IsInactive;
                    model.UpdateBy = "Admin";
                    model.UpdateDate = GetDateTimeFromServer();

                    await _context.SaveChangesAsync();

                    return new Tuple<List<InformationUserDmOutput>, Notify>(await GetListInfromatiomUserAsync(), Notify.UpdateCompelete);
                }
                return new Tuple<List<InformationUserDmOutput>, Notify>(null, Notify.NotFound);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        /// <summary>
        /// Load list informationUser
        /// </summary>
        /// <returns></returns>
        public async Task<List<InformationUserDmOutput>> GetListInfromatiomUserAsync()
        {
            return await _context.InfomationUser.Where(lc => lc.IsInactive == false).Select(c => new InformationUserDmOutput
            {
                Id = c.Id,
                CreateBy = c.CreateBy,
                CreateDate = c.CreateDate,
                DiemSo = c.DiemSo,
                FistName = c.FistName,
                Fk_AccountObject = c.Fk_AccountObject,
                IsInactive = c.IsInactive,
                Kieu = c.Kieu,
                LastName = c.LastName,
                Loai = c.Loai,
                NgaySinh = c.NgaySinh,
                RowVesion = c.RowVesion,
                UpdateBy = c.UpdateBy,
                UpdateDate = c.UpdateDate
            }).OrderBy(c => c.Loai).ToListAsync();
        }

        /// <summary>
        /// Hàm tạo mới Informtion
        /// </summary>
        /// <param name="input">InformationUserDmInput</param>
        /// <returns>InformationUserDmOutput</returns>
        public async Task<InformationUserDmOutput> CreateInfromationUser(InformationUserDmInput input)
        {
            try
            {
                if (input != null)
                {

                    //Khởi tạo dữ liệu Information
                    var modelMapper = _mapper.Map<InfomationUser>(input);
                    modelMapper.Id = Guid.NewGuid().ToString();
                    modelMapper.CreateBy = "Admin"; // Giã dữ liệu
                    modelMapper.CreateDate = GetDateTimeFromServer();
                    modelMapper.UpdateBy = "Admin"; // Giã dữ liệu
                    modelMapper.UpdateDate = GetDateTimeFromServer();
                    modelMapper.IsInactive = false;
                    modelMapper.Code = await GetSerialCode(AtSerialNoConts.CODE_INFORMATION);
                    await _context.InfomationUser.AddAsync(modelMapper);

                  
                    await UploadFileLogic(modelMapper.Id, input.listFileIds, input.listFileNames, modelMapper.CreateBy);

                    await _context.SaveChangesAsync();

                    return new InformationUserDmOutput
                    {
                        Id = modelMapper.Id,
                        CreateBy = modelMapper.CreateBy,
                        CreateDate = modelMapper.CreateDate,
                        DiemSo = modelMapper.DiemSo,
                        FistName = modelMapper.FistName,
                        Fk_AccountObject = modelMapper.Fk_AccountObject,
                        IsInactive = modelMapper.IsInactive,
                        Kieu = modelMapper.Kieu,
                        LastName = modelMapper.LastName,
                        Loai = modelMapper.Loai,
                        NgaySinh = modelMapper.NgaySinh,
                        RowVesion = modelMapper.RowVesion,
                        UpdateBy = modelMapper.UpdateBy,
                        UpdateDate = modelMapper.UpdateDate,
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Hàm delete Information 
        /// </summary>
        /// <param name="input"> InformationUserDmInput_Delete </param>
        /// <returns> Emun Notify thông báo trạng thái xử lý</returns>
        public async Task<Notify> DeleteInfromationUser(InformationUserDmInput_Delete input)
        {
            try
            {
                if (input != null)
                {
                    var model = await _context.InfomationUser.FirstOrDefaultAsync(c => c.Id == input.Id);
                    if (!model.RowVesion.SequenceEqual(input.RowVesion))
                    {
                        return Notify.PhienGiaoDichHetHan;
                    }
                    _context.InfomationUser.Remove(model);
                    await _context.SaveChangesAsync();
                    return Notify.DeleteComplete;

                }
                else
                {
                    return Notify.NotFound;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


    }
}
