using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SoloDevApp.Repository.Models;
using SoloDevApp.Repository.Repositories;
using SoloDevApp.Service.Constants;
using SoloDevApp.Service.Helpers;
using SoloDevApp.Service.Infrastructure;
using SoloDevApp.Service.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SoloDevApp.Service.Services
{
    public interface INguoiDungService : IService<NguoiDung, NguoiDungViewModel>
    {
        Task<ResponseEntity> GetByName(string TenNguoiDung);
        Task<ResponseEntity> ThemNguoiDung(ThongTinNguoiDung model);
        Task<ResponseEntity> SuaNguoiDung(int id, CapNhatNguoiDung model);
        Task<ResponseEntity> LayTatCa();
        Task<ResponseEntity> LayChiTiet(int id);
        Task<ResponseEntity> DangKy(ThongTinNguoiDung model);
        Task<ResponseEntity> DangNhap(DangNhapView model);
        Task<ResponseEntity> UploadAvatar(string nguoiDungId, Photo file);


    }

    public class NguoiDungService : ServiceBase<NguoiDung, NguoiDungViewModel>, INguoiDungService
    {
        private readonly INguoiDungRepository _nguoiDungRepository;
        private readonly IAppSettings _appSettings;
        private readonly IFileService _fileService;

        public NguoiDungService(INguoiDungRepository nguoiDungRepository, 
            IMapper mapper,
            IAppSettings appSettings,
            IFileService fileService
            )
            : base(nguoiDungRepository, mapper)
        {
            _nguoiDungRepository = nguoiDungRepository;
            _appSettings = appSettings;
            _fileService = fileService;
        }
        public async Task<ResponseEntity> UploadAvatar(string nguoiDungId, Photo file)
        {
            try
            {
                NguoiDung nguoiDung = await _nguoiDungRepository.GetByIdAsync(int.Parse(nguoiDungId));

                if(nguoiDung == null)
                    return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, "Không tìm thấy người dùng");

                if (file.formFile == null)
                    return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, "Không tìm thấy hình để thêm");
                
                //kiem tra dugn luong hinh chi cho phep hinh < 1Mb
                if (file.formFile.Length > 1000000)
                    return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, "Dung lượng hình phải dưới 1Mb");

                if(file.formFile.ContentType != "image/jpg" && file.formFile.ContentType != "image/jpeg" && file.formFile.ContentType != "image/png" && file.formFile.ContentType != "image/gif")
                    return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, "Chỉ cho phép dịnh dạng (jpg, jpeg, png, gif)");

                string filePath = "";
               
               filePath = await _fileService.SaveFileAsync(file.formFile, "avatar");

                nguoiDung.Avatar = _appSettings.UrlMain + filePath;
                await _nguoiDungRepository.UpdateAsync(nguoiDung.Id, nguoiDung);

                return new ResponseEntity(StatusCodeConstants.OK, nguoiDung);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

     

        public async Task<ResponseEntity> DangNhap(DangNhapView model)
        {
            try
            {
                List<KeyValuePair<string, dynamic>> columns = new List<KeyValuePair<string, dynamic>>();
                columns.Add(new KeyValuePair<string, dynamic>("Email", model.Email));
                columns.Add(new KeyValuePair<string, dynamic>("Password", model.Password));

                NguoiDung nguoiDung = await _nguoiDungRepository.GetSingleAsync(columns);
                if(nguoiDung == null)
                    return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, "Email hoặc mật khẩu không đúng !");

                nguoiDung.Password = "";
                NguoiDungLoginView nguoiDungLoginView = new NguoiDungLoginView();
                nguoiDungLoginView.user = _mapper.Map<NguoiDungViewModel>(nguoiDung);
                nguoiDungLoginView.token = await GenerateToken(nguoiDung);


                return new ResponseEntity(StatusCodeConstants.OK, nguoiDungLoginView);
            }
            catch
            {
                return new ResponseEntity(StatusCodeConstants.ERROR_SERVER);
            }
        }


        public async Task<ResponseEntity> DangKy(ThongTinNguoiDung model)
        {
            try
            {
                List<KeyValuePair<string, dynamic>> columns = new List<KeyValuePair<string, dynamic>>();
                columns.Add(new KeyValuePair<string, dynamic>("Email", model.Email));
                NguoiDung checkNguoiDung = await _nguoiDungRepository.GetSingleAsync(columns);
                if(checkNguoiDung != null)
                    return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, "Email đã tồn tại !");

                NguoiDung nguoiDung = new NguoiDung();
                nguoiDung.Name = model.Name;
                nguoiDung.Email = model.Email;
                nguoiDung.Phone = model.Phone;
                nguoiDung.Password = model.Password;
                nguoiDung.Birthday = model.Birthday;
                nguoiDung.Gender = model.Gender;
                nguoiDung.Role = "USER";
       

                await _nguoiDungRepository.InsertAsync(nguoiDung);

                return new ResponseEntity(StatusCodeConstants.OK, nguoiDung);
            }
            catch
            {
                return new ResponseEntity(StatusCodeConstants.ERROR_SERVER);
            }
        }

        public async Task<ResponseEntity> LayChiTiet(int id)
        {
            try
            {
                NguoiDung nguoiDung = await _nguoiDungRepository.GetByIdAsync(id);
                if(nguoiDung == null)
                    return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, "Người dùng không tồn tại !");

                nguoiDung.Password = "";

                NguoiDungViewModel nguoiDungNew = _mapper.Map<NguoiDungViewModel>(nguoiDung);

                return new ResponseEntity(StatusCodeConstants.OK, nguoiDungNew);
            }
            catch
            {
                return new ResponseEntity(StatusCodeConstants.ERROR_SERVER);
            }
        }

        public async Task<ResponseEntity> LayTatCa()
        {
            try
            {
                IEnumerable<NguoiDung> dsNguoiDung = await _nguoiDungRepository.GetAllAsync();

                List<NguoiDungViewModel> lstNguoiDungNew = new List<NguoiDungViewModel>();

                foreach (NguoiDung nguoiDung in dsNguoiDung)
                {
                    NguoiDungViewModel nguoiDungNew = new NguoiDungViewModel();
                    nguoiDungNew.Id = nguoiDung.Id;
                    nguoiDungNew.Name = nguoiDung.Name;
                    nguoiDungNew.Email = nguoiDung.Email;
                    nguoiDungNew.Password = "";
                    nguoiDungNew.Birthday = nguoiDung.Birthday;
                    nguoiDungNew.Gender = nguoiDung.Gender;
                    nguoiDungNew.Role = nguoiDung.Role;
                   
                    lstNguoiDungNew.Add(nguoiDungNew);
                }

                

                return new ResponseEntity(StatusCodeConstants.OK, lstNguoiDungNew);
            }
            catch
            {
                return new ResponseEntity(StatusCodeConstants.ERROR_SERVER);
            }
        }

        public async Task<ResponseEntity> ThemNguoiDung(ThongTinNguoiDung model)
        {
            try
            {
                List<KeyValuePair<string, dynamic>> columns = new List<KeyValuePair<string, dynamic>>();
                columns.Add(new KeyValuePair<string, dynamic>("Email", model.Email));
                NguoiDung checkNguoiDung = await _nguoiDungRepository.GetSingleAsync(columns);
                if (checkNguoiDung != null)
                    return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, "Email đã tồn tại !");

                NguoiDung nguoiDung = new NguoiDung();
                nguoiDung.Name = model.Name;
                nguoiDung.Email = model.Email;
                nguoiDung.Phone= model.Phone;
                nguoiDung.Password = model.Password;
                nguoiDung.Birthday = model.Birthday;
                nguoiDung.Gender = model.Gender;
                nguoiDung.Role = model.Role;
              

                await _nguoiDungRepository.InsertAsync(nguoiDung);

                return new ResponseEntity(StatusCodeConstants.OK, nguoiDung);
            }
            catch
            {
                return new ResponseEntity(StatusCodeConstants.ERROR_SERVER);
            }
        }

        public async Task<ResponseEntity> SuaNguoiDung(int id, CapNhatNguoiDung model)
        {
            try
            {

                NguoiDung nguoiDung = await _nguoiDungRepository.GetByIdAsync(id);
                if(nguoiDung == null)
                    return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, "Người dùng không tồn tại !");

                List<KeyValuePair<string, dynamic>> columns = new List<KeyValuePair<string, dynamic>>();
                columns.Add(new KeyValuePair<string, dynamic>("Email", model.Email));
                NguoiDung checkNguoiDung = await _nguoiDungRepository.GetSingleAsync(columns);
                if (checkNguoiDung != null && checkNguoiDung.Id != nguoiDung.Id)
                    return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, "Email đã tồn tại !");

                nguoiDung.Name = model.Name;
                nguoiDung.Email = model.Email;
                nguoiDung.Phone = model.Phone;

                nguoiDung.Birthday = model.Birthday;
                nguoiDung.Gender = model.Gender;
                nguoiDung.Role = model.Role;
              

                await _nguoiDungRepository.UpdateAsync(nguoiDung.Id, nguoiDung);

                return new ResponseEntity(StatusCodeConstants.OK, nguoiDung);
            }
            catch
            {
                return new ResponseEntity(StatusCodeConstants.ERROR_SERVER);
            }
        }

        public async Task<ResponseEntity> GetByName(string TenNguoiDung)
        {
            try
            {
              
                string formatName = TenNguoiDung.ToLower().Trim();
                IEnumerable<NguoiDung> entity = await _nguoiDungRepository.GetAllAsync();

                List<NguoiDung> nguoiDung = entity.Where(n => n.Name.Contains(formatName)).ToList();


             

                return new ResponseEntity(StatusCodeConstants.OK, nguoiDung);
            }
            catch
            {
                return new ResponseEntity(StatusCodeConstants.ERROR_SERVER);
            }
        }

        private async Task<string> GenerateToken(NguoiDung entity)
        {
            try
            {

                DateTime dNow = DateTime.Now;

                var arrInfo = new List<Claim> {
                new Claim("id", entity.Id.ToString()),
                new Claim("email", entity.Email),
                new Claim("role",entity.Role)

            };

                SymmetricSecurityKey SIGNING_KEY = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_appSettings.Secret));
                var token = new JwtSecurityToken(
                            claims: arrInfo,
                            notBefore: dNow,
                            expires: dNow.AddDays(7),
                            signingCredentials: new SigningCredentials(SIGNING_KEY, SecurityAlgorithms.HmacSha256)
                    );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}