using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MusicFall2016.Migrations
{
    public partial class playList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Playlists",
                columns: table => new
                {
                    playListID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(nullable: true),
                    userId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Playlists", x => x.playListID);
                    table.ForeignKey(
                        name: "FK_Playlists_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlaylistExtension",
                columns: table => new
                {
                    playlistID = table.Column<int>(nullable: false),
                    albumID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaylistExtension", x => new { x.playlistID, x.albumID });
                    table.ForeignKey(
                        name: "FK_PlaylistExtension_Albums_albumID",
                        column: x => x.albumID,
                        principalTable: "Albums",
                        principalColumn: "AlbumID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlaylistExtension_Playlists_playlistID",
                        column: x => x.playlistID,
                        principalTable: "Playlists",
                        principalColumn: "playListID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Playlists_userId",
                table: "Playlists",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistExtension_albumID",
                table: "PlaylistExtension",
                column: "albumID");

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistExtension_playlistID",
                table: "PlaylistExtension",
                column: "playlistID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlaylistExtension");

            migrationBuilder.DropTable(
                name: "Playlists");
        }
    }
}
