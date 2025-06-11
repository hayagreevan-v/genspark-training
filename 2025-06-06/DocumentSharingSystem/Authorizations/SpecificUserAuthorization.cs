using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace DocumentSharingSystem.Authorizations
{
    public class SpecificUserAuthorizationRequirement : IAuthorizationRequirement { }
    public class SpecificUserAuthorizationHandler : AuthorizationHandler<SpecificUserAuthorizationRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SpecificUserAuthorizationHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SpecificUserAuthorizationRequirement requirement)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            var reqUserId = httpContext?.Request.RouteValues["id"]?.ToString();
            var currentUserId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentUserRole = context.User.FindFirstValue(ClaimTypes.Role);
            if (string.IsNullOrEmpty(reqUserId) || string.IsNullOrEmpty(currentUserId))
            {
                context.Fail();
                return Task.CompletedTask;
            }
            if (currentUserRole == "Admin" || currentUserId == reqUserId)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}