namespace DocumentSharingSystem.Models
{
    public class UserTableLog
    {
        public long Id { get; set; }
        public Guid ModifiedUserId { get; set; }
        public string ModificationType { get; set; } = string.Empty;
        public string? OldValue { get; set; }
        public string? NewValue { get; set; }

        public Guid? ModifiedByUserId { get; set; }
        public DateTime ModifiedAt { get; set; }

        public User? ModifiedUser { get; set; }
        public User? ModifiedByUser { get; set; }
    }
}