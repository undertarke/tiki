using Microsoft.AspNetCore.Mvc;
using SoloDevApp.Api.Filters;
using SoloDevApp.Service.Services;
using SoloDevApp.Service.ViewModels;
using System.Threading.Tasks;

namespace SoloDevApp.Api.Controllers
{
    [Route("api/dondathang")]
    [ApiController]
    [ApiKeyAuth("")]
    public class DonDatHangController : ControllerBase
    {
        private readonly IDonDatHangService _donDatHangService;

        public DonDatHangController(IDonDatHangService donDatHangService)
        {
            _donDatHangService = donDatHangService;
        }

       [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await _donDatHangService.GetAllAsync();
        }
      

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return await _donDatHangService.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DonDatHangViewModel model)
        {
            return await _donDatHangService.InsertAsync(model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] DonDatHangViewModel model)
        {
            return await _donDatHangService.UpdateAsync(id, model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await _donDatHangService.DeleteByIdAsync(id);
        }
    }
}