using System.Text.Json;
using DocumentSharingSystem.Models.DTOs.CustomResponseDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;

namespace DocumentSharingSystem.Authorizations
{
    public class CustomAuthorizationMiddleware : IAuthorizationMiddlewareResultHandler
    {
        public readonly AuthorizationMiddlewareResultHandler _defaultHandler = new AuthorizationMiddlewareResultHandler();
        public async Task HandleAsync(RequestDelegate next,
                                        HttpContext context,
                                        AuthorizationPolicy policy,
                                        PolicyAuthorizationResult authorizeResult)
        {
            if (!authorizeResult.Succeeded || authorizeResult.Challenged)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                context.Response.ContentType = "application/json";
                var roleRequirement = policy.Requirements.OfType<RolesAuthorizationRequirement>().FirstOrDefault();
                string authorizedRoles = roleRequirement != null ? string.Join(", ", roleRequirement.AllowedRoles) : "authorized roles";


                var response = new CustomResponseDTO<string?>
                {
                    Success = false,
                    Message = "UnAuthorized Access",
                    Data = null,
                    ResultsCount = 0,
                    Errors = new ErrorDTO
                    {
                        message = $"Only {authorizedRoles} can perform this action!",
                        type = "Authorization Restriction"
                    }
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                return;
            }
            await _defaultHandler.HandleAsync(next, context, policy, authorizeResult);                     
        }
    }
}