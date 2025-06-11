
using System.Threading.Tasks;
using DocumentSharingSystem.Contexts;
using DocumentSharingSystem.Models;
using DocumentSharingSystem.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DocumentSharingSystem.Test;

public class UserRepoTest
{
    private DocumentSharingSystemContext _context;
    private UserRepo userRepo;
    [SetUp]
    public void Setup()
    {
        DbContextOptions options = new DbContextOptionsBuilder()
                                        .UseInMemoryDatabase("TestDb")
                                        .Options;
        _context = new DocumentSharingSystemContext(options);

        userRepo = new UserRepo(_context);
    }

    [Test]
    public async Task Add_Test()
    {
        User user = new User
        {
            Id = Guid.NewGuid(),
            Name = "Test1",
            Email = "test@mail.com",
            Role = "User",
            CreatedAt = DateTime.UtcNow,
            LastUpdatedAt = DateTime.UtcNow,
            CreatedByUserId = Guid.NewGuid(),
            LastUpdatedByUserId = Guid.NewGuid()
        };
        await userRepo.Add(user);
        var usersCollection = await userRepo.GetAll();
        var users = usersCollection.ToList();
        Assert.That(users, Is.Not.Null);
        Assert.That(users.Count(), Is.GreaterThanOrEqualTo(1));
        Assert.That(users[0].Id, Is.EqualTo(user.Id));
    }

    [Test]
    public async Task Update_Test()
    {
        var usersCollection = await userRepo.GetAll();
        var users = usersCollection.ToList();
        Assert.That(users, Is.Not.Null);
        Assert.That(users.Count(), Is.GreaterThanOrEqualTo(1));


        User user = users[0];
        User updateuser = new User
        {
            Name = "Test2",
            Email = "Test2@mail.com",
            Role = "User"
        };
        var updatedUser = await userRepo.Update(user.Id, updateuser);

        Assert.That(updatedUser, Is.Not.Null);
        Assert.That(updatedUser.Id,Is.EqualTo(user.Id));
        Assert.That(updatedUser.Name,Is.EqualTo("Test2"));

        usersCollection = await userRepo.GetAll();
        users = usersCollection.ToList();
        Assert.That(users, Is.Not.Null);
        Assert.That(users.Count(), Is.GreaterThanOrEqualTo(1));
        Assert.That(users[0].Email, Is.EqualTo("Test2@mail.com"));  
    }

    [Test]
    public async Task GetAll_Test()
    {
        var usersCollection = await userRepo.GetAll();
        var users = usersCollection.ToList();
        Assert.That(users, Is.Not.Null);
        Assert.That(users.Count(), Is.GreaterThanOrEqualTo(1)); 
    }
    [Test]
    public async Task Get_Test()
    {
        var usersCollection = await userRepo.GetAll();
        var users = usersCollection.ToList();
        Assert.That(users, Is.Not.Null);
        Assert.That(users.Count(), Is.GreaterThanOrEqualTo(1));

        var userId = users[0].Id;
        var user = await userRepo.Get(userId);
        Assert.That(user, Is.Not.Null);
        Assert.That(users[0], Is.EqualTo(user));

    }

    [Test]
    public async Task Delete_Test()
    {
        User user = new User
        {
            Id = Guid.NewGuid(),
            Name = "Test3",
            Email = "test3@mail.com",
            Role = "User",
            CreatedAt = DateTime.UtcNow,
            LastUpdatedAt = DateTime.UtcNow,
            CreatedByUserId = Guid.NewGuid(),
            LastUpdatedByUserId = Guid.NewGuid()
        };
        await userRepo.Add(user);
        var usersCollection = await userRepo.GetAll();
        var users = usersCollection.ToList();
        Assert.That(users, Is.Not.Null);
        Assert.That(users.Count(), Is.EqualTo(2));

        var userId = user.Id;
        var deleteduser = await userRepo.Delete(userId,Guid.NewGuid());
        Assert.That(deleteduser, Is.Not.Null);
        Assert.That(deleteduser.IsDeleted, Is.EqualTo(true));

        usersCollection = await userRepo.GetAll();
        users = usersCollection.Where(d => !d.IsDeleted).ToList();
        Assert.That(users.Count(), Is.EqualTo(1));
        Assert.That(users[0], Is.Not.EqualTo(deleteduser));

    }

    [TearDown]
    public async Task TearDown() {
       await  _context.DisposeAsync();
    }
}
