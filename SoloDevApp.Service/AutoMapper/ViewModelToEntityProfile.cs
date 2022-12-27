using AutoMapper;
using Newtonsoft.Json;
using SoloDevApp.Repository.Models;
using SoloDevApp.Service.ViewModels;

namespace SoloDevApp.Service.AutoMapper
{
    public class ViewModelToEntityProfile : Profile
    {
        public ViewModelToEntityProfile()
        {
            CreateMap< SkillViewModel, Skill>();
            // CreateMap<NhomQuyenViewModel, NhomQuyen>().ForMember(dest => dest.DanhSachQuyen, m => m.MapFrom(src => JsonConvert.SerializeObject(src.DanhSachQuyen)));
            CreateMap<NguoiDungViewModel, NguoiDung>();
          
            CreateMap<BinhLuanViewModel, BinhLuan>();
            CreateMap<BannerViewModel, Banner>();
            CreateMap<SanPhamViewModel, SanPham>();
            CreateMap<DanhMucSanPhamViewModel, DanhMucSanPham>();
            CreateMap<DonDatHangViewModel, DonDatHang>();
            CreateMap<CuaHangViewModel, CuaHang>();


        }
    }
}