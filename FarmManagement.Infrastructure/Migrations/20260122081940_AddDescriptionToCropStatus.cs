using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDescriptionToCropStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CropStatuses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "LocationStatuses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 22, 8, 19, 39, 175, DateTimeKind.Utc).AddTicks(82), new DateTime(2026, 1, 22, 8, 19, 39, 175, DateTimeKind.Utc).AddTicks(86) });

            migrationBuilder.UpdateData(
                table: "LocationStatuses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 22, 8, 19, 39, 175, DateTimeKind.Utc).AddTicks(95), new DateTime(2026, 1, 22, 8, 19, 39, 175, DateTimeKind.Utc).AddTicks(96) });

            migrationBuilder.UpdateData(
                table: "LocationTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 22, 8, 19, 39, 175, DateTimeKind.Utc).AddTicks(1828), new DateTime(2026, 1, 22, 8, 19, 39, 175, DateTimeKind.Utc).AddTicks(1829) });

            migrationBuilder.UpdateData(
                table: "LocationTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 22, 8, 19, 39, 175, DateTimeKind.Utc).AddTicks(1832), new DateTime(2026, 1, 22, 8, 19, 39, 175, DateTimeKind.Utc).AddTicks(1832) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "CropStatuses");

            migrationBuilder.UpdateData(
                table: "LocationStatuses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 12, 16, 8, 59, 53, 494, DateTimeKind.Utc).AddTicks(955), new DateTime(2025, 12, 16, 8, 59, 53, 494, DateTimeKind.Utc).AddTicks(958) });

            migrationBuilder.UpdateData(
                table: "LocationStatuses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 12, 16, 8, 59, 53, 494, DateTimeKind.Utc).AddTicks(964), new DateTime(2025, 12, 16, 8, 59, 53, 494, DateTimeKind.Utc).AddTicks(964) });

            migrationBuilder.UpdateData(
                table: "LocationTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 12, 16, 8, 59, 53, 494, DateTimeKind.Utc).AddTicks(2570), new DateTime(2025, 12, 16, 8, 59, 53, 494, DateTimeKind.Utc).AddTicks(2572) });

            migrationBuilder.UpdateData(
                table: "LocationTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 12, 16, 8, 59, 53, 494, DateTimeKind.Utc).AddTicks(2576), new DateTime(2025, 12, 16, 8, 59, 53, 494, DateTimeKind.Utc).AddTicks(2576) });
        }
    }
}
