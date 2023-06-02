using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forum.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Content", "Title" },
                values: new object[] { new Guid("aad1921c-4c2d-4876-a228-4d82843a76e5"), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque vel pretium velit, eget imperdiet massa. In diam dolor, hendrerit. ", "My third post" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Content", "Title" },
                values: new object[] { new Guid("eb765af1-79d6-4c73-a9e4-ddef2bfbe4f1"), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed venenatis libero vel nibh ultricies mattis. Sed sagittis sem in leo.", "My first post" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Content", "Title" },
                values: new object[] { new Guid("edb8b8d7-9bb8-4fc4-9845-bd1d134267a6"), "Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit...", "My second post" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");
        }
    }
}
