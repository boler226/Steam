using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Steam.Migrations
{
    /// <inheritdoc />
    public partial class updatetblsGameUserNews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_tblUserGame_AspNetUsers_UserId",
                table: "tblUserGame");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "VideoURL",
                table: "tblNews");

            migrationBuilder.DropColumn(
                name: "SystemRequirements",
                table: "tblGame");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                newName: "tblUser");

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "tblNews",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "tblNews",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VideoId",
                table: "tblNews",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "tblGame",
                type: "integer",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AddColumn<int>(
                name: "DeveloperId",
                table: "tblGame",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "tblGame",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Photo",
                table: "tblUser",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(2500)",
                oldMaxLength: 2500,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_tblUser",
                table: "tblUser",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Comment = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Rating = table.Column<int>(type: "integer", nullable: false),
                    NewsId = table.Column<int>(type: "integer", nullable: false),
                    GameId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_tblGame_GameId",
                        column: x => x.GameId,
                        principalTable: "tblGame",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_tblNews_NewsId",
                        column: x => x.NewsId,
                        principalTable: "tblNews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_tblUser_UserId",
                        column: x => x.UserId,
                        principalTable: "tblUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblGameVideo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    GameId = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblGameVideo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblGameVideo_tblGame_GameId",
                        column: x => x.GameId,
                        principalTable: "tblGame",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblSystemRequirements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OperatingSystem = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Processor = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    RAM = table.Column<int>(type: "integer", nullable: false),
                    VideoCard = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    DiskSpace = table.Column<int>(type: "integer", nullable: false),
                    GameId = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSystemRequirements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblSystemRequirements_tblGame_GameId",
                        column: x => x.GameId,
                        principalTable: "tblGame",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblNews_UserId",
                table: "tblNews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tblNews_VideoId",
                table: "tblNews",
                column: "VideoId");

            migrationBuilder.CreateIndex(
                name: "IX_tblGame_DeveloperId",
                table: "tblGame",
                column: "DeveloperId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_GameId",
                table: "Comments",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_NewsId",
                table: "Comments",
                column: "NewsId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tblGameVideo_GameId",
                table: "tblGameVideo",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSystemRequirements_GameId",
                table: "tblSystemRequirements",
                column: "GameId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_tblUser_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "tblUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_tblUser_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "tblUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_tblUser_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "tblUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_tblUser_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "tblUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tblGame_tblUser_DeveloperId",
                table: "tblGame",
                column: "DeveloperId",
                principalTable: "tblUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tblNews_tblGameVideo_VideoId",
                table: "tblNews",
                column: "VideoId",
                principalTable: "tblGameVideo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tblNews_tblUser_UserId",
                table: "tblNews",
                column: "UserId",
                principalTable: "tblUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tblUserGame_tblUser_UserId",
                table: "tblUserGame",
                column: "UserId",
                principalTable: "tblUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_tblUser_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_tblUser_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_tblUser_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_tblUser_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_tblGame_tblUser_DeveloperId",
                table: "tblGame");

            migrationBuilder.DropForeignKey(
                name: "FK_tblNews_tblGameVideo_VideoId",
                table: "tblNews");

            migrationBuilder.DropForeignKey(
                name: "FK_tblNews_tblUser_UserId",
                table: "tblNews");

            migrationBuilder.DropForeignKey(
                name: "FK_tblUserGame_tblUser_UserId",
                table: "tblUserGame");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "tblGameVideo");

            migrationBuilder.DropTable(
                name: "tblSystemRequirements");

            migrationBuilder.DropIndex(
                name: "IX_tblNews_UserId",
                table: "tblNews");

            migrationBuilder.DropIndex(
                name: "IX_tblNews_VideoId",
                table: "tblNews");

            migrationBuilder.DropIndex(
                name: "IX_tblGame_DeveloperId",
                table: "tblGame");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tblUser",
                table: "tblUser");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "tblNews");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "tblNews");

            migrationBuilder.DropColumn(
                name: "VideoId",
                table: "tblNews");

            migrationBuilder.DropColumn(
                name: "DeveloperId",
                table: "tblGame");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "tblGame");

            migrationBuilder.RenameTable(
                name: "tblUser",
                newName: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "VideoURL",
                table: "tblNews",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "tblGame",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "SystemRequirements",
                table: "tblGame",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Photo",
                table: "AspNetUsers",
                type: "character varying(2500)",
                maxLength: 2500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "AspNetUsers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tblUserGame_AspNetUsers_UserId",
                table: "tblUserGame",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
