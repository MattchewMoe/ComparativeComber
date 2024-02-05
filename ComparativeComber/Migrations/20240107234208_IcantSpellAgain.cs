using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComparativeComber.Migrations
{
    /// <inheritdoc />
    public partial class IcantSpellAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "OrganizationId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 1, 7, 23, 42, 7, 812, DateTimeKind.Utc).AddTicks(9727), new DateTime(2024, 1, 7, 23, 42, 7, 812, DateTimeKind.Utc).AddTicks(9727) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "OrganizationId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 1, 7, 23, 38, 6, 887, DateTimeKind.Utc).AddTicks(3172), new DateTime(2024, 1, 7, 23, 38, 6, 887, DateTimeKind.Utc).AddTicks(3173) });
        }
    }
}
