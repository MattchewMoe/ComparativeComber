using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComparativeComber.Migrations
{
    /// <inheritdoc />
    public partial class DAMNTHIS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "OrganizationId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 1, 7, 23, 46, 34, 269, DateTimeKind.Utc).AddTicks(2116), new DateTime(2024, 1, 7, 23, 46, 34, 269, DateTimeKind.Utc).AddTicks(2117) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "OrganizationId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 1, 7, 23, 42, 7, 812, DateTimeKind.Utc).AddTicks(9727), new DateTime(2024, 1, 7, 23, 42, 7, 812, DateTimeKind.Utc).AddTicks(9727) });
        }
    }
}
