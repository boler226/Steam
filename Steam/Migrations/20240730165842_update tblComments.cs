using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Steam.Migrations
{
    /// <inheritdoc />
    public partial class updatetblComments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblNews_tblGameVideo_VideoId",
                table: "tblNews");

            migrationBuilder.DropIndex(
                name: "IX_tblNews_VideoId",
                table: "tblNews");

            migrationBuilder.DropColumn(
                name: "VideoId",
                table: "tblNews");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "tblNews",
                newName: "ImageOrVideo");

            migrationBuilder.AlterColumn<string>(
                name: "Photo",
                table: "tblUser",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NewsId",
                table: "Comments",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "Comments",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageOrVideo",
                table: "tblNews",
                newName: "Image");

            migrationBuilder.AlterColumn<string>(
                name: "Photo",
                table: "tblUser",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AddColumn<int>(
                name: "VideoId",
                table: "tblNews",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "NewsId",
                table: "Comments",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "Comments",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblNews_VideoId",
                table: "tblNews",
                column: "VideoId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblNews_tblGameVideo_VideoId",
                table: "tblNews",
                column: "VideoId",
                principalTable: "tblGameVideo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
