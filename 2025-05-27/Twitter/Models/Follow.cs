using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Twitter.Models
{
    public class Follow
    {
        [Key]
        public int FollowId { get; set; }
        public int FollowerUserId { get; set; }
        public int FollowedUserId { get; set; }
        


        [ForeignKey("FollowerUserId")]
        public User? Follower { get; set; }

        [ForeignKey("FollowedUserId")]
        public User? Followed { get; set; }
    }
}