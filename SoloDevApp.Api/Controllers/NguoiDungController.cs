using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoloDevApp.Api.Filters;
using SoloDevApp.Repository.Models;
using SoloDevApp.Service.Services;
using SoloDevApp.Service.Utilities;
using SoloDevApp.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoloDevApp.Api.Controllers
{
    //ADMIN, USER, SHOP
    [Route("api/users")]
    [ApiController]
    [ApiKeyAuth("")]
    public class NguoiDungController : ControllerBase
    {
        private readonly INguoiDungService _nguoiDungService;

        public NguoiDungController(INguoiDungService nguoiDungService)
        {
            _nguoiDungService = nguoiDungService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await _nguoiDungService.LayTatCa();
        }
        [HttpGet("phan-trang-tim-kiem")]
        public async Task<IActionResult> GetPaging(int pageIndex, int pageSize, string keyword)
        {
            return await _nguoiDungService.GetPagingAsync(pageIndex, pageSize, keyword == null ? keyword : " Name LIKE N'%" + keyword + "%'");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return await _nguoiDungService.LayChiTiet(id);
        }

 

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ThongTinNguoiDung model)
        {
          
            return await _nguoiDungService.ThemNguoiDung(model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CapNhatNguoiDung model)
        {
            if (id == 1)
                return new ResponseEntity(403, "Không có quyền");

            return await _nguoiDungService.SuaNguoiDung(id, model);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {

            if (id == 1)
                return new ResponseEntity(403, "Không có quyền");

            return await _nguoiDungService.DeleteByIdAsync(id);

        }

        [HttpGet("search/{TenNguoiDung}")]
        public async Task<IActionResult> GetByName(string TenNguoiDung)
        {
            return await _nguoiDungService.GetByName(TenNguoiDung);
        }

        [HttpPost("upload-avatar")]
        public async Task<IActionResult> UploadAvatar([FromHeader] string token,[FromForm] Photo files)
        {

            string nguoiDungId = FuncUtilities.CheckToken(token,false);
            string sMess = FuncUtilities.TokenMessage(nguoiDungId,false);
            if (sMess != "")
                return new ResponseEntity(403, sMess);

            if (int.Parse(nguoiDungId) == 1)
                return new ResponseEntity(403, "Không có quyền");

            return await _nguoiDungService.UploadAvatar(nguoiDungId, files);

        }
    }
}