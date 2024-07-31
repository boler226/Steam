using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Steam.Migrations
{
    /// <inheritdoc />
    public partial class updatetblNewsnullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblNews_tblGame_GameId",
                table: "tblNews");

            migrationBuilder.DropForeignKey(
                name: "FK_tblNews_tblUser_UserId",
                table: "tblNews");

            migrationBuilder.DropIndex(
                name: "IX_tblNews_UserId",
                table: "tblNews");

            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "tblNews",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "UserOrDeveloperId",
                table: "tblNews",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tblNews_UserOrDeveloperId",
                table: "tblNews",
                column: "UserOrDeveloperId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblNews_tblGame_GameId",
                table: "tblNews",
                column: "GameId",
                principalTable: "tblGame",
                principalColumn: "Id");

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
                name: "FK_tblNews_tblGame_GameId",
                table: "tblNews");

            migrationBuilder.DropForeignKey(
                name: "FK_tblNews_tblUser_UserOrDeveloperId",
                table: "tblNews");

            migrationBuilder.DropIndex(
                name: "IX_tblNews_UserOrDeveloperId",
                table: "tblNews");

            migrationBuilder.DropColumn(
                name: "UserOrDeveloperId",
                table: "tblNews");

            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "tblNews",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblNews_UserId",
                table: "tblNews",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblNews_tblGame_GameId",
                table: "tblNews",
                column: "GameId",
                principalTable: "tblGame",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tblNews_tblUser_UserId",
                table: "tblNews",
                column: "UserId",
                principalTable: "tblUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
