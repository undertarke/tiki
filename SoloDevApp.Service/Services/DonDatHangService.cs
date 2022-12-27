using AutoMapper;
using SoloDevApp.Repository.Models;
using SoloDevApp.Repository.Repositories;
using SoloDevApp.Service.Infrastructure;
using SoloDevApp.Service.ViewModels;
using System.Threading.Tasks;

namespace SoloDevApp.Service.Services
{
    public interface IDonDatHangService : IService<DonDatHang, DonDatHangViewModel>
    {
        //Task<ResponseEntity> CheckPass(NguoiDungViewModel model);

    }

    public class DonDatHangService : ServiceBase<DonDatHang, DonDatHangViewModel>, IDonDatHangService
    {
        //private readonly INguoiDungRepository _nguoiDungRepository;

        public DonDatHangService(IDonDatHangRepository donDatHangRepository, IMapper mapper)
            : base(donDatHangRepository, mapper)
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

      /*  private async Task<string> GenerateToken(NguoiDung entity)
        {
            try
            {
                NhomQuyen nhomQuyen = await _nhomQuyenRepository.GetByIdAsync(entity.MaNhomQuyen);
                if (nhomQuyen == null)
                    return string.Empty;

                List<string> roles = JsonConvert.DeserializeObject<List<string>>(nhomQuyen.DanhSachQuyen);

                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, entity.Id));
                claims.Add(new Claim(ClaimTypes.Email, entity.Email));
                foreach (var item in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, item.Trim()));
                }

                var secret = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var token = new JwtSecurityToken(
                    claims: claims,
                    notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                    expires: new DateTimeOffset(DateTime.Now.AddMinutes(60)).DateTime,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secret),
                        SecurityAlgorithms.HmacSha256Signature)
                );
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }*/
    }
}