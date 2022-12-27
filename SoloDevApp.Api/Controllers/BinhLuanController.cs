using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SoloDevApp.Api.Filters;
using SoloDevApp.Repository.Models;
using SoloDevApp.Service.Services;
using SoloDevApp.Service.Utilities;
using SoloDevApp.Service.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoloDevApp.Api.Controllers
{
    [Route("api/binh-luan")]
    [ApiController]
    [ApiKeyAuth("")]
    public class BinhLuanController : ControllerBase
    {
        private readonly IBinhLuanService _binhLuanService;

        public BinhLuanController(IBinhLuanService binhLuanService)
        {
            _binhLuanService = binhLuanService;
        }

       [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await _binhLuanService.GetAllAsync();
        }



        [HttpPost]
        public async Task<IActionResult> Post([FromHeader] string token,[FromBody] BinhLuanViewModel model)
        {
            string nguoiDungId = FuncUtilities.CheckToken(token, false);
            string sMess = FuncUtilities.TokenMessage(nguoiDungId, false);
            if (sMess != "")
                return new ResponseEntity(403, sMess);

     
            return await _binhLuanService.InsertAsync(model);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromHeader] string token, int id, [FromBody] BinhLuanViewModel model)
        {
            string nguoiDungId = FuncUtilities.CheckToken(token, true);
            string sMess = FuncUtilities.TokenMessage(nguoiDungId, true);
            if (sMess != "")
                return new ResponseEntity(403, sMess);


            return await _binhLuanService.UpdateAsync(id, model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromHeader] string token, int id)
        {
            string nguoiDungId = FuncUtilities.CheckToken(token, true);
            string sMess = FuncUtilities.TokenMessage(nguoiDungId, true);
            if (sMess != "")
                return new ResponseEntity(403, sMess);

            return await _binhLuanService.DeleteByIdAsync(id);
        }

        [HttpGet("lay-binh-luan-theo-cong-viec/{MaCongViec}")]
        public async Task<IActionResult> GetBinhLuanTheoCongViec(string MaCongViec)
        {

            return await _binhLuanService.GetBinhLuanTheoCongViec(MaCongViec);
        }


    }
}