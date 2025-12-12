using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KePass.Server.Migrations
{
    /// <inheritdoc />
    public partial class Update01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Subscriptions",
                newName: "Type");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Audits",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Audits");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Subscriptions",
                newName: "Name");
        }
    }
}
