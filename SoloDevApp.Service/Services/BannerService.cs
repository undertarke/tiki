using AutoMapper;
using SoloDevApp.Repository.Models;
using SoloDevApp.Repository.Repositories;
using SoloDevApp.Service.Infrastructure;
using SoloDevApp.Service.ViewModels;
using System.Threading.Tasks;

namespace SoloDevApp.Service.Services
{
    public interface IBannerService : IService<Banner, BannerViewModel>
    {
        //Task<ResponseEntity> CheckPass(NguoiDungViewModel model);

    }

    public class BannerService : ServiceBase<Banner, BannerViewModel>, IBannerService
    {
        //private readonly INguoiDungRepository _nguoiDungRepository;

        public BannerService(IBannerRepository bannerRepository, IMapper mapper)
            : base(bannerRepository, mapper)
        {
        }

        /*  public async Task<ResponseEntity> CheckPass(NguoiDungViewModel model)
          {
              try
              {
                  NguoiDung entity = await _nguoiDungRepository.GetByIdAsync(model.Id);
                  if (entity == null)
                      return new ResponseEntity(StatusCodeConstants.NOT_FOUND);

                  if (model.MatKhau != entity.MatKhau)
                      return new ResponseEntity(StatusCodeConstants.OK, "0");

                  NguoiDungViewModel nguoiDung = _mapper.Map<NguoiDungViewModel>(entity);

                  return new ResponseEntity(StatusCodeConstants.OK, nguoiDung, MessageConstants.UPDATE_SUCCESS);
              }
              catch
              {
                  return new ResponseEntity(StatusCodeConstants.ERROR_SERVER);
              }
          }*/

   
    }
}