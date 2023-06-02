using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forum.Data.Migrations
{
    public partial class Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("aad1921c-4c2d-4876-a228-4d82843a76e5"));

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("eb765af1-79d6-4c73-a9e4-ddef2bfbe4f1"));

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("edb8b8d7-9bb8-4fc4-9845-bd1d134267a6"));

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Content", "Title" },
                values: new object[] { new Guid("0e2e76c8-1c1f-45e5-991d-2d7509a4e82f"), "Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit...", "My second post" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Content", "Title" },
                values: new object[] { new Guid("630ee4ac-aeca-42e1-81bc-498e12d21a06"), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque vel pretium velit, eget imperdiet massa. In diam dolor, hendrerit. ", "My third post" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Content", "Title" },
                values: new object[] { new Guid("e46d7ce8-a444-430d-9318-fc961101c6f0"), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed venenatis libero vel nibh ultricies mattis. Sed sagittis sem in leo.", "My first post" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("0e2e76c8-1c1f-45e5-991d-2d7509a4e82f"));

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("630ee4ac-aeca-42e1-81bc-498e12d21a06"));

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("e46d7ce8-a444-430d-9318-fc961101c6f0"));

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
    }
}
