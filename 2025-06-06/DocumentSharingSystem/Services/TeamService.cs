using DocumentSharingSystem.Interfaces;
using DocumentSharingSystem.Models;
using DocumentSharingSystem.Repositories;

namespace DocumentSharingSystem.Services
{
    public class TeamService
    {
        private readonly IRepo<long,Team> _teamRepo;
        private readonly UserService _userService;
        private readonly DocumentService _documentService;
        public TeamService(IRepo<long, Team> teamRepo, UserService userService, DocumentService documentService)
        {
            _teamRepo = teamRepo;
            _documentService = documentService;
            _userService = userService;
        }

        public async Task<List<Team>>GetAll()
        {
            return (await _teamRepo.GetAll()).Where(t => !t.IsDeleted).OrderBy(t => t.Name).ToList();
        }
        public async Task<List<Team>>GetAll_Admin()
        {
            return (await _teamRepo.GetAll()).OrderBy(t => t.Name).ToList();
        }
        public async Task<List<Team>> GetFilter_Admin(string? searchQuery)
        {
            var teams = (await _teamRepo.GetAll()).ToList();
            teams = teams.Where(t => t.Name.Contains(searchQuery?.ToLower()??"", StringComparison.OrdinalIgnoreCase)).OrderBy(t => t.Name).ToList();
            if (teams == null || teams.Count() == 0) throw new Exception("No teams found under the search");
            return teams;
            
        }
        public async Task<Team>Get(long id)
        {
            return (await _teamRepo.Get(id));
        }
        public async Task<Team> AddTeam(string name, Guid updatedByUserId)
        {
            DateTime time = DateTime.UtcNow;
            Team team = await _teamRepo.Add(new Team
            {
                Name = name,
                CreatedByUserId = updatedByUserId,
                LastUpdatedByUserId = updatedByUserId,
                CreatedAt = time,
                LastUpdatedAt = time,
            });
            if (team == null) throw new Exception("Team Creation Error");
            return team;
        }
        public async Task<Team> UpdateTeam(long id, string name, Guid updatedByUserId)
        {
            Team? editTeam = await _teamRepo.Get(id);
            if (editTeam == null) {
                throw new Exception("No such user found");
            }
            editTeam.Name = name;
            editTeam.LastUpdatedByUserId = updatedByUserId;
            editTeam.LastUpdatedAt = DateTime.UtcNow;
            
            Team team = await _teamRepo.Update(id,editTeam);
            if (team == null) throw new Exception("Team Creation Error");
            return team;
        }
        public async Task<Team> DeleteTeam(long id, Guid updatedByUserId)
        {
            if ((await _userService.GetAll()).Count(u => u.TeamId == id) > 0)
            {
                throw new Exception("Team with active users cannot be deleted");
            }
            if ((await _documentService.GetAll()).Count(d => d.TeamId == id) > 0)
            {
                throw new Exception("Team with active documents cannot be deleted");
            }
            Console.WriteLine("Deletion"+ id);
            Team? editTeam = await _teamRepo.Get(id);
            if (editTeam == null) {
                throw new Exception("No such user found");
            }
            // editTeam.IsDeleted = true;
            // editTeam.LastUpdatedByUserId = updatedByUserId;
            // editTeam.LastUpdatedAt = DateTime.UtcNow;

            Team? team = await _teamRepo.Delete(id,updatedByUserId);
            if (team == null) throw new Exception("Team Deletion Error");
            return team;
        }
        public async Task<Team> RestoreTeam(long id, Guid updatedByUserId)
        {
            Team? editTeam = (await _teamRepo.GetAll()).FirstOrDefault(t => t.Id == id);
            if (editTeam == null) {
                throw new Exception("No such user found");
            }
            editTeam.IsDeleted = false;
            editTeam.LastUpdatedByUserId = updatedByUserId;
            editTeam.LastUpdatedAt = DateTime.UtcNow;

            Team team = await _teamRepo.Update(id,editTeam);
            if (team == null) throw new Exception("Team Creation Error");
            return team;
        }
    }
}