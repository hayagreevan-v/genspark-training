using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using DocumentSharingSystem.Models.DTOs;
using DocumentSharingSystem.Models.DTOs.CustomResponseDTOs;

namespace DocumentSharingSystem.Misc;

public class CustomExceptionFilter : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        Console.WriteLine("----- Exception Caught in CustomExceptionFilter -----");
        Console.WriteLine($"Type: {context.Exception.GetType()}");
        Console.WriteLine($"Message: {context.Exception.Message}");
        Console.WriteLine($"Stack Trace: {context.Exception.StackTrace}");
        Console.WriteLine("-----------------------------------------------------");

        context.Result = new BadRequestObjectResult(new CustomResponseDTO<string?>
        {
            Success = false,
            Message = "Validation failed",
            ResultsCount = 0,
            Data = null,
            Errors = new ErrorDTO
            {
                message = context.Exception.Message,
                type = context.Exception.GetType().ToString()
            }
        });
        context.ExceptionHandled = true;
    }
}
