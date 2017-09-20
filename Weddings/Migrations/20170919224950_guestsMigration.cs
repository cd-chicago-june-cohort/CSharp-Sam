using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Weddings.Migrations
{
    public partial class guestsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Guest_users_userId",
                table: "Guest");

            migrationBuilder.DropForeignKey(
                name: "FK_Guest_weddings_weddingId",
                table: "Guest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Guest",
                table: "Guest");

            migrationBuilder.RenameTable(
                name: "Guest",
                newName: "guests");

            migrationBuilder.RenameIndex(
                name: "IX_Guest_weddingId",
                table: "guests",
                newName: "IX_guests_weddingId");

            migrationBuilder.RenameIndex(
                name: "IX_Guest_userId",
                table: "guests",
                newName: "IX_guests_userId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_guests",
                table: "guests",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_guests_users_userId",
                table: "guests",
                column: "userId",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_guests_weddings_weddingId",
                table: "guests",
                column: "weddingId",
                principalTable: "weddings",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_guests_users_userId",
                table: "guests");

            migrationBuilder.DropForeignKey(
                name: "FK_guests_weddings_weddingId",
                table: "guests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_guests",
                table: "guests");

            migrationBuilder.RenameTable(
                name: "guests",
                newName: "Guest");

            migrationBuilder.RenameIndex(
                name: "IX_guests_weddingId",
                table: "Guest",
                newName: "IX_Guest_weddingId");

            migrationBuilder.RenameIndex(
                name: "IX_guests_userId",
                table: "Guest",
                newName: "IX_Guest_userId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Guest",
                table: "Guest",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Guest_users_userId",
                table: "Guest",
                column: "userId",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Guest_weddings_weddingId",
                table: "Guest",
                column: "weddingId",
                principalTable: "weddings",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
