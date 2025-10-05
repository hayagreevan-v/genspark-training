
using System.Threading.Tasks;
using DocumentSharingSystem.Contexts;
using DocumentSharingSystem.Models;
using DocumentSharingSystem.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DocumentSharingSystem.Test;

public class RefreshTokenRepoTest
{
    private DocumentSharingSystemContext _context;
    private RefreshTokenRepo rtRepo;
    DbContextOptions options;

    Guid userId = Guid.NewGuid();

    [SetUp]
    public async Task Setup()
    {
        options = new DbContextOptionsBuilder<DocumentSharingSystemContext>()
            .UseInMemoryDatabase("TestDbRT")
            .Options;

        _context = new DocumentSharingSystemContext(options);
        rtRepo = new RefreshTokenRepo(_context);

        _context.users.RemoveRange(_context.users);
        await _context.users.AddAsync(new User { Id = userId, Email = "test@example.com" });
        await _context.SaveChangesAsync();
    }

    [Test]
    public async Task Add_Test()
    {

        RefreshToken rt = new RefreshToken
        {
            UserId = userId,
            Token = Guid.NewGuid()
        };
        rt = await rtRepo.Add(rt);
        var rts = (await rtRepo.GetAll()).ToList();

        Assert.That(rt.UserId, Is.EqualTo(userId));
        Assert.That(rts, Is.Not.Null);
        Assert.That(rts.Count(), Is.GreaterThanOrEqualTo(1));
    }

    [Test]
    public async Task GetAll_Test()
    {
        RefreshToken rt = new RefreshToken
        {
            UserId = Guid.NewGuid(),
            Token = Guid.NewGuid()
        };
        rt = await rtRepo.Add(rt);
        var rts = (await rtRepo.GetAll()).ToList();
        Assert.That(rts, Is.Not.Null);
        Assert.That(rts.Count(), Is.GreaterThanOrEqualTo(1));
    }
    [Test]
    public async Task Get_Test()
    {
        RefreshToken rt = new RefreshToken
        {
            UserId = Guid.NewGuid(),
            Token = Guid.NewGuid()
        };
        rt = await rtRepo.Add(rt);
        var rts = (await rtRepo.GetAll()).ToList();
        Assert.That(rts, Is.Not.Null);
        Assert.That(rts.Count(), Is.GreaterThanOrEqualTo(1));

        var rtUserId = rts[0].UserId;
        var rtToken = rts[0].Token;
        rt = await rtRepo.Get(rtUserId);
        Assert.That(rt, Is.Not.Null);
        Assert.That(rt.Token, Is.EqualTo(rtToken));
        Assert.That(rt.UserId, Is.EqualTo(rtUserId));
        Assert.That(rt, Is.EqualTo(rts[0]));

    }

    [Test]
    public async Task Delete_Test()
    {
        RefreshToken rt = new RefreshToken
        {
            UserId = Guid.NewGuid(),
            Token = Guid.NewGuid()
        };
        rt = await rtRepo.Add(rt);
        var rts = (await rtRepo.GetAll()).ToList();
        Assert.That(rts, Is.Not.Null);

        var rtUserId = rt.UserId;
        var deletedrt = await rtRepo.Delete(rtUserId, Guid.NewGuid());
        rts = (await rtRepo.GetAll()).ToList();
        Assert.That(rts.FirstOrDefault(r => r.UserId== rtUserId), Is.Null);
    }

    [TearDown]
    public async Task TearDown()
    {
        await _context.DisposeAsync();
    }
}
