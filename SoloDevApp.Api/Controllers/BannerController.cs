using Microsoft.AspNetCore.Mvc;
using SoloDevApp.Api.Filters;
using SoloDevApp.Service.Services;
using SoloDevApp.Service.ViewModels;
using System.Threading.Tasks;

namespace SoloDevApp.Api.Controllers
{
    [Route("api/banner")]
    [ApiController]
    [ApiKeyAuth("")]
    public class BannerController : ControllerBase
    {
        private readonly IBannerService _bannerService;

        public BannerController(IBannerService bannerService)
        {
            _bannerService = bannerService;
        }

       [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await _bannerService.GetAllAsync();
        }

        //[HttpGet("phan-trang")]
        //public async Task<IActionResult> GetPaging(int pageIndex, int pageSize, string keyword)
        //{
        //    return await _BannerService.GetPagingAsync(pageIndex, pageSize, keyword);
        //}

        //[HttpGet("{id}")]
        // public async Task<IActionResult> Get(int id)
        // {
        //     return await _bannerService.GetByIdAsync(id);
        // }

         [HttpPost]
         public async Task<IActionResult> Post([FromBody] BannerViewModel model)
         {
             return await _bannerService.InsertAsync(model);
         }

         [HttpPut("{id}")]
         public async Task<IActionResult> Put(int id, [FromBody] BannerViewModel model)
         {
             return await _bannerService.UpdateAsync(id, model);
         }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await _bannerService.DeleteByIdAsync(id);
        }
    }
}