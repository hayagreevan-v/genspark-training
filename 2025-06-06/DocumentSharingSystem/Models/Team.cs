namespace DocumentSharingSystem.Models
{
    public class Team
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;

        public Guid CreatedByUserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid LastUpdatedByUserId { get; set; }
        public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;

        public List<User>? TeamMembers { get; set; }
        public List<Document>? TeamDocuments { get; set; }
        public User? CreatedByUser { get; set; }
        public User? LastUpdatedByUser { get; set; }
    }
}