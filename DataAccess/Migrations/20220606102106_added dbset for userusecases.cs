using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class addeddbsetforuserusecases : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserUseCase_Users_UserId",
                table: "UserUseCase");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserUseCase",
                table: "UserUseCase");

            migrationBuilder.RenameTable(
                name: "UserUseCase",
                newName: "UserUseCases");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserUseCases",
                table: "UserUseCases",
                columns: new[] { "UserId", "UseCaseId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserUseCases_Users_UserId",
                table: "UserUseCases",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserUseCases_Users_UserId",
                table: "UserUseCases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserUseCases",
                table: "UserUseCases");

            migrationBuilder.RenameTable(
                name: "UserUseCases",
                newName: "UserUseCase");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserUseCase",
                table: "UserUseCase",
                columns: new[] { "UserId", "UseCaseId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserUseCase_Users_UserId",
                table: "UserUseCase",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
