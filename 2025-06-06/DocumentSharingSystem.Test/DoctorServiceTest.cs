
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

public class DoctorServiceTest
{
    // userRepo = new UserRepo(_context);
    static Guid userId = Guid.NewGuid();
    static Guid docId = Guid.NewGuid();
    static Guid docId2 = Guid.NewGuid();
    static Document doc = new Document
        {
            Id = docId,
            OriginalFileName = "1",
            StoredFileName = "1",
            CreatedByUserId = userId,
            CreatedAt = DateTime.UtcNow,
            LastUpdatedByUserId = userId,
            LastUpdatedAt = DateTime.UtcNow
        };
    static Document doc2 = new Document
        {
            Id = docId2,
            OriginalFileName = "2",
            StoredFileName = "2",
            CreatedByUserId = userId,
            CreatedAt = DateTime.UtcNow,
            LastUpdatedByUserId = userId,
            LastUpdatedAt = DateTime.UtcNow,
            IsDeleted= true
        };


    private DocumentSharingSystemContext _context;
    // private IRepo<Guid, User> userRepo;
    Mock<DocumentRepo> docRepoMock;
    Mock<PaginationContextFns> paginationContextFnsMock;
    private DocumentService documentService;
    [SetUp]
    public void Setup()
    {

        DbContextOptions options = new DbContextOptionsBuilder()
                                        .UseInMemoryDatabase("TestDb")
                                        .Options;
        _context = new DocumentSharingSystemContext(options);

        

        docRepoMock = new Mock<DocumentRepo>(_context);
        paginationContextFnsMock = new(_context);

        docRepoMock.Setup(u => u.Add(It.IsAny<Document>())).Returns(async () => await Task.FromResult(doc));
        docRepoMock.Setup(u => u.Update(It.IsAny<Guid>(),It.IsAny<Document>())).Returns(async () => await Task.FromResult(doc));
        docRepoMock.Setup(u => u.Get(It.IsAny<Guid>())).Returns(async () => await Task.FromResult(doc));
        docRepoMock.Setup(u => u.GetAll()).Returns(async () => await Task.FromResult(new List<Document>
                                                                                            {
                                                                                                doc2,
                                                                                                doc
                                                                                            }
                                                                                        ));
        docRepoMock.Setup(u => u.Delete(It.IsAny<Guid>(),It.IsAny<Guid>())).Returns(async () => await Task.FromResult(doc2));

        documentService = new DocumentService(
                                        docRepoMock.Object,
                                        paginationContextFnsMock.Object
                                    );
    }

    [Test]
    public async Task AddDocument_Test()
    {
        Document AddedDocument = await documentService.AddDocument(doc);
        Assert.That(AddedDocument, Is.Not.Null);
        Assert.That(AddedDocument, Is.EqualTo(doc));
    }

    [Test]
    public async Task GetDocument_Test()
    {
        var newDocument = await documentService.GetDocument(docId);

        Assert.That(newDocument, Is.Not.Null);
        Assert.That(newDocument,Is.EqualTo(doc));
    }
    [Test]
    public async Task GetDocument_Admin_Test()
    {
        var newDocument = await documentService.GetDocument_Admin(docId);

        Assert.That(newDocument, Is.Not.Null);
        Assert.That(newDocument,Is.EqualTo(doc));

        newDocument = await documentService.GetDocument_Admin(docId2);

        Assert.That(newDocument, Is.Not.Null);
        Assert.That(newDocument,Is.EqualTo(doc2));
    }

    [Test]
    public async Task GetAll_Test()
    {
        var documents = await documentService.GetAll();
        Assert.That(documents, Is.Not.Null);
        Assert.That(documents.Count(), Is.EqualTo(1));
    }
    [Test]
    public async Task GetAll_Admin_Test()
    {
        var documents = await documentService.GetAll_Admin();
        Assert.That(documents, Is.Not.Null);
        Assert.That(documents.Count(), Is.EqualTo(2));
    }

    [Test]
    public async Task DeleteDocument_Test()
    {
        var deleteddoc = await documentService.DeleteDocument(docId2, userId);
        Assert.That(deleteddoc, Is.Not.Null);
        Assert.That(deleteddoc.IsDeleted, Is.EqualTo(true));
       
    }
    
    [TearDown]
    public async Task TearDown() {
       await  _context.DisposeAsync();
    }
}