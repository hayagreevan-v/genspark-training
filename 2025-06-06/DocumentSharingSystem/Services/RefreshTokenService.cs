using DocumentSharingSystem.Interfaces;
using DocumentSharingSystem.Models;

namespace DocumentSharingSystem.Services
{
    public class RefreshTokenService
    {
        private readonly IRepo<Guid, RefreshToken> _refreshTokenRepo;
        public RefreshTokenService(IRepo<Guid, RefreshToken> repo)
        {
            _refreshTokenRepo = repo;
        }
        public virtual async Task<RefreshToken> CreateToken(Guid id)
        {
            RefreshToken refreshToken = new RefreshToken { UserId = id, Token = Guid.NewGuid() };
            refreshToken = await _refreshTokenRepo.Add(refreshToken);
            return refreshToken;
        }
        public virtual async Task<RefreshToken> GetToken(Guid token)
        {
            var refreshTokens = await _refreshTokenRepo.GetAll();
            var refreshToken = refreshTokens.FirstOrDefault(rt => rt.Token == token);
            if (refreshToken == null) throw new Exception("No refresh token found");
            return refreshToken;
        }
        public virtual async Task<RefreshToken> GetTokenByUserId(Guid userId)
        {
            var refreshToken = await _refreshTokenRepo.Get(userId);
            if (refreshToken == null) throw new Exception("No refresh token found");
            return refreshToken;
        }
        public virtual async Task<bool> RemoveToken(Guid id)
        {
            var refreshToken = await _refreshTokenRepo.Delete(id,id);
            if (refreshToken != null) return true;
            return false;
        }
    }
}