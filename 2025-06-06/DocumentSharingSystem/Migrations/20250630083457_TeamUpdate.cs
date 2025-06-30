using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocumentSharingSystem.Migrations
{
    /// <inheritdoc />
    public partial class TeamUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "teams",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserId",
                table: "teams",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedAt",
                table: "teams",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastUpdatedByUserId",
                table: "teams",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_teams_CreatedByUserId",
                table: "teams",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_teams_LastUpdatedByUserId",
                table: "teams",
                column: "LastUpdatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Team_Document",
                table: "teams",
                column: "CreatedByUserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_teams_users_LastUpdatedByUserId",
                table: "teams",
                column: "LastUpdatedByUserId",
                principalTable: "users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Team_Document",
                table: "teams");

            migrationBuilder.DropForeignKey(
                name: "FK_teams_users_LastUpdatedByUserId",
                table: "teams");

            migrationBuilder.DropIndex(
                name: "IX_teams_CreatedByUserId",
                table: "teams");

            migrationBuilder.DropIndex(
                name: "IX_teams_LastUpdatedByUserId",
                table: "teams");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "teams");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "teams");

            migrationBuilder.DropColumn(
                name: "LastUpdatedAt",
                table: "teams");

            migrationBuilder.DropColumn(
                name: "LastUpdatedByUserId",
                table: "teams");
        }
    }
}
