namespace DocumentSharingSystem.Models.DTOs
{
    public class UserFilterModel
    {
        public string? SearchQuery { get; set; }
        public string? SortOrder { get; set; }
        public string? Role { get; set; }
        public long? TeamId { get; set; }
    }
}