using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComparativeComber.Migrations
{
    /// <inheritdoc />
    public partial class AdjustingNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Organizations",
                newName: "OganizationId");

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "OganizationId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 1, 7, 23, 32, 40, 219, DateTimeKind.Utc).AddTicks(3525), new DateTime(2024, 1, 7, 23, 32, 40, 219, DateTimeKind.Utc).AddTicks(3525) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OganizationId",
                table: "Organizations",
                newName: "Id");

            migrationBuilder.UpdateData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 1, 7, 23, 28, 25, 634, DateTimeKind.Utc).AddTicks(5672), new DateTime(2024, 1, 7, 23, 28, 25, 634, DateTimeKind.Utc).AddTicks(5673) });
        }
    }
}
