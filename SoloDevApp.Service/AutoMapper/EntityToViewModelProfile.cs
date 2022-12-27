using AutoMapper;
using Newtonsoft.Json;
using SoloDevApp.Repository.Models;
using SoloDevApp.Service.ViewModels;
using System.Collections.Generic;

namespace SoloDevApp.Service.AutoMapper
{
    public class EntityToViewModelProfile : Profile
    {
        public EntityToViewModelProfile()
        {
            CreateMap<Skill, SkillViewModel>();
            //  CreateMap<NhomQuyen, NhomQuyenViewModel>().ForMember(dest => dest.DanhSachQuyen, m => m.MapFrom(src => JsonConvert.DeserializeObject<List<string>>(src.DanhSachQuyen)));
            CreateMap<NguoiDung, NguoiDungViewModel>();
       
            CreateMap<BinhLuan, BinhLuanViewModel>();
            CreateMap<Banner, BannerViewModel>();
            CreateMap<SanPham, SanPhamViewModel>();
            CreateMap<DanhMucSanPham, DanhMucSanPhamViewModel>();
            CreateMap<DonDatHang, DonDatHangViewModel>();
            CreateMap<CuaHang, CuaHangViewModel>();

        }
    }
}