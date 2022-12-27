using Microsoft.AspNetCore.Mvc;
using SoloDevApp.Api.Filters;
using SoloDevApp.Service.Services;
using SoloDevApp.Service.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoloDevApp.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    [ApiKeyAuth("")]
    public class AuthController : ControllerBase
    {
        private readonly INguoiDungService _nguoiDungService;

        public AuthController(INguoiDungService nguoiDungService)
        {
            _nguoiDungService = nguoiDungService;
        }

       

        [HttpPost("signup")]
        public async Task<IActionResult> DangKy(ThongTinNguoiDung model)
        {
            return await _nguoiDungService.DangKy(model);
        }
        [HttpPost("signin")]
        public async Task<IActionResult> DangNhap(DangNhapView model)
        {
            return await _nguoiDungService.DangNhap(model);
        }

    }
}