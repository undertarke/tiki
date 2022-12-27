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
    public interface ISanPhamService : IService<SanPham, SanPhamViewModel>
    {
      
        Task<ResponseEntity> UploadHinhAnh(string sanPhamId, Photo file);

    }

    public class SanPhamService : ServiceBase<SanPham, SanPhamViewModel>, ISanPhamService
    {
        private readonly ISanPhamRepository _sanPhamRepository;
        private readonly IFileService _fileService;
        private readonly IAppSettings _appSettings;

        public SanPhamService(ISanPhamRepository sanPhamRepository,
            IFileService fileService,
            IAppSettings appSettings,
            IMapper mapper)
            : base(sanPhamRepository, mapper)
        {
            _sanPhamRepository = sanPhamRepository;
            _fileService = fileService;
            _appSettings = appSettings;
        }


        public async Task<ResponseEntity> UploadHinhAnh(string sanPhamId, Photo file)
        {
            try
            {
                SanPham sanPham = await _sanPhamRepository.GetByIdAsync(int.Parse(sanPhamId));

                if (sanPham == null)
                    return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, "Không tìm thấy sản phẩm");

                if (file.formFile == null)
                    return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, "Không tìm thấy hình để thêm");

                //kiem tra dugn luong hinh chi cho phep hinh < 1Mb
                if (file.formFile.Length > 1000000)
                    return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, "Dung lượng hình phải dưới 1Mb");

                if (file.formFile.ContentType != "image/jpg" && file.formFile.ContentType != "image/jpeg" && file.formFile.ContentType != "image/png" && file.formFile.ContentType != "image/gif")
                    return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, "Chỉ cho phép dịnh dạng (jpg, jpeg, png, gif)");

                string filePath = "";

                filePath = await _fileService.SaveFileAsync(file.formFile, "images");

                sanPham.HinhAnh = _appSettings.UrlMain + filePath;
                await _sanPhamRepository.UpdateAsync(sanPham.Id, sanPham);

                return new ResponseEntity(StatusCodeConstants.OK, sanPham);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}