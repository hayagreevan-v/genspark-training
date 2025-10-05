
using System.Threading.Tasks;
using DocumentSharingSystem.Contexts;
using DocumentSharingSystem.Models;
using DocumentSharingSystem.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DocumentSharingSystem.Test;

public class TeamRepoTest
{
    private DocumentSharingSystemContext _context;
    private TeamRepo teamRepo;
    DbContextOptions options;

    Guid userId = Guid.NewGuid();


    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        options = new DbContextOptionsBuilder<DocumentSharingSystemContext>()
            .UseInMemoryDatabase("TestDbTeam")
            .Options;
    }
    [SetUp]
    public async Task Setup()
    {
        _context = new DocumentSharingSystemContext(options);
        teamRepo = new TeamRepo(_context);

        _context.documents.RemoveRange(_context.documents);
        _context.users.RemoveRange(_context.users);
        _context.teams.RemoveRange(_context.teams);
        await _context.SaveChangesAsync();

        await _context.users.AddAsync(new User { Id = userId, Email = "test@example.com" });
        await _context.SaveChangesAsync();
    }

    [Test]
    public async Task Add_Test()
    {

        Team team = new Team
        {
            Name = "1",
            LastUpdatedByUserId = userId,
            CreatedByUserId = userId
        };
        team = await teamRepo.Add(team);
        var teams = (await teamRepo.GetAll()).ToList();

        Assert.That(team.Id, Is.Not.EqualTo(0));
        Assert.That(teams, Is.Not.Null);
        Assert.That(teams.Count(), Is.GreaterThanOrEqualTo(1));
    }

    [Test]
    public async Task Update_Test()
    {

        Team team = new Team
        {
            Name = "1",
            LastUpdatedByUserId = userId,
            CreatedByUserId = userId
        };
        team = await teamRepo.Add(team);

        var teamsCollection = await teamRepo.GetAll();
        var teams = teamsCollection.ToList();
        Assert.That(teams, Is.Not.Null);
        Assert.That(teams.Count(), Is.GreaterThanOrEqualTo(1));

        Team updateTeam = new Team { Name = "2" };
        var newTeam = await teamRepo.Update(team.Id, updateTeam);

        Assert.That(newTeam, Is.Not.Null);
        Assert.That(newTeam.Id, Is.EqualTo(team.Id));
        Assert.That(newTeam.Name, Is.EqualTo("2"));

        teamsCollection = await teamRepo.GetAll();
        teams = teamsCollection.ToList();
        Assert.That(team, Is.Not.Null);
        Assert.That(teams.Count(), Is.GreaterThanOrEqualTo(1));
        Assert.That(teams.FirstOrDefault(d => d.Id == team.Id)!.Name, Is.EqualTo("2"));
    }

    [Test]
    public async Task GetAll_Test()
    {
        Team team = new Team
        {
            Name = "1",
            LastUpdatedByUserId = userId,
            CreatedByUserId = userId
        };
        team = await teamRepo.Add(team);

        var teamsCollection = await teamRepo.GetAll();
        var teams = teamsCollection.ToList();
        Assert.That(teams, Is.Not.Null);
        Assert.That(teams.Count(), Is.GreaterThanOrEqualTo(1));
    }
    [Test]
    public async Task Get_Test()
    {
        Team team = new Team
        {
            Name = "1",
            LastUpdatedByUserId = userId,
            CreatedByUserId = userId
        };
        team = await teamRepo.Add(team);

        var teamsCollection = await teamRepo.GetAll();
        var teams = teamsCollection.ToList();
        Assert.That(teams, Is.Not.Null);
        Assert.That(teams.Count(), Is.GreaterThanOrEqualTo(1));

        var teamId = teams[0].Id;
        team = await teamRepo.Get(teamId);
        Assert.That(team, Is.Not.Null);
        Assert.That(team.Id, Is.EqualTo(teamId));
        Assert.That(team, Is.EqualTo(teams[0]));

    }

    [Test]
    public async Task Delete_Test()
    {
        Team team = new Team
        {
            Name = "1",
            LastUpdatedByUserId = userId,
            CreatedByUserId = userId
        };
        team = await teamRepo.Add(team);
        var teamsCollection = await teamRepo.GetAll();
        var teams = teamsCollection.ToList();
        Assert.That(teams, Is.Not.Null);

        var teamId = team.Id;
        var deletedteam = await teamRepo.Delete(teamId, Guid.NewGuid());
        Assert.That(deletedteam, Is.Not.Null);
        Assert.That(deletedteam.IsDeleted, Is.EqualTo(true));

        // teamsCollection = await documentRepo.GetAll();
        // docs = docsCollection.Where(d => d.IsDeleted).ToList();
        // Assert.That(docs[0].Id, Is.Not.EqualTo(docId));
        // Assert.That(docs.Count(), Is.EqualTo(1));

    }

    [TearDown]
    public async Task TearDown()
    {
        await _context.DisposeAsync();
    }
}
