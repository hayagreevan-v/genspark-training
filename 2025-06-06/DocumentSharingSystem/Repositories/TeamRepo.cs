using System;
using Microsoft.EntityFrameworkCore;
using DocumentSharingSystem.Contexts;
using DocumentSharingSystem.Models;

namespace DocumentSharingSystem.Repositories;

public class TeamRepo : Repo<long, Team>
{
    public TeamRepo(DocumentSharingSystemContext DocumentSharingSystemContext) : base(DocumentSharingSystemContext)
    {
    }


    public override async Task<Team> Get(long id)
    {
        Team? team = await _context.teams.FindAsync(id);
        if (team == null || team.IsDeleted) throw new Exception("Team not found");
        return team;
    }

    public override async Task<ICollection<Team>> GetAll()
    {
        var teams = _context.teams.Include(t => t.CreatedByUser).Include(t => t.LastUpdatedByUser);
        if (teams == null) throw new Exception("No teams found");
        return await teams.ToListAsync();
    }
}
