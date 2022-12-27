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
    public interface IBinhLuanService : IService<BinhLuan, BinhLuanViewModel>
    {
         Task<ResponseEntity> GetBinhLuanTheoCongViec(string MaCongViec);

    }

    public class BinhLuanService : ServiceBase<BinhLuan, BinhLuanViewModel>, IBinhLuanService
    {
        private readonly IBinhLuanRepository _binhLuanRepository;

        private readonly INguoiDungRepository _nguoiDungRepository;

        private readonly IAppSettings _appSettings;
        private readonly IFileService _fileService;

        public BinhLuanService(IBinhLuanRepository binhLuanRepository,
     
            INguoiDungRepository nguoiDungRepository,
      
        IMapper mapper,
            IAppSettings appSettings,
            IFileService fileService
            )
            : base(binhLuanRepository, mapper)
        {
            _binhLuanRepository = binhLuanRepository;
        
            _nguoiDungRepository = nguoiDungRepository;
            _appSettings = appSettings;
            _fileService = fileService;


        }

        public async Task<ResponseEntity> GetBinhLuanTheoCongViec(string MaCongViec)
        {
            try
            {
                IEnumerable<BinhLuan> dsBinhLuan = await _binhLuanRepository.GetMultiByConditionAsync("MaCongViec", MaCongViec);

               List<GetBinhLuanView> lstBinhLuanView = new List<GetBinhLuanView>();
                foreach (BinhLuan binhLuan in dsBinhLuan)
                {
                    NguoiDung nguoiDung = await _nguoiDungRepository.GetByIdAsync(binhLuan.MaNguoiBinhLuan);

                    if (nguoiDung != null)
                    {
                        GetBinhLuanView binhLuanVew = new GetBinhLuanView();
                        binhLuanVew.NoiDung = binhLuan.NoiDung;
                        binhLuanVew.SaoBinhLuan = binhLuan.SaoBinhLuan;
                        binhLuanVew.NgayBinhLuan = binhLuan.NgayBinhLuan;
                        binhLuanVew.TenNguoiBinhLuan = nguoiDung.Name;
                        binhLuanVew.Avatar = nguoiDung.Avatar;

                        lstBinhLuanView.Add(binhLuanVew);
                    }else
                    {
                        await _binhLuanRepository.DeleteByIdAsync(binhLuan.Id);
                    }
                  
                }

                return new ResponseEntity(StatusCodeConstants.OK, lstBinhLuanView);
            }
            catch
            {
                return new ResponseEntity(StatusCodeConstants.ERROR_SERVER);
            }
        }


    }
}