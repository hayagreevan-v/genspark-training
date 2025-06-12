
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BCrypt.Net;
using DocumentSharingSystem.Contexts;
using DocumentSharingSystem.Interfaces;
using DocumentSharingSystem.Misc;
using DocumentSharingSystem.Models;
using DocumentSharingSystem.Models.DTOs;
using DocumentSharingSystem.Models.DTOs.CustomResponseDTOs;
using DocumentSharingSystem.Repositories;
using DocumentSharingSystem.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;

namespace DocumentSharingSystem.Test;

public class AuthenticationServiceTest
{
    static Guid userId = Guid.NewGuid();
    static Guid docId = Guid.NewGuid();
    static Guid docId2 = Guid.NewGuid();
    static User user = new User
        {
            Id = userId,
            Name = "Test1",
            Email = "test@mail.com",
            Role = "User",
            Password = Encoding.UTF8.GetBytes(BCrypt.Net.BCrypt.EnhancedHashPassword("test",13)),
            CreatedAt = DateTime.UtcNow,
            LastUpdatedAt = DateTime.UtcNow,
            CreatedByUserId = userId,
            LastUpdatedByUserId = userId
        };
    RefreshToken refreshToken = new RefreshToken
    {
        UserId = userId,
        Token = Guid.NewGuid()
    };

    LoginRequestDTO loginRequestDTO = new LoginRequestDTO
    {
        Email = "test@mail.com",
        Password = "test"
    };


    private DocumentSharingSystemContext _context;
    // private IRepo<Guid, User> userRepo;
    Mock<UserService> userServiceMock;
    Mock<TokenService> tokenServiceMock;
    Mock<RefreshTokenService> refreshTokenServiceMock;

     Mock<UserRepo> userRepoMock;
     Mock<RefreshTokenRepo> refreshTokenRepoMock;
    Mock<IMapper> mapperMock;
    Mock<IConfiguration> configurationMock;
    Mock<PaginationContextFns> paginationContextFnsMock;

    private AuthenticationService authService;

    [SetUp]
    public void Setup()
    {

        DbContextOptions options = new DbContextOptionsBuilder()
                                        .UseInMemoryDatabase("TestDb")
                                        .Options;
        _context = new DocumentSharingSystemContext(options);


        mapperMock = new();
        userRepoMock = new Mock<UserRepo>(_context);
        refreshTokenRepoMock = new(_context);
        paginationContextFnsMock = new(_context);
        configurationMock = new();

        userServiceMock = new(userRepoMock.Object, mapperMock.Object, paginationContextFnsMock.Object);
        tokenServiceMock = new(configurationMock.Object);
        refreshTokenServiceMock = new(refreshTokenRepoMock.Object);

        configurationMock.SetReturnsDefault("This is a secret key for JWT!!@#$%^&*()_+{}::?><");

        userServiceMock.Setup(u => u.GetUserByEmail(It.IsAny<string>()))
                                    .Returns(async () => await Task.FromResult(user));
        userServiceMock.Setup(u => u.GetUser(It.IsAny<Guid>()))
                                    .Returns(async () => await Task.FromResult(user));

        tokenServiceMock.Setup(rt => rt.GenerateToken(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>()))
                                    .Returns("accesstoken");
        refreshTokenServiceMock.Setup(rt => rt.GetTokenByUserId(It.IsAny<Guid>()))
                                    .Returns(async ()=> await Task.FromResult(refreshToken));
        refreshTokenServiceMock.Setup(rt => rt.CreateToken(It.IsAny<Guid>()))
                                    .Returns(async ()=> await Task.FromResult(refreshToken));
        refreshTokenServiceMock.Setup(rt => rt.GetToken(It.IsAny<Guid>()))
                                    .Returns(async ()=> await Task.FromResult(refreshToken));
        refreshTokenServiceMock.Setup(rt => rt.RemoveToken(It.IsAny<Guid>()))
                                    .Returns(async ()=> await Task.FromResult(true));



        authService = new AuthenticationService(
                                        userServiceMock.Object,
                                        tokenServiceMock.Object,
                                        refreshTokenServiceMock.Object
                                    );
    }

    [Test]
    public async Task Login_Test()
    {
        var loginResponseDTO = await authService.Login(loginRequestDTO);
        Assert.That(loginResponseDTO, Is.Not.Null);
        Assert.That(loginResponseDTO.Email, Is.EqualTo(user.Email));
        Assert.That(loginResponseDTO.RefreshToken, Is.EqualTo(refreshToken.Token));
        Assert.That(loginResponseDTO.AccessToken, Is.EqualTo("accesstoken"));
    }
    [Test]
    public async Task Refresh_Test()
    {
        var loginResponseDTO = await authService.Refresh(userId);
        Assert.That(loginResponseDTO, Is.Not.Null);
        Assert.That(loginResponseDTO.Email, Is.EqualTo(user.Email));
        Assert.That(loginResponseDTO.RefreshToken, Is.EqualTo(refreshToken.Token));
        Assert.That(loginResponseDTO.AccessToken, Is.EqualTo("accesstoken"));
    }
    [Test]
    public async Task Logout_Test()
    {
        var IsLoggedout = await authService.Logout(userId);
        Assert.That(IsLoggedout, Is.EqualTo(true));
    }

    
    [TearDown]
    public async Task TearDown() {
       await  _context.DisposeAsync();
    }
}