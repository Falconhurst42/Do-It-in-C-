using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoItInCpp.Migrations
{
    public partial class FixIncludeEquiv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IncludeEquivalence_Include_C_IncludeID",
                table: "IncludeEquivalence");

            migrationBuilder.DropForeignKey(
                name: "FK_IncludeEquivalence_Include_CPP_IncludeID",
                table: "IncludeEquivalence");

            migrationBuilder.DropIndex(
                name: "IX_IncludeEquivalence_C_IncludeID",
                table: "IncludeEquivalence");

            migrationBuilder.DropIndex(
                name: "IX_IncludeEquivalence_CPP_IncludeID",
                table: "IncludeEquivalence");

            migrationBuilder.DropColumn(
                name: "CPP_IncludeID",
                table: "IncludeEquivalence");

            migrationBuilder.DropColumn(
                name: "C_IncludeID",
                table: "IncludeEquivalence");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CPP_IncludeID",
                table: "IncludeEquivalence",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "C_IncludeID",
                table: "IncludeEquivalence",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_IncludeEquivalence_C_IncludeID",
                table: "IncludeEquivalence",
                column: "C_IncludeID");

            migrationBuilder.CreateIndex(
                name: "IX_IncludeEquivalence_CPP_IncludeID",
                table: "IncludeEquivalence",
                column: "CPP_IncludeID");

            migrationBuilder.AddForeignKey(
                name: "FK_IncludeEquivalence_Include_C_IncludeID",
                table: "IncludeEquivalence",
                column: "C_IncludeID",
                principalTable: "Include",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IncludeEquivalence_Include_CPP_IncludeID",
                table: "IncludeEquivalence",
                column: "CPP_IncludeID",
                principalTable: "Include",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
