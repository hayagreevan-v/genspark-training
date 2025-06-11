using DocumentSharingSystem.Contexts;
using DocumentSharingSystem.Models;
using DocumentSharingSystem.Models.DTOs;

namespace DocumentSharingSystem.Misc
{
    public class PaginationContextFns
    {
        private readonly DocumentSharingSystemContext _context;
        public PaginationContextFns(DocumentSharingSystemContext documentSharingSystemContext)
        {
            _context = documentSharingSystemContext;
        }
        public virtual async Task<PaginationDataDTO<User>> UsersPagination_Admin(int pageNo, int pageSize)
        {
            return await _context.UsersPagination_Admin(pageNo, pageSize);
        }
        public virtual async Task<PaginationDataDTO<Document>> DocumentsPagination_Admin(int pageNo, int pageSize)
        {
            return await _context.DocumentsPagination_Admin(pageNo, pageSize);
        }
        public virtual async Task<PaginationDataDTO<User>> UsersPagination(int pageNo, int pageSize)
        {
            return await _context.UsersPagination(pageNo, pageSize);
        }
        public virtual async Task<PaginationDataDTO<Document>> DocumentsPagination(int pageNo, int pageSize)
        {
            return await _context.DocumentsPagination(pageNo, pageSize);
        }
    }
}