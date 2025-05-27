using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Twitter.Models
{
    public class HashTag_Tweet
    {
        [Key]
        public int HashTagTweetId { get; set; }
        public int HashTagId { get; set; }

        public int TweetId { get; set; }

        [ForeignKey("HashTagId")]
        public HashTag? hashTag { get; set; }
        
        [ForeignKey("TweetId")]
        public Tweet? tweet { get; set; }
    }
}