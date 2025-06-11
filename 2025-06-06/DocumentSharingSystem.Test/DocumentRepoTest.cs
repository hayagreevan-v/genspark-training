
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
    [SetUp]
    public void Setup()
    {
        DbContextOptions options = new DbContextOptionsBuilder()
                                        .UseInMemoryDatabase("TestDb")
                                        .Options;
        _context = new DocumentSharingSystemContext(options);

        documentRepo = new DocumentRepo(_context);
    }

    [Test]
    public async Task Add_Test()
    {
        Document doc = new Document
        {
            Id = Guid.NewGuid(),
            OriginalFileName = "1",
            StoredFileName = "1",
            CreatedByUserId = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            LastUpdatedByUserId = Guid.NewGuid(),
            LastUpdatedAt = DateTime.UtcNow
        };
        await documentRepo.Add(doc);
        var docs = await documentRepo.GetAll();
        Assert.That(docs, Is.Not.Null);
        Assert.That(docs.Count(), Is.GreaterThanOrEqualTo(1));
    }

    [Test]
    public async Task Update_Test()
    {
        var docsCollection = await documentRepo.GetAll();
        var docs = docsCollection.ToList();
        Assert.That(docs, Is.Not.Null);
        Assert.That(docs.Count(), Is.GreaterThanOrEqualTo(1));


        Document doc = docs[0];
        Document updateDoc = new Document
        {
            OriginalFileName = "2",
            StoredFileName = "2",
            CreatedByUserId = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            LastUpdatedByUserId = Guid.NewGuid(),
            LastUpdatedAt = DateTime.UtcNow

        };
        var newDoc = await documentRepo.Update(doc.Id, updateDoc);

        Assert.That(newDoc, Is.Not.Null);
        Assert.That(newDoc.Id,Is.EqualTo(doc.Id));
        Assert.That(newDoc.OriginalFileName,Is.EqualTo("2"));

        docsCollection = await documentRepo.GetAll();
        docs = docsCollection.ToList();
        Assert.That(docs, Is.Not.Null);
        Assert.That(docs.Count(), Is.GreaterThanOrEqualTo(1));
        Assert.That(docs[0].StoredFileName, Is.EqualTo("2"));  
    }

    [Test]
    public async Task GetAll_Test()
    {
        var docsCollection = await documentRepo.GetAll();
        var docs = docsCollection.ToList();
        Assert.That(docs, Is.Not.Null);
        Assert.That(docs.Count(), Is.GreaterThanOrEqualTo(1)); 
    }
    [Test]
    public async Task Get_Test()
    {
        var docsCollection = await documentRepo.GetAll();
        var docs = docsCollection.ToList();
        Assert.That(docs, Is.Not.Null);
        Assert.That(docs.Count(), Is.GreaterThanOrEqualTo(1));

        var docId = docs[0].Id;
        var doc = await documentRepo.Get(docId);
        Assert.That(doc, Is.Not.Null);
        Assert.That(doc.Id, Is.EqualTo(docId));

    }

    [Test]
    public async Task Delete_Test()
    {
        Document doc = new Document
        {
            Id = Guid.NewGuid(),
            OriginalFileName = "3",
            StoredFileName = "3",
            CreatedByUserId = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            LastUpdatedByUserId = Guid.NewGuid(),
            LastUpdatedAt = DateTime.UtcNow
        };
        await documentRepo.Add(doc);
        var docsCollection = await documentRepo.GetAll();
        var docs = docsCollection.ToList();
        Assert.That(docs, Is.Not.Null);
        Assert.That(docs.Count(), Is.EqualTo(2));

        var docId = doc.Id;
        var deleteddoc = await documentRepo.Delete(docId,Guid.NewGuid());
        Assert.That(deleteddoc, Is.Not.Null);
        Assert.That(deleteddoc.IsDeleted, Is.EqualTo(true));

        docsCollection = await documentRepo.GetAll();
        docs = docsCollection.Where(d => !d.IsDeleted).ToList();
        Assert.That(docs[0].Id, Is.Not.EqualTo(docId));
        Assert.That(docs.Count(), Is.EqualTo(1));

    }

    [TearDown]
    public async Task TearDown() {
       await  _context.DisposeAsync();
    }
}
