using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InsightIn_Note_DBContext.Migrations
{
    public partial class AddNotesToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    NoteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoteCategoryID = table.Column<int>(type: "int", nullable: false),
                    NoteTitle = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    NoteDescription = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Reminder = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.NoteID);
                    table.ForeignKey(
                        name: "FK_Notes_NoteCategories_NoteCategoryID",
                        column: x => x.NoteCategoryID,
                        principalTable: "NoteCategories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notes_NoteCategoryID",
                table: "Notes",
                column: "NoteCategoryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notes");
        }
    }
}
