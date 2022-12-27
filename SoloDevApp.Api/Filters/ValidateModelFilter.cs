﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace SoloDevApp.Api.Filters
{
    public class ValidateModelFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new JsonResult(new
                {
                    StatusCode = 400,
                    Content = context.ModelState,
                    Message = "Dữ liệu nhập vào không đúng!",
                    DateTime = DateTime.Now
                });
            }
        }
    }
}