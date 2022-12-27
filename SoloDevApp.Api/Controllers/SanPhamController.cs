using Microsoft.AspNetCore.Mvc;
using SoloDevApp.Api.Filters;
using SoloDevApp.Repository.Models;
using SoloDevApp.Service.Services;
using SoloDevApp.Service.Utilities;
using SoloDevApp.Service.ViewModels;
using System.Threading.Tasks;

namespace SoloDevApp.Api.Controllers
{
    [Route("api/sanpham")]
    [ApiController]
    [ApiKeyAuth("")]
    public class SanPhamController : ControllerBase
    {
        private readonly ISanPhamService _sanPhamService;

        public SanPhamController(ISanPhamService sanPhamService)
        {
            _sanPhamService = sanPhamService;
        }

       [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await _sanPhamService.GetAllAsync();
        }
       
        [HttpGet("phan-trang")]
        public async Task<IActionResult> GetPaging(int pageIndex, int pageSize, string keyword)
        {
            return await _sanPhamService.GetPagingAsync(pageIndex, pageSize, keyword);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return await _sanPhamService.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SanPhamViewModel model)
        {
            return await _sanPhamService.InsertAsync(model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] SanPhamViewModel model)
        {
            return await _sanPhamService.UpdateAsync(id, model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await _sanPhamService.DeleteByIdAsync(id);
        }

        [HttpPost("upload-hinhanh/{sanPhamId}")]
        public async Task<IActionResult> UploadHinhAnh(string sanPhamId, [FromForm] Photo files)
        {

            return await _sanPhamService.UploadHinhAnh(sanPhamId, files);

        }
    }
}