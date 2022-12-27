using AutoMapper;
using SoloDevApp.Repository.Models;
using SoloDevApp.Repository.Repositories;
using SoloDevApp.Service.Constants;
using SoloDevApp.Service.Helpers;
using SoloDevApp.Service.Infrastructure;
using SoloDevApp.Service.ViewModels;
using System;
using System.Threading.Tasks;

namespace SoloDevApp.Service.Services
{
    public interface ICuaHangService : IService<CuaHang, CuaHangViewModel>
    {
        
        Task<ResponseEntity> UploadHinhAnh(string cuaHangId, Photo file);

    }

    public class CuaHangService : ServiceBase<CuaHang, CuaHangViewModel>, ICuaHangService
    {
        private readonly ICuaHangRepository _cuaHangRepository;
        private readonly IAppSettings _appSettings;
        private readonly IFileService _fileService;

        public CuaHangService(ICuaHangRepository cuaHangRepository,
             IAppSettings appSettings,
            IFileService fileService,
            IMapper mapper)
            : base(cuaHangRepository, mapper)
        {
            _cuaHangRepository = cuaHangRepository;
            _fileService = fileService;
            _appSettings = appSettings;
        }

        public async Task<ResponseEntity> UploadHinhAnh(string cuaHangId, Photo file)
        {
            try
            {
                CuaHang cuaHang = await _cuaHangRepository.GetByIdAsync(int.Parse(cuaHangId));

                if (cuaHang == null)
                    return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, "Không tìm thấy cửa hàng");

                if (file.formFile == null)
                    return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, "Không tìm thấy hình để thêm");

                //kiem tra dugn luong hinh chi cho phep hinh < 1Mb
                if (file.formFile.Length > 1000000)
                    return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, "Dung lượng hình phải dưới 1Mb");

                if (file.formFile.ContentType != "image/jpg" && file.formFile.ContentType != "image/jpeg" && file.formFile.ContentType != "image/png" && file.formFile.ContentType != "image/gif")
                    return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, "Chỉ cho phép dịnh dạng (jpg, jpeg, png, gif)");

                string filePath = "";

                filePath = await _fileService.SaveFileAsync(file.formFile, "avatar");

                cuaHang.Avatar = _appSettings.UrlMain + filePath;
                await _cuaHangRepository.UpdateAsync(cuaHang.Id, cuaHang);

                return new ResponseEntity(StatusCodeConstants.OK, cuaHang);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}