using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComparativeComber.Migrations
{
    /// <inheritdoc />
    public partial class DAMNTHISTOHELL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "OrganizationId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 1, 7, 23, 48, 24, 811, DateTimeKind.Utc).AddTicks(3742), new DateTime(2024, 1, 7, 23, 48, 24, 811, DateTimeKind.Utc).AddTicks(3742) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "OrganizationId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 1, 7, 23, 46, 34, 269, DateTimeKind.Utc).AddTicks(2116), new DateTime(2024, 1, 7, 23, 46, 34, 269, DateTimeKind.Utc).AddTicks(2117) });
        }
    }
}
