using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicFall2016.Migrations
{
    public partial class likeMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Like",
                table: "Albums",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Genres",
                maxLength: 20,
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Artists",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Bio",
                table: "Artists",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Albums",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Like",
                table: "Albums");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Genres",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Artists",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Bio",
                table: "Artists",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Albums",
                nullable: true);
        }
    }
}
