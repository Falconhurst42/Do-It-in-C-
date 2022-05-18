using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoItInCpp.Migrations
{
    public partial class UMLUpdate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder) {
            
            migrationBuilder.DropColumn(
                name: "Body",
                table: "Snippet");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Snippet",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Snippet",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 5000,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Documentation",
                table: "Snippet",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "VersionID",
                table: "Snippet",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AddOn",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 10000, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddOn", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Include",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Include", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LanguageVersion",
                columns: table => new
                {
                    Year_XX = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageVersion", x => x.Year_XX);
                });

            migrationBuilder.CreateTable(
                name: "AddOnInSnippet",
                columns: table => new
                {
                    SnippetID = table.Column<int>(type: "int", nullable: false),
                    AddOnID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddOnInSnippet", x => new { x.SnippetID, x.AddOnID });
                    table.ForeignKey(
                        name: "FK_AddOnInSnippet_AddOn_AddOnID",
                        column: x => x.AddOnID,
                        principalTable: "AddOn",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AddOnInSnippet_Snippet_SnippetID",
                        column: x => x.SnippetID,
                        principalTable: "Snippet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "IncludeEquivalence",
                columns: table => new
                {
                    C_ID = table.Column<int>(type: "int", nullable: false),
                    CPP_ID = table.Column<int>(type: "int", nullable: false),
                    C_IncludeID = table.Column<int>(type: "int", nullable: false),
                    CPP_IncludeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncludeEquivalence", x => new { x.C_ID, x.CPP_ID });
                    table.ForeignKey(
                        name: "FK_IncludeEquivalence_Include_C_IncludeID",
                        column: x => x.C_IncludeID,
                        principalTable: "Include",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_IncludeEquivalence_Include_CPP_IncludeID",
                        column: x => x.CPP_IncludeID,
                        principalTable: "Include",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "IncludeInSnippet",
                columns: table => new
                {
                    SnippetID = table.Column<int>(type: "int", nullable: false),
                    IncludeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncludeInSnippet", x => new { x.SnippetID, x.IncludeID });
                    table.ForeignKey(
                        name: "FK_IncludeInSnippet_Include_IncludeID",
                        column: x => x.IncludeID,
                        principalTable: "Include",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_IncludeInSnippet_Snippet_SnippetID",
                        column: x => x.SnippetID,
                        principalTable: "Snippet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Snippet_VersionID",
                table: "Snippet",
                column: "VersionID");

            migrationBuilder.CreateIndex(
                name: "IX_AddOnInSnippet_AddOnID",
                table: "AddOnInSnippet",
                column: "AddOnID");

            migrationBuilder.CreateIndex(
                name: "IX_IncludeEquivalence_C_IncludeID",
                table: "IncludeEquivalence",
                column: "C_IncludeID");

            migrationBuilder.CreateIndex(
                name: "IX_IncludeEquivalence_CPP_IncludeID",
                table: "IncludeEquivalence",
                column: "CPP_IncludeID");

            migrationBuilder.CreateIndex(
                name: "IX_IncludeInSnippet_IncludeID",
                table: "IncludeInSnippet",
                column: "IncludeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Snippet_LanguageVersion_VersionID",
                table: "Snippet",
                column: "VersionID",
                principalTable: "LanguageVersion",
                principalColumn: "Year_XX",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Snippet_LanguageVersion_VersionID",
                table: "Snippet");

            migrationBuilder.DropTable(
                name: "AddOnInSnippet");

            migrationBuilder.DropTable(
                name: "IncludeEquivalence");

            migrationBuilder.DropTable(
                name: "IncludeInSnippet");

            migrationBuilder.DropTable(
                name: "LanguageVersion");

            migrationBuilder.DropTable(
                name: "AddOn");

            migrationBuilder.DropTable(
                name: "Include");

            migrationBuilder.DropIndex(
                name: "IX_Snippet_VersionID",
                table: "Snippet");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Snippet");

            migrationBuilder.DropColumn(
                name: "Documentation",
                table: "Snippet");

            migrationBuilder.DropColumn(
                name: "VersionID",
                table: "Snippet");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Snippet",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 5000);

            migrationBuilder.AddColumn<string>(
                name: "Body",
                table: "Snippet",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                defaultValue: "");
        }
    }
}