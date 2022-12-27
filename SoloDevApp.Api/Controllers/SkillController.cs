using Microsoft.AspNetCore.Mvc;
using SoloDevApp.Api.Filters;
using SoloDevApp.Service.Services;
using SoloDevApp.Service.ViewModels;
using System.Threading.Tasks;

namespace SoloDevApp.Api.Controllers
{
    [Route("api/skill")]
    [ApiController]
    [ApiKeyAuth("")]
    public class SkillController : ControllerBase
    {
        private readonly ISkillService _skillService;

        public SkillController(ISkillService skillService)
        {
            _skillService = skillService;
        }

       [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await _skillService.GetAllAsync();
        }
       /*
        [HttpGet("phan-trang")]
        public async Task<IActionResult> GetPaging(int pageIndex, int pageSize, string keyword)
        {
            return await _skillService.GetPagingAsync(pageIndex, pageSize, keyword);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return await _skillService.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SkillViewModel model)
        {
            return await _skillService.InsertAsync(model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] SkillViewModel model)
        {
            return await _skillService.UpdateAsync(id, model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await _skillService.DeleteByIdAsync(id);
        }*/
    }
}