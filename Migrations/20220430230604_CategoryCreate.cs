using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoItInCpp.Migrations
{
    public partial class CategoryCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CategoryInSnippet",
                columns: table => new
                {
                    SnippetID = table.Column<int>(type: "int", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryInSnippet", x => new { x.SnippetID, x.CategoryID });
                    table.ForeignKey(
                        name: "FK_CategoryInSnippet_Category_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Category",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryInSnippet_Snippet_SnippetID",
                        column: x => x.SnippetID,
                        principalTable: "Snippet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryInSnippet_CategoryID",
                table: "CategoryInSnippet",
                column: "CategoryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryInSnippet");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
