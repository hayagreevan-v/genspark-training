using System;
using System.Threading.Tasks;
using DocumentSharingSystem.Interfaces;
using DocumentSharingSystem.Misc;
using DocumentSharingSystem.Models;
using DocumentSharingSystem.Models.DTOs;

namespace DocumentSharingSystem.Services;

public class DocumentService
{
    private readonly IRepo<Guid, Document> _docRepo;
    private readonly PaginationContextFns _paginationContextFns;
    public DocumentService(IRepo<Guid, Document> docRepo, PaginationContextFns paginationContextFns)
    {
        _docRepo = docRepo;
        _paginationContextFns = paginationContextFns;
    }
    public async Task<Document> AddDocument(Document dto)
    {
        dto = await _docRepo.Add(dto);
        return dto;
    }
    public async Task<ICollection<Document>> GetAll()
    {
        var docs = await _docRepo.GetAll();
        docs = docs.Where(d => !d.IsDeleted).ToList();
        if (docs == null) throw new Exception("No document found");
        return docs;
    }
    public async Task<Document> GetDocument(Guid id)
    {
        var doc = await _docRepo.Get(id);
        if (doc == null) throw new Exception("No document found");
        return doc;
    }
    public async Task<ICollection<Document>> GetAll_Admin()
    {
        var docs = await _docRepo.GetAll();
        // docs = docs.Where(d => !d.IsDeleted).ToList();
        if (docs == null) throw new Exception("No document found");
        return docs;
    }
    public async Task<Document> GetDocument_Admin(Guid id)
    {
        var docs = await _docRepo.GetAll();
        var doc = docs.FirstOrDefault(d => d.Id == id);
        if (doc == null) throw new Exception("No document found");
        return doc;
    }
    public async Task<Document> DeleteDocument(Guid id, Guid userId)
    {
        var doc = await _docRepo.Delete(id, userId);
        return doc;
    }

    public async Task<PaginationDataDTO<Document>> DocumentsPagination_Admin(int pageNo, int pageSize)
    {
        return await _paginationContextFns.DocumentsPagination_Admin(pageNo, pageSize);
    }
    public async Task<PaginationDataDTO<Document>> DocumentsPagination(int pageNo, int pageSize)
    {
        return await _paginationContextFns.DocumentsPagination(pageNo, pageSize);
    }

    public async Task<List<Document>> Filter(DocumentFilterModel filter, string role)
    {
        var _docs = await _docRepo.GetAll();
        var docs = _docs.ToList();
        if (role != "Admin")
        {
            docs = docs.Where(d => !d.IsDeleted).ToList();
        }
        if (filter.SearchByOriginalFileName != null)
        {
            docs = docs.Where(d => d.OriginalFileName.Contains(filter.SearchByOriginalFileName, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        if (filter.SearchByCreatedUserId != null)
        {
            docs = docs.Where(d => d.CreatedByUserId == filter.SearchByCreatedUserId).ToList();
        }
        if (filter.SearchByCreatedTime != null)
        {
            docs = docs.Where(d => DateOnly.FromDateTime(d.CreatedAt) == DateOnly.FromDateTime((DateTime)filter.SearchByCreatedTime)).ToList();
        }
        if (filter.SortBy != null)
        {
            if (filter.SortBy.Equals("Id", StringComparison.OrdinalIgnoreCase))
            {
                docs = docs.OrderBy(d => d.Id).ToList();
            }
            if (filter.SortBy.Equals("StoredFileName", StringComparison.OrdinalIgnoreCase))
            {
                docs = docs.OrderBy(d => d.StoredFileName).ToList();
            }
            if (filter.SortBy.Equals("OriginalFileName", StringComparison.OrdinalIgnoreCase))
            {
                docs = docs.OrderBy(d => d.OriginalFileName).ToList();
            }
            if (filter.SortBy.Equals("CreatedByUserId", StringComparison.OrdinalIgnoreCase))
            {
                docs = docs.OrderBy(d => d.CreatedByUserId).ToList();
            }
            if (filter.SortBy.Equals("CreatedAt", StringComparison.OrdinalIgnoreCase))
            {
                docs = docs.OrderBy(d => d.CreatedAt).ToList();
            }
            if (filter.SortBy.Equals("LastUpdatedByUserId", StringComparison.OrdinalIgnoreCase))
            {
                docs = docs.OrderBy(d => d.LastUpdatedByUserId).ToList();
            }
            if (filter.SortBy.Equals("LastUpdatedAt", StringComparison.OrdinalIgnoreCase))
            {
                docs = docs.OrderBy(d => d.LastUpdatedAt).ToList();
            }
        }
        if (docs.Count() == 0) throw new Exception("NO documents found under the filter");
        return docs;
    }
}
