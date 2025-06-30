using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DocumentSharingSystem.Migrations
{
    /// <inheritdoc />
    public partial class Team : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TeamId",
                table: "users",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "documents",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TeamId",
                table: "documents",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "teams",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teams", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_users_TeamId",
                table: "users",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_documents_TeamId",
                table: "documents",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Team_Document",
                table: "documents",
                column: "TeamId",
                principalTable: "teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Team_User",
                table: "users",
                column: "TeamId",
                principalTable: "teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Team_Document",
                table: "documents");

            migrationBuilder.DropForeignKey(
                name: "FK_Team_User",
                table: "users");

            migrationBuilder.DropTable(
                name: "teams");

            migrationBuilder.DropIndex(
                name: "IX_users_TeamId",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_documents_TeamId",
                table: "documents");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "users");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "documents");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "documents");
        }
    }
}
