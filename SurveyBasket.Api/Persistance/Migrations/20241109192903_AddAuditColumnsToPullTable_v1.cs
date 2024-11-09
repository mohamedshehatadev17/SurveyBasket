using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveyBasket.Api.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditColumnsToPullTable_v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Polls_AspNetUsers_UpdatedById1",
                table: "Polls");

            migrationBuilder.DropIndex(
                name: "IX_Polls_UpdatedById1",
                table: "Polls");

            migrationBuilder.DropColumn(
                name: "UpdatedById1",
                table: "Polls");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedById",
                table: "Polls",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Polls_UpdatedById",
                table: "Polls",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Polls_AspNetUsers_UpdatedById",
                table: "Polls",
                column: "UpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Polls_AspNetUsers_UpdatedById",
                table: "Polls");

            migrationBuilder.DropIndex(
                name: "IX_Polls_UpdatedById",
                table: "Polls");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedById",
                table: "Polls",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedById1",
                table: "Polls",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Polls_UpdatedById1",
                table: "Polls",
                column: "UpdatedById1");

            migrationBuilder.AddForeignKey(
                name: "FK_Polls_AspNetUsers_UpdatedById1",
                table: "Polls",
                column: "UpdatedById1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
