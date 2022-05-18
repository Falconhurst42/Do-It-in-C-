using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoItInCpp.Migrations
{
    public partial class CascadeUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IncludeInSnippet_Include_IncludeID",
                table: "IncludeInSnippet"
            );
            migrationBuilder.AddForeignKey(
                name: "FK_IncludeInSnippet_Include_IncludeID",
                table: "IncludeInSnippet",
                column: "IncludeID",
                principalTable: "Include",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade
            );
            migrationBuilder.DropForeignKey(
                name: "FK_IncludeInSnippet_Snippet_SnippetID",
                table: "IncludeInSnippet"
            );
            migrationBuilder.AddForeignKey(
                name: "FK_IncludeInSnippet_Snippet_SnippetID",
                table: "IncludeInSnippet",
                column: "SnippetID",
                principalTable: "Snippet",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.DropForeignKey(
                name: "FK_AddOnInSnippet_AddOn_AddOnID",
                table: "AddOnInSnippet"
            );
            migrationBuilder.AddForeignKey(
                name: "FK_AddOnInSnippet_AddOn_AddOnID",
                table: "AddOnInSnippet",
                column: "AddOnID",
                principalTable: "AddOn",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade
            );
            migrationBuilder.DropForeignKey(
                name: "FK_AddOnInSnippet_Snippet_SnippetID",
                table: "AddOnInSnippet"
            );
            migrationBuilder.AddForeignKey(
                name: "FK_AddOnInSnippet_Snippet_SnippetID",
                table: "AddOnInSnippet",
                column: "SnippetID",
                principalTable: "Snippet",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IncludeInSnippet_Include_IncludeID",
                table: "IncludeInSnippet"
            );
            migrationBuilder.AddForeignKey(
                name: "FK_IncludeInSnippet_Include_IncludeID",
                table: "IncludeInSnippet",
                column: "IncludeID",
                principalTable: "Include",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction
            );
            migrationBuilder.DropForeignKey(
                name: "FK_IncludeInSnippet_Snippet_SnippetID",
                table: "IncludeInSnippet"
            );
            migrationBuilder.AddForeignKey(
                name: "FK_IncludeInSnippet_Snippet_SnippetID",
                table: "IncludeInSnippet",
                column: "SnippetID",
                principalTable: "Snippet",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction
            );

            migrationBuilder.DropForeignKey(
                name: "FK_AddOnInSnippet_AddOn_AddOnID",
                table: "AddOnInSnippet"
            );
            migrationBuilder.AddForeignKey(
                name: "FK_AddOnInSnippet_AddOn_AddOnID",
                table: "AddOnInSnippet",
                column: "AddOnID",
                principalTable: "AddOn",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction
            );
            migrationBuilder.DropForeignKey(
                name: "FK_AddOnInSnippet_Snippet_SnippetID",
                table: "AddOnInSnippet"
            );
            migrationBuilder.AddForeignKey(
                name: "FK_AddOnInSnippet_Snippet_SnippetID",
                table: "AddOnInSnippet",
                column: "SnippetID",
                principalTable: "Snippet",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction
            );
        }
    }
}
