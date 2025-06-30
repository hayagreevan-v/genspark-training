using DocumentSharingSystem.Interfaces;
using DocumentSharingSystem.Models;
using DocumentSharingSystem.Repositories;

namespace DocumentSharingSystem.Services
{
    public class TeamService
    {
        private readonly IRepo<long,Team> _teamRepo;
        public TeamService(IRepo<long,Team> teamRepo)
        {
            _teamRepo = teamRepo;
        }

        public async Task<List<Team>>GetAll()
        {
            return (await _teamRepo.GetAll()).Where(t => !t.IsDeleted).ToList();
        }
        public async Task<Team> AddTeam(string name)
        {
            Team team = await _teamRepo.Add(new Team { Name = name });
            if (team == null) throw new Exception("Team Creation Error");
            return team;
        }
    }
}