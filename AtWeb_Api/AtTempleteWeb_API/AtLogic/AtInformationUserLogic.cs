using AtDomain;
using AtTempleteWeb_API.Context;
using AtTempleteWeb_API.Entires;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AtDomain.AtFileAttachmentDm;
using static AtDomain.InformationUserDm;

namespace AtTempleteWeb_API.AtLogic
{
    public partial class AtInformationUserLogic : AtBaseLogic
    {
        private static readonly IMapper _mapper;
        private static int _PageSize = 0;

        static AtInformationUserLogic()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<InformationUserDmInput, InfomationUser>();
                cfg.CreateMap<InfomationUser, InformationUserDmInput>();
            });
            _mapper = config.CreateMapper();
        }

        public AtInformationUserLogic(AtTempleteWebContext context, IConfiguration config) : base(context, config)
        {
            _PageSize = Convert.ToInt32(_config["PageSize"]);
        }




        /// <summary>
        /// Load dữ liệu InformationUser theo Id
        /// </summary>
        /// <param name="idInformation"> Id </param>
        /// <returns> InformationUserDmInput </returns>
        public async Task<InformationUserDmInput> GetInfromatiomUserAsync(string idInformation)
        {
            var model = await _context.InfomationUser.Select(c => new InformationUserDmInput
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
            }).FirstOrDefaultAsync(a => a.Id == idInformation);

            if (model != null)
            {
                var listFileAttchment = await _context.FileAttachment.Where(c => c.RefID == idInformation).ToListAsync().ConfigureAwait(true);
                var fileAttachment = new List<FileAttachmentDmInput>();
                foreach (var file in listFileAttchment)
                {
                    fileAttachment.Add(new FileAttachmentDmInput
                    {
                        RefID = file.RefID,
                        AttachmentID = file.AttachmentID,
                        CreatedBy = file.CreatedBy,
                        FileName = file.FileName
                    });
                }

                model.ListFileAttchment = fileAttachment;

                return model;
            }

            return null;
        }


        /// <summary>
        /// Hàm update Informationuser
        /// </summary>
        /// <param name="input"> InformationUserDmInput </param>
        /// <returns> Tuper gồm danh sách Infromation được cập nhập và  Enum Notify là không báo lỗi </returns>
        public async Task<Tuple<InformationUserDmInput, AtNotify>> UpdateInfromatiomUserAsync(InformationUserDmInput input)
        {
            try
            {
                var model = await _context.InfomationUser.FirstOrDefaultAsync(c => c.Id == input.Id);

                if (model == null)
                {
                    return new Tuple<InformationUserDmInput, AtNotify>(null, AtNotify.NotFound);
                }

                if (!model.RowVesion.SequenceEqual(input.RowVesion))
                {
                    return new Tuple<InformationUserDmInput, AtNotify>(null, AtNotify.PhienGiaoDichHetHan);
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


                return new Tuple<InformationUserDmInput, AtNotify>(input, AtNotify.UpdateCompelete);
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
        public async Task<List<InformationUserDmOutput>> GetListInfromatiomUserAsync(int indexPage)
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
                UpdateDate = c.UpdateDate,
                Code = c.Code
            }).OrderBy(c => c.Loai).Skip(indexPage * _PageSize).Take(_PageSize).ToListAsync();
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
                        Code = modelMapper.Code
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
        public async Task<AtNotify> DeleteInfromationUser(InformationUserDmInput_Delete input)
        {
            try
            {
                if (input != null)
                {
                    var model = await _context.InfomationUser.FirstOrDefaultAsync(c => c.Id == input.Id);
                    if (!model.RowVesion.SequenceEqual(input.RowVesion))
                    {
                        return AtNotify.PhienGiaoDichHetHan;
                    }
                    _context.InfomationUser.Remove(model);
                    await _context.SaveChangesAsync();
                    return AtNotify.DeleteComplete;

                }
                else
                {
                    return AtNotify.NotFound;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


    }
}
