using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoItInCpp.Migrations
{
    public partial class DateUpdater : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "Snippet",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                defaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Snippet",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Snippet",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                defaultValueSql: "GETDATE()");
                
            migrationBuilder.Sql("CREATE TRIGGER dbo.snippet_lu_update ON dbo.Snippet AFTER UPDATE AS BEGIN UPDATE dbo.Snippet SET LastUpdated = GETDATE() WHERE ID IN(SELECT DISTINCT ID FROM Inserted) END");
            migrationBuilder.Sql("CREATE TRIGGER dbo.snippet_date_init ON dbo.Snippet AFTER INSERT AS BEGIN UPDATE dbo.Snippet SET LastUpdated = GETDATE(), Created = GETDATE() WHERE ID IN(SELECT DISTINCT ID FROM Inserted) END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "Snippet",
                type: "datetime2",
                nullable: false,
                defaultValueSql: null,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Snippet",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 5000,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Snippet",
                type: "datetime2",
                nullable: false,
                defaultValueSql: null,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.Sql("DROP TRIGGER dbo.snippet_lu_update");
            migrationBuilder.Sql("CREATE TRIGGER dbo.snippet_date_init");
        }
    }
}
