namespace DocumentSharingSystem.Models
{
    public class RefreshToken
    {
        public Guid UserId{ get; set; }
        public Guid Token{ get; set; }
    }
}