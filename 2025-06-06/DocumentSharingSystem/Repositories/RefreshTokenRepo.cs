
using DocumentSharingSystem.Contexts;
using DocumentSharingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace DocumentSharingSystem.Repositories
{
    public class RefreshTokenRepo : Repo<Guid, RefreshToken>
    {
        public RefreshTokenRepo(DocumentSharingSystemContext DocumentSharingSystemContext) : base(DocumentSharingSystemContext)
        {
        }

        public override async Task<RefreshToken> Delete(Guid id, Guid userId)
        {
            var rt = await Get(id);
            if (rt == null) throw new Exception("No refresh token found");
            _context.Remove(rt);
            await _context.SaveChangesAsync();
            return rt;
        }
        public override async Task<RefreshToken> Get(Guid id)
        {
            var rt = await _context.refresh_tokens.FindAsync(id);
            if (rt == null) throw new Exception("No refresh token found");
            return rt;
        }

        public override async Task<ICollection<RefreshToken>> GetAll()
        {
            return await _context.refresh_tokens.ToListAsync();
        }
    }
}