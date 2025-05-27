using Microsoft.EntityFrameworkCore;
using Twitter.Models;

namespace Twitter.Contexts
{
    public class TwitterContext : DbContext
    {
        public TwitterContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> users { get; set; }
        public DbSet<Tweet> tweets { get; set; }
        public DbSet<Like> likes { get; set; }
        public DbSet<HashTag> hashtags { get; set; }
        public DbSet<HashTag_Tweet> hashtag_tweets { get; set; }
        public DbSet<Follow> follows{ get; set; }
    }
}