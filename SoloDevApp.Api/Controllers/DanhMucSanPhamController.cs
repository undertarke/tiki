using Microsoft.AspNetCore.Mvc;
using SoloDevApp.Api.Filters;
using SoloDevApp.Service.Services;
using SoloDevApp.Service.ViewModels;
using System.Threading.Tasks;

namespace SoloDevApp.Api.Controllers
{
    [Route("api/danhmucsanpham")]
    [ApiController]
    [ApiKeyAuth("")]
    public class DanhMucSanPhamController : ControllerBase
    {
        private readonly IDanhMucSanPhamService _danhMucSanPhamService;

        public DanhMucSanPhamController(IDanhMucSanPhamService danhMucSanPhamService)
        {
            _danhMucSanPhamService = danhMucSanPhamService;
        }

       [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await _danhMucSanPhamService.GetAllAsync();
        }
       
       

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return await _danhMucSanPhamService.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DanhMucSanPhamViewModel model)
        {
            return await _danhMucSanPhamService.InsertAsync(model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] DanhMucSanPhamViewModel model)
        {
            return await _danhMucSanPhamService.UpdateAsync(id, model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await _danhMucSanPhamService.DeleteByIdAsync(id);
        }
    }
}