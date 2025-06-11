using System;
using DocumentSharingSystem.Misc;

namespace DocumentSharingSystem.Models;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    [RoleValidation]
    public string Role { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public byte[]? Password { get; set; }
    public bool IsDeleted { get; set; } = false;
    // public byte[]? HashKey { get; set; }

    public Guid? CreatedByUserId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Guid? LastUpdatedByUserId { get; set; }
    public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;



    public User? CreatedByUser { get; set; }
    public User? LastUpdatedByUser { get; set; }
    public List<User>? CreatedUsers { get; set; }
    public List<Document>? CreatedDocuments { get; set; }
    public List<DocumentTableLog>? UpdatedDocumentLogs { get; set; }
    public List<UserTableLog>? UpdatedUserLogs { get; set; }
    public List<UserTableLog>? UpdatedByUserLogs { get; set; }
}
