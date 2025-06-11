namespace DocumentSharingSystem.Models
{
    public class DocumentTableLog
    {
        public long Id { get; set; }
        public Guid ModifiedDocumentId { get; set; }
        public string ModificationType { get; set; } = string.Empty;
        public string? OldValue { get; set; }
        public string? NewValue { get; set; }

        public Guid? ModifiedByUserId { get; set; }
        public DateTime ModifiedAt { get; set; }

        public Document? ModifiedDocument { get; set; }
        public User? ModifiedByUser { get; set; }
    }
}