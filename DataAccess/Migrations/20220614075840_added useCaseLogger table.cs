using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class addeduseCaseLoggertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Books_Isbn",
                table: "Books");

            migrationBuilder.AlterColumn<string>(
                name: "Isbn",
                table: "Books",
                type: "nchar(100)",
                fixedLength: true,
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nchar(450)",
                oldFixedLength: true);

            migrationBuilder.CreateTable(
                name: "UseCaseLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExecutionTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UseCaseName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAuthorized = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UseCaseLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UseCaseLogs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UseCaseLogs_UseCaseName",
                table: "UseCaseLogs",
                column: "UseCaseName");

            migrationBuilder.CreateIndex(
                name: "IX_UseCaseLogs_UserId",
                table: "UseCaseLogs",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UseCaseLogs");

            migrationBuilder.AlterColumn<string>(
                name: "Isbn",
                table: "Books",
                type: "nchar(450)",
                fixedLength: true,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nchar(100)",
                oldFixedLength: true,
                oldMaxLength: 100);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Books_Isbn",
                table: "Books",
                column: "Isbn");
        }
    }
}
