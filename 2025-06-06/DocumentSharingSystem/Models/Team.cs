namespace DocumentSharingSystem.Models
{
    public class Team
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;
        public List<User>? TeamMembers { get; set; }
        public List<Document>? TeamDocuments { get; set; }
    }
}