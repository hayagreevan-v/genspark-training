using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Twitter.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Age = table.Column<int>(type: "integer", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    ContactNo = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "follows",
                columns: table => new
                {
                    FollowId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FollowerUserId = table.Column<int>(type: "integer", nullable: false),
                    FollowedUserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_follows", x => x.FollowId);
                    table.ForeignKey(
                        name: "FK_follows_users_FollowedUserId",
                        column: x => x.FollowedUserId,
                        principalTable: "users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_follows_users_FollowerUserId",
                        column: x => x.FollowerUserId,
                        principalTable: "users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tweets",
                columns: table => new
                {
                    TweetId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PostedByUserId = table.Column<int>(type: "integer", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tweets", x => x.TweetId);
                    table.ForeignKey(
                        name: "FK_tweets_users_PostedByUserId",
                        column: x => x.PostedByUserId,
                        principalTable: "users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "hashtags",
                columns: table => new
                {
                    HashTagId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HashTagContent = table.Column<string>(type: "text", nullable: false),
                    TweetId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hashtags", x => x.HashTagId);
                    table.ForeignKey(
                        name: "FK_hashtags_tweets_TweetId",
                        column: x => x.TweetId,
                        principalTable: "tweets",
                        principalColumn: "TweetId");
                });

            migrationBuilder.CreateTable(
                name: "likes",
                columns: table => new
                {
                    LikeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TweetId = table.Column<int>(type: "integer", nullable: false),
                    LikedByUserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_likes", x => x.LikeId);
                    table.ForeignKey(
                        name: "FK_likes_tweets_TweetId",
                        column: x => x.TweetId,
                        principalTable: "tweets",
                        principalColumn: "TweetId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_likes_users_LikedByUserId",
                        column: x => x.LikedByUserId,
                        principalTable: "users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "hashtag_tweets",
                columns: table => new
                {
                    HashTagTweetId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HashTagId = table.Column<int>(type: "integer", nullable: false),
                    TweetId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hashtag_tweets", x => x.HashTagTweetId);
                    table.ForeignKey(
                        name: "FK_hashtag_tweets_hashtags_HashTagId",
                        column: x => x.HashTagId,
                        principalTable: "hashtags",
                        principalColumn: "HashTagId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_hashtag_tweets_tweets_TweetId",
                        column: x => x.TweetId,
                        principalTable: "tweets",
                        principalColumn: "TweetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_follows_FollowedUserId",
                table: "follows",
                column: "FollowedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_follows_FollowerUserId",
                table: "follows",
                column: "FollowerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_hashtag_tweets_HashTagId",
                table: "hashtag_tweets",
                column: "HashTagId");

            migrationBuilder.CreateIndex(
                name: "IX_hashtag_tweets_TweetId",
                table: "hashtag_tweets",
                column: "TweetId");

            migrationBuilder.CreateIndex(
                name: "IX_hashtags_TweetId",
                table: "hashtags",
                column: "TweetId");

            migrationBuilder.CreateIndex(
                name: "IX_likes_LikedByUserId",
                table: "likes",
                column: "LikedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_likes_TweetId",
                table: "likes",
                column: "TweetId");

            migrationBuilder.CreateIndex(
                name: "IX_tweets_PostedByUserId",
                table: "tweets",
                column: "PostedByUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "follows");

            migrationBuilder.DropTable(
                name: "hashtag_tweets");

            migrationBuilder.DropTable(
                name: "likes");

            migrationBuilder.DropTable(
                name: "hashtags");

            migrationBuilder.DropTable(
                name: "tweets");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
