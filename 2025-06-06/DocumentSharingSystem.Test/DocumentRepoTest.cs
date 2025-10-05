
using System.Threading.Tasks;
using DocumentSharingSystem.Contexts;
using DocumentSharingSystem.Models;
using DocumentSharingSystem.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DocumentSharingSystem.Test;

public class DocumentRepoTest
{
    private DocumentSharingSystemContext _context;
    private DocumentRepo documentRepo;
    DbContextOptions options;

    Guid userId = Guid.NewGuid();
    long teamId = 1;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        options = new DbContextOptionsBuilder<DocumentSharingSystemContext>()
            .UseInMemoryDatabase("TestDbDocument")
            .Options;
    }
    [SetUp]
    public async Task Setup()
    {
        _context = new DocumentSharingSystemContext(options);
        documentRepo = new DocumentRepo(_context);

        _context.documents.RemoveRange(_context.documents);
        _context.users.RemoveRange(_context.users);
        _context.teams.RemoveRange(_context.teams);
        await _context.SaveChangesAsync();

        await _context.users.AddAsync(new User { Id = userId, Email = "test@example.com" });
        await _context.teams.AddAsync(new Team { Id = teamId, Name = "Test Team" });
        await _context.SaveChangesAsync();
    }

    [Test]
    public async Task Add_Test()
    {

        Document doc = new Document
        {
            Id = Guid.NewGuid(),
            OriginalFileName = "1",
            StoredFileName = "1",
            CreatedByUserId = userId,
            CreatedAt = DateTime.UtcNow,
            LastUpdatedByUserId = userId,
            LastUpdatedAt = DateTime.UtcNow,
            TeamId = teamId,
            Description = "desc",
            Visibility = "Public",
            IsDeleted = false
        };
        doc = await documentRepo.Add(doc);
        var docs = (await documentRepo.GetAll()).ToList();
        Assert.That(doc.Id, Is.Not.EqualTo(Guid.Empty));
        Assert.That(docs, Is.Not.Null);
        Assert.That(docs.Count(), Is.GreaterThanOrEqualTo(1));
    }

    [Test]
    public async Task Update_Test()
    {
        Guid docId = Guid.NewGuid();
        Document doc = new Document
        {
            Id = docId,
            OriginalFileName = "5",
            StoredFileName = "5",
            CreatedByUserId = userId,
            CreatedAt = DateTime.UtcNow,
            LastUpdatedByUserId = userId,
            LastUpdatedAt = DateTime.UtcNow,
            TeamId = teamId,
            Description = "desc",
            Visibility = "Public",
            IsDeleted = false
        };
        doc = await documentRepo.Add(doc);

        var docsCollection = await documentRepo.GetAll();
        var docs = docsCollection.ToList();
        Assert.That(docs, Is.Not.Null);
        Assert.That(docs.Count(), Is.GreaterThanOrEqualTo(1));

        Document updateDoc = new Document
        {
            OriginalFileName = "2",
            StoredFileName = "2",
            CreatedByUserId = userId,
            CreatedAt = DateTime.UtcNow,
            LastUpdatedByUserId = userId,
            LastUpdatedAt = DateTime.UtcNow
        };
        var newDoc = await documentRepo.Update(doc.Id, updateDoc);

        Assert.That(newDoc, Is.Not.Null);
        Assert.That(newDoc.Id, Is.EqualTo(doc.Id));
        Assert.That(newDoc.OriginalFileName, Is.EqualTo("2"));

        docsCollection = await documentRepo.GetAll();
        docs = docsCollection.ToList();
        Assert.That(docs, Is.Not.Null);
        Assert.That(docs.Count(), Is.GreaterThanOrEqualTo(1));
        Assert.That(docs.FirstOrDefault(d => d.Id == doc.Id)!.StoredFileName, Is.EqualTo("2"));
    }

    [Test]
    public async Task GetAll_Test()
    {
        Guid docId = Guid.NewGuid();
        Document doc = new Document
        {
            Id = docId,
            OriginalFileName = "5",
            StoredFileName = "5",
            CreatedByUserId = userId,
            CreatedAt = DateTime.UtcNow,
            LastUpdatedByUserId = userId,
            LastUpdatedAt = DateTime.UtcNow,
            TeamId = teamId,
            Description = "desc",
            Visibility = "Public",
            IsDeleted = false
        };
        doc = await documentRepo.Add(doc);
        var docsCollection = await documentRepo.GetAll();
        var docs = docsCollection.ToList();
        Assert.That(docs, Is.Not.Null);
        Assert.That(docs.Count(), Is.GreaterThanOrEqualTo(1));
    }
    [Test]
    public async Task Get_Test()
    {
        Guid docId = Guid.NewGuid();
        Document doc = new Document
        {
            Id = docId,
            OriginalFileName = "5",
            StoredFileName = "5",
            CreatedByUserId = userId,
            CreatedAt = DateTime.UtcNow,
            LastUpdatedByUserId = userId,
            LastUpdatedAt = DateTime.UtcNow,
            TeamId = teamId,
            Description = "desc",
            Visibility = "Public",
            IsDeleted = false
        };
        doc = await documentRepo.Add(doc);

        var docsCollection = await documentRepo.GetAll();
        var docs = docsCollection.ToList();
        Assert.That(docs, Is.Not.Null);
        Assert.That(docs.Count(), Is.GreaterThanOrEqualTo(1));

        docId = docs[0].Id;
        doc = await documentRepo.Get(docId);
        Assert.That(doc, Is.Not.Null);
        Assert.That(doc.Id, Is.EqualTo(docId));
        Assert.That(doc, Is.EqualTo(docs[0]));

    }

    [Test]
    public async Task Delete_Test()
    {
        Document doc = new Document
        {
            Id = Guid.NewGuid(),
            OriginalFileName = "3",
            StoredFileName = "3",
            CreatedByUserId = userId,
            CreatedAt = DateTime.UtcNow,
            LastUpdatedByUserId = userId,
            LastUpdatedAt = DateTime.UtcNow
        };
        doc = await documentRepo.Add(doc);
        var docsCollection = await documentRepo.GetAll();
        var docs = docsCollection.ToList();
        Assert.That(docs, Is.Not.Null);

        var docId = doc.Id;
        var deleteddoc = await documentRepo.Delete(docId, Guid.NewGuid());
        Assert.That(deleteddoc, Is.Not.Null);
        Assert.That(deleteddoc.IsDeleted, Is.EqualTo(true));

        // docsCollection = await documentRepo.GetAll();
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
