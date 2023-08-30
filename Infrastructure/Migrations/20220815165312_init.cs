using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Library");

            migrationBuilder.EnsureSchema(
                name: "Log");

            migrationBuilder.CreateTable(
                name: "Book",
                schema: "Library",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(type: "NVarchar(200)", nullable: true),
                    Total_pages = table.Column<int>(nullable: false),
                    Published_Date = table.Column<string>(type: "varchar(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Error",
                schema: "Log",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Method = table.Column<string>(type: "varchar(16)", nullable: true),
                    Path = table.Column<string>(type: "varchar(128)", nullable: true),
                    DateTime = table.Column<DateTime>(nullable: false),
                    StatusCode = table.Column<int>(nullable: true),
                    Data = table.Column<string>(type: "NVarchar(4000)", nullable: true),
                    ErrorMessage = table.Column<string>(type: "NVarchar(4000)", nullable: true),
                    ClientIp = table.Column<string>(type: "varchar(64)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Error", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Information",
                schema: "Log",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Method = table.Column<string>(type: "varchar(16)", nullable: true),
                    Path = table.Column<string>(type: "NVarchar(256)", nullable: true),
                    QueryString = table.Column<string>(type: "NVarchar(256)", nullable: true),
                    Type = table.Column<string>(type: "varchar(16)", nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false),
                    StatusCode = table.Column<int>(nullable: true),
                    Data = table.Column<string>(type: "NVarchar(4000)", nullable: true),
                    ClientIp = table.Column<string>(type: "varchar(64)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Information", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Chapter",
                schema: "Library",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(type: "NVarchar(200)", nullable: true),
                    Total_pages = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    BookId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chapter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chapter_Book_BookId",
                        column: x => x.BookId,
                        principalSchema: "Library",
                        principalTable: "Book",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chapter_BookId",
                schema: "Library",
                table: "Chapter",
                column: "BookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chapter",
                schema: "Library");

            migrationBuilder.DropTable(
                name: "Error",
                schema: "Log");

            migrationBuilder.DropTable(
                name: "Information",
                schema: "Log");

            migrationBuilder.DropTable(
                name: "Book",
                schema: "Library");
        }
    }
}
