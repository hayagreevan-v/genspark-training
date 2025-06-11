using System;
using Microsoft.EntityFrameworkCore;
using DocumentSharingSystem.Contexts;
using DocumentSharingSystem.Models;

namespace DocumentSharingSystem.Repositories;

public class DocumentRepo : Repo<Guid, Document>
{
    public DocumentRepo(DocumentSharingSystemContext DocumentSharingSystemContext) : base(DocumentSharingSystemContext)
    {
    }


    public override async Task<Document> Get(Guid id)
    {
        Document? document = await _context.documents.FindAsync(id);
        if (document == null || document.IsDeleted) throw new Exception("Document not found");
        return document;
    }

    public override async Task<ICollection<Document>> GetAll()
    {
        var documents = _context.documents;
        if (documents == null) throw new Exception("No documents found");
        return await documents.ToListAsync();
    }
}
