using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Steam.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblDeveloperGame_tblUser_UserId",
                table: "tblDeveloperGame");

            migrationBuilder.DropForeignKey(
                name: "FK_tblNews_tblUser_UserOrDeveloperId",
                table: "tblNews");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "tblDeveloperGame",
                newName: "DeveloperId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblDeveloperGame_tblUser_DeveloperId",
                table: "tblDeveloperGame",
                column: "DeveloperId",
                principalTable: "tblUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tblNews_tblUser_UserOrDeveloperId",
                table: "tblNews",
                column: "UserOrDeveloperId",
                principalTable: "tblUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblDeveloperGame_tblUser_DeveloperId",
                table: "tblDeveloperGame");

            migrationBuilder.DropForeignKey(
                name: "FK_tblNews_tblUser_UserOrDeveloperId",
                table: "tblNews");

            migrationBuilder.RenameColumn(
                name: "DeveloperId",
                table: "tblDeveloperGame",
                newName: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblDeveloperGame_tblUser_UserId",
                table: "tblDeveloperGame",
                column: "UserId",
                principalTable: "tblUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tblNews_tblUser_UserOrDeveloperId",
                table: "tblNews",
                column: "UserOrDeveloperId",
                principalTable: "tblUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
