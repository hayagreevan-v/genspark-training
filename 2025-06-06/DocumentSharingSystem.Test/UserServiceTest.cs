
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

public class UserServiceTest
{
    // userRepo = new UserRepo(_context);
        static Guid userId = Guid.NewGuid();
        static Guid userId2 = Guid.NewGuid();
        static User user = new User
        {
            Id = userId,
            Name = "Test1",
            Email = "test@mail.com",
            Role = "User",
            CreatedAt = DateTime.UtcNow,
            LastUpdatedAt = DateTime.UtcNow,
            CreatedByUserId = userId,
            LastUpdatedByUserId = userId
        };
        static User user2 = new User
        {
            Id = userId2,
            Name = "Test2",
            Email = "test2@mail.com",
            Role = "User",
            CreatedAt = DateTime.UtcNow,
            LastUpdatedAt = DateTime.UtcNow,
            CreatedByUserId = userId,
            LastUpdatedByUserId = userId,
            IsDeleted = true
        };
        static UserAddServiceDTO userAddServiceDTO = new UserAddServiceDTO
        {
            Name = "Test1",
            Email = "test@mail.com",
            Role = "User",
            CreatedByUserId = userId,
        };

        PaginationDataDTO<User> paginationDataDTO = new PaginationDataDTO<User>
        {
            Data = new List<User> { user},
            TotalRecords = 2
        };
        PaginationDataDTO<User> paginationDataDTO_Admin = new PaginationDataDTO<User>
        {
            Data = new List<User> { user, user2 },
            TotalRecords = 2
        };

    private DocumentSharingSystemContext _context;
    // private IRepo<Guid, User> userRepo;
    Mock<UserRepo> userRepoMock;
    Mock<IMapper> mapperMock;
    Mock<PaginationContextFns> paginationContextFnsMock;
    private UserService userService;
    [SetUp]
    public void Setup()
    {

        DbContextOptions options = new DbContextOptionsBuilder()
                                        .UseInMemoryDatabase("TestDb")
                                        .Options;
        _context = new DocumentSharingSystemContext(options);

        

        mapperMock = new();
        userRepoMock = new Mock<UserRepo>(_context);
        paginationContextFnsMock = new(_context);

        mapperMock.Setup(m => m.Map<UserAddServiceDTO, User>(It.IsAny<UserAddServiceDTO>()))
                  .Returns(user);

        userRepoMock.Setup(u => u.Add(It.IsAny<User>())).Returns(async () => await Task.FromResult(user));
        userRepoMock.Setup(u => u.Update(It.IsAny<Guid>(),It.IsAny<User>())).Returns(async () => await Task.FromResult(user));
        userRepoMock.Setup(u => u.Get(It.IsAny<Guid>())).Returns(async () => await Task.FromResult(user));
        userRepoMock.Setup(u => u.GetAll()).Returns(async () => await Task.FromResult(new List<User>
                                                                                            {
                                                                                                user2,
                                                                                                user
                                                                                            }
                                                                                        ));
        userRepoMock.Setup(u => u.Delete(It.IsAny<Guid>(),It.IsAny<Guid>())).Returns(async () => await Task.FromResult(user2));

        paginationContextFnsMock.Setup(pcf => pcf.UsersPagination(It.IsAny<int>(), It.IsAny<int>()))
                            .Returns(async () => await Task.FromResult(paginationDataDTO));
        paginationContextFnsMock.Setup(pcf => pcf.UsersPagination_Admin(It.IsAny<int>(), It.IsAny<int>()))
                            .Returns(async () => await Task.FromResult(paginationDataDTO_Admin));

        userService = new UserService(
                                        userRepoMock.Object,
                                        mapperMock.Object,
                                        paginationContextFnsMock.Object
                                    );
    }

    [Test]
    public async Task AddUser_Test()
    {
        User AddedUser = await userService.AddUser(userAddServiceDTO);
        Assert.That(AddedUser, Is.Not.Null);
        Assert.That(AddedUser, Is.EqualTo(user));
    }

    [Test]
    public async Task UpdateUser_Test()
    {
        var newUser = await userService.UpdateUser(userId, userAddServiceDTO);

        Assert.That(newUser, Is.Not.Null);
        Assert.That(newUser,Is.EqualTo(user));
    }
    [Test]
    public async Task GetUser_Test()
    {
        var newUser = await userService.GetUser(userId);

        Assert.That(newUser, Is.Not.Null);
        Assert.That(newUser,Is.EqualTo(user));
    }
    [Test]
    public async Task GetUser_Admin_Test()
    {
        var newUser = await userService.GetUser_Admin(userId);

        Assert.That(newUser, Is.Not.Null);
        Assert.That(newUser,Is.EqualTo(user));

        newUser = await userService.GetUser_Admin(userId2);

        Assert.That(newUser, Is.Not.Null);
        Assert.That(newUser,Is.EqualTo(user2));
    }
    [Test]
    public async Task GetUserByEmail_Test()
    {
        var newUser = await userService.GetUserByEmail("test@mail.com");
        Assert.That(newUser, Is.Not.Null);
        Assert.That(newUser, Is.EqualTo(user));

        Assert.ThrowsAsync<Exception>(() => userService.GetUserByEmail("test2@mail.com"));
    }
    [Test]
    public async Task GetUserByEmail_Admin_Test()
    {
        var newUser = await userService.GetUserByEmail_Admin("test@mail.com");

        Assert.That(newUser, Is.Not.Null);
        Assert.That(newUser,Is.EqualTo(user));

        newUser = await userService.GetUserByEmail_Admin("test2@mail.com");

        Assert.That(newUser, Is.Not.Null);
        Assert.That(newUser,Is.EqualTo(user2));
    }

    [Test]
    public async Task GetAll_Test()
    {
        var users = await userService.GetAll();
        Assert.That(users, Is.Not.Null);
        Assert.That(users.Count(), Is.EqualTo(1));
    }
    [Test]
    public async Task GetAll_Admin_Test()
    {
        var users = await userService.GetAll_Admin();
        Assert.That(users, Is.Not.Null);
        Assert.That(users.Count(), Is.EqualTo(2));
    }

    [Test]
    public async Task DeleteUser_Test()
    {
        var deleteduser = await userService.DeleteUser(userId, userId);
        Assert.That(deleteduser, Is.Not.Null);
        Assert.That(deleteduser.IsDeleted, Is.EqualTo(true));
       
    }
    
    [TearDown]
    public async Task TearDown() {
       await  _context.DisposeAsync();
    }
}
