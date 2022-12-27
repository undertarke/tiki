using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using SoloDevApp.Service.Helpers;
using SoloDevApp.Service.ViewModels;
using SoloDevApp.Service.Constants;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Http.Features;
using System.Globalization;
namespace SoloDevApp.Api.Filters
{
    public class ApiKeyAuthAttribute : Attribute, IAsyncActionFilter
    {
        private string sRole = "";


        public ApiKeyAuthAttribute(string roles)
        {
            sRole = roles;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext filterContext, ActionExecutionDelegate next)
        {
            try
            {

                var accessToken = filterContext.HttpContext.Request.Headers["tokenCybersoft"];
                JwtSecurityToken token = new JwtSecurityTokenHandler().ReadJwtToken(accessToken);
                string hetHanTime = token.Claims.FirstOrDefault(c => c.Type == "HetHanString").Value;

                DateTime dDatetime = DateTime.ParseExact(hetHanTime, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                if (dDatetime <= DateTime.Now)
                {


                    filterContext.HttpContext.Response.Headers.Add("authToken", accessToken);
                    filterContext.HttpContext.Response.Headers.Add("AuthStatus", "NotAuthorized");

                    filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    filterContext.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Not Authorized";
                    filterContext.Result = new ResponseEntity(403, "Token không cybersoft không hợp lệ hoặc đã hết hạn truy cập !", MessageConstants.USER_DO_NOT_AUTHORIZED);


                }
               await next();

            }
            catch (Exception ex)
            {
                filterContext.Result = new ResponseEntity(403, "Token không cybersoft không hợp lệ hoặc đã hết hạn truy cập !", MessageConstants.USER_DO_NOT_AUTHORIZED);
            }
        }
    }
}