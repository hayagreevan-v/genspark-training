using System.ComponentModel.DataAnnotations.Schema;

namespace Twitter.Models
{
    public class Like
    {
        public int LikeId { get; set; }
        public int TweetId { get; set; }
        public int LikedByUserId { get; set; }

        [ForeignKey("TweetId")]
        public Tweet? tweet { get; set; }

        [ForeignKey("LikedByUserId")]
        public User? LikedByUser { get; set; }
    }
}