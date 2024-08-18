using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace blogApp.Migrations
{
    public partial class updateMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AuthorEmail = table.Column<string>(type: "TEXT", nullable: false),
                    Blogid = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Text = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.id);
                    table.ForeignKey(
                        name: "FK_Comment_Blog_Blogid",
                        column: x => x.Blogid,
                        principalTable: "Blog",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_Blogid",
                table: "Comment",
                column: "Blogid");
        }
    }
}
