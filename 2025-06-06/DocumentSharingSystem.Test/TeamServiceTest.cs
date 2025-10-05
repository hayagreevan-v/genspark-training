
using System.Threading.Tasks;
using AutoMapper;
using DocumentSharingSystem.Contexts;
using DocumentSharingSystem.Interfaces;
using DocumentSharingSystem.Misc;
using DocumentSharingSystem.Models;
using DocumentSharingSystem.Models.DTOs;
using DocumentSharingSystem.Models.DTOs.CustomResponseDTOs;
using DocumentSharingSystem.Repositories;
using DocumentSharingSystem.Services;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace DocumentSharingSystem.Test;

public class TeamServiceTest
{
    // userRepo = new UserRepo(_context);
        static Guid userId = Guid.NewGuid();
        static UserAddServiceDTO userAddServiceDTO = new UserAddServiceDTO
        {
            Name = "Test1",
            Email = "test@mail.com",
            Role = "User",
            Password = "User",
            LastUpdatedByUserId = userId,
        };

    static Team team = new Team
    {
        Name = "1",
        Id = 1
    };
    static Team team2 = new Team { Id = 2, Name = "test", IsDeleted = true };



    private DocumentSharingSystemContext _context;
    // private IRepo<Guid, User> userRepo;
    Mock<TeamRepo> teamRepoMock;
    Mock<UserService> mockUserService;
    Mock<DocumentService> mockDocumentService;


    Mock<DocumentRepo> docRepoMock;
    Mock<UserRepo> userRepoMock;
    Mock<IMapper> mapperMock;
    Mock<PaginationContextFns> paginationContextFnsMock;


    private TeamService teamService;

    [SetUp]
    public void Setup()
    {

        DbContextOptions options = new DbContextOptionsBuilder()
                                        .UseInMemoryDatabase("TestDb")
                                        .Options;
        _context = new DocumentSharingSystemContext(options);



        docRepoMock = new Mock<DocumentRepo>(_context);
        userRepoMock = new Mock<UserRepo>(_context);
        teamRepoMock = new Mock<TeamRepo>(_context);

        paginationContextFnsMock = new(_context);
        mapperMock = new();

        mockUserService = new Mock<UserService>(userRepoMock.Object, mapperMock.Object, paginationContextFnsMock.Object);
        mockDocumentService = new Mock<DocumentService>(docRepoMock.Object, paginationContextFnsMock.Object);

        teamRepoMock.Setup(u => u.Add(It.IsAny<Team>())).Returns(async () => await Task.FromResult(team));
        teamRepoMock.Setup(u => u.Update(It.IsAny<long>(), It.IsAny<Team>())).Returns(async () => await Task.FromResult(team));
        teamRepoMock.Setup(u => u.Get(It.IsAny<long>())).Returns(async () => await Task.FromResult(team));
        teamRepoMock.Setup(u => u.GetAll()).Returns(async () => await Task.FromResult(new List<Team>
                                                                                            {
                                                                                                team,
                                                                                                team2
                                                                                            }
                                                                                        ));
        teamRepoMock.Setup(u => u.Delete(It.IsAny<long>(), It.IsAny<Guid>())).Returns(async () => await Task.FromResult(team2));

        mockUserService.Setup(u => u.GetAll()).Returns(async () => await Task.FromResult(new List<User>()));
        mockDocumentService.Setup(u => u.GetAll()).Returns(async () => await Task.FromResult(new List<Document>()));

        teamService = new TeamService(teamRepoMock.Object, mockUserService.Object, mockDocumentService.Object);

    }

    [Test]
    public async Task AddTeam_Test()
    {
        Team AddedTeam = await teamService.AddTeam("n1",userId);
        Assert.That(AddedTeam, Is.Not.Null);
        Assert.That(AddedTeam, Is.EqualTo(team));
    }

    [Test]
    public async Task UpdateTeam_Test()
    {
        var newTeam = await teamService.UpdateTeam(team.Id,"2",userId);

        Assert.That(newTeam, Is.Not.Null);
        Assert.That(newTeam,Is.EqualTo(team));
    }
    [Test]
    public async Task GetAll_Test()
    {
        var teams = await teamService.GetAll();

        Assert.That(teams, Is.Not.Null);
        Assert.That(teams[0],Is.EqualTo(team));
        Assert.That(teams.Count(),Is.EqualTo(1));
    }
    [Test]
    public async Task GetAll_Admin_Test()
    {
        var teams = await teamService.GetAll_Admin();

        Assert.That(teams, Is.Not.Null);
        Assert.That(teams.Count(),Is.EqualTo(2));

    }
    [Test]
    public async Task GetFilter_Admin_Test()
    {
        var teams = await teamService.GetFilter_Admin("1");
        Assert.That(teams, Is.Not.Null);
        Assert.That(teams.Count(), Is.EqualTo(1));
        Assert.That(teams[0], Is.EqualTo(team));
    }
    [Test]
    public async Task Get_Test()
    {
        var team1 = await teamService.Get(1);
        Assert.That(team1, Is.Not.Null);
        Assert.That(team1,Is.EqualTo(team));

    }

    [Test]
    public async Task DeleteTeam_Test()
    {
        var deletedTeam = await teamService.DeleteTeam(team2.Id, userId);
        Assert.That(deletedTeam, Is.Not.Null);
        Assert.That(deletedTeam.IsDeleted, Is.EqualTo(true));
       
    }
    
    [TearDown]
    public async Task TearDown() {
       await  _context.DisposeAsync();
    }
}
