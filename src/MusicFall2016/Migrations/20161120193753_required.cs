using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicFall2016.Migrations
{
    public partial class required : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Playlists_AspNetUsers_userId",
                table: "Playlists");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "Playlists",
                nullable: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Playlists_AspNetUsers_UserId",
                table: "Playlists",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Playlists",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Playlists_userId",
                table: "Playlists",
                newName: "IX_Playlists_UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Playlists_AspNetUsers_UserId",
                table: "Playlists");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "Playlists",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Playlists_AspNetUsers_userId",
                table: "Playlists",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Playlists",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_Playlists_UserId",
                table: "Playlists",
                newName: "IX_Playlists_userId");
        }
    }
}
