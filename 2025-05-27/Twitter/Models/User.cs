using System.ComponentModel.DataAnnotations.Schema;

namespace Twitter.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Email { get; set; } = string.Empty;
        public string ContactNo { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;


        // public ICollection<User>? FollowedBy { get; set; }
        // public ICollection<User>? Following { get; set; }
        public ICollection<Tweet>? tweets{ get; set; }
    }
}