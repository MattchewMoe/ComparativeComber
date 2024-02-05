using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComparativeComber.Migrations
{
    /// <inheritdoc />
    public partial class IcantSpell : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OganizationId",
                table: "Organizations",
                newName: "OrganizationId");

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "OrganizationId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 1, 7, 23, 38, 6, 887, DateTimeKind.Utc).AddTicks(3172), new DateTime(2024, 1, 7, 23, 38, 6, 887, DateTimeKind.Utc).AddTicks(3173) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrganizationId",
                table: "Organizations",
                newName: "OganizationId");

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "OganizationId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 1, 7, 23, 32, 40, 219, DateTimeKind.Utc).AddTicks(3525), new DateTime(2024, 1, 7, 23, 32, 40, 219, DateTimeKind.Utc).AddTicks(3525) });
        }
    }
}
