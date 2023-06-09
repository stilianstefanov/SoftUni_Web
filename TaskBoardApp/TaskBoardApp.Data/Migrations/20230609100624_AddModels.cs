using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskBoardApp.Web.Data.Migrations
{
    public partial class AddModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BoardId = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_Boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Boards",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Open" });

            migrationBuilder.InsertData(
                table: "Boards",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "In Progress" });

            migrationBuilder.InsertData(
                table: "Boards",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Done" });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "BoardId", "CreatedOn", "Description", "OwnerId", "Title" },
                values: new object[,]
                {
                    { new Guid("1f9a71d6-6e88-49ab-a986-56af023bb969"), 1, new DateTime(2023, 1, 9, 10, 6, 23, 810, DateTimeKind.Utc).AddTicks(7946), "Create Android client App for the RESTful TaskBoard service", "5b7d619d-6f74-4f61-b386-f752958b8ea3", "Android Client App" },
                    { new Guid("3e2216fd-5459-488c-b005-b3a242adea41"), 2, new DateTime(2023, 5, 9, 10, 6, 23, 810, DateTimeKind.Utc).AddTicks(7950), "Create Desktop client App for the RESTful TaskBoard service", "f7e2d916-39c6-4240-8c80-185c2fea89b3", "Desktop Client App" },
                    { new Guid("53f86f34-040c-45ac-b3f5-16de1d8414d4"), 1, new DateTime(2022, 11, 21, 10, 6, 23, 810, DateTimeKind.Utc).AddTicks(7923), "Implement better styling for all public pages", "5b7d619d-6f74-4f61-b386-f752958b8ea3", "Improve CSS styles" },
                    { new Guid("84539879-7ed7-488b-870f-a023d9aac150"), 3, new DateTime(2022, 6, 9, 10, 6, 23, 810, DateTimeKind.Utc).AddTicks(7952), "Implement [Create Task] page for adding tasks", "f7e2d916-39c6-4240-8c80-185c2fea89b3", "Create Tasks" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_BoardId",
                table: "Tasks",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_OwnerId",
                table: "Tasks",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Boards");
        }
    }
}
