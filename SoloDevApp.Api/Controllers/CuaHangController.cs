using Microsoft.AspNetCore.Mvc;
using SoloDevApp.Api.Filters;
using SoloDevApp.Repository.Models;
using SoloDevApp.Service.Services;
using SoloDevApp.Service.Utilities;
using SoloDevApp.Service.ViewModels;
using System.Threading.Tasks;

namespace SoloDevApp.Api.Controllers
{
    [Route("api/cuahang")]
    [ApiController]
    [ApiKeyAuth("")]
    public class CuaHangController : ControllerBase
    {
        private readonly ICuaHangService _cuaHangService;

        public CuaHangController(ICuaHangService cuaHangService)
        {
            _cuaHangService = cuaHangService;
        }

       [HttpGet()]
        public async Task<IActionResult> Get()
        {
            return await _cuaHangService.GetAllAsync();
        }
       
      

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return await _cuaHangService.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CuaHangViewModel model)
        {
            return await _cuaHangService.InsertAsync(model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CuaHangViewModel model)
        {
            return await _cuaHangService.UpdateAsync(id, model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await _cuaHangService.DeleteByIdAsync(id);
        }
        [HttpPost("upload-hinhanh/{cuaHangId}")]
        public async Task<IActionResult> UploadHinhAnh(string cuaHangId, [FromForm] Photo files)
        {

            
            if (int.Parse(cuaHangId) == 1)
                return new ResponseEntity(403, "Không có quyền");

            return await _cuaHangService.UploadHinhAnh(cuaHangId, files);

        }
    }
}