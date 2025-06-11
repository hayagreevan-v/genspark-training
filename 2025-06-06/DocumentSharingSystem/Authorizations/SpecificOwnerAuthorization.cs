using System.Security.Claims;
using DocumentSharingSystem.Models;
using DocumentSharingSystem.Services;
using Microsoft.AspNetCore.Authorization;

namespace DocumentSharingSystem.Authorizations
{
    public class SpecificOwnerAuthorizationRequirement : IAuthorizationRequirement;
    public class SpecificOwnerAuthorizationHandler : AuthorizationHandler<SpecificOwnerAuthorizationRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DocumentService _documentService;
        public SpecificOwnerAuthorizationHandler(IHttpContextAccessor httpContextAccessor, DocumentService documentService)
        {
            _httpContextAccessor = httpContextAccessor;
            _documentService = documentService;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, SpecificOwnerAuthorizationRequirement requirement)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            var reqDocId = httpContext?.Request.RouteValues["id"]?.ToString();
            var currentUserId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentUserRole = context.User.FindFirstValue(ClaimTypes.Role);
            if (string.IsNullOrEmpty(reqDocId) || string.IsNullOrEmpty(currentUserId))
            {
                context.Fail();
                return;
            }
            Document doc = await _documentService.GetDocument(Guid.Parse(reqDocId));
            Guid docOwnerId = doc.CreatedByUserId;
            if (currentUserRole == "Admin" || currentUserId == docOwnerId.ToString())
            {
                context.Succeed(requirement);
            }
            return;
        }
    }
}