using System.ComponentModel.DataAnnotations.Schema;

namespace Twitter.Models
{
    public class Tweet
    {
        public int TweetId { get; set; }
        public int PostedByUserId { get; set; }
        public string Content { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;

        [ForeignKey("PostedByUserId")]
        public User? PostByUser { get; set; }

        public ICollection<Like>? likes { get; set; }
        public ICollection<HashTag>? hashTags { get; set; }
    }
}