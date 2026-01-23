using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedLocationTypeAndStatus3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "CropStatuses",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "CropStatuses");

            migrationBuilder.UpdateData(
                table: "LocationStatuses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 12, 16, 8, 9, 44, 809, DateTimeKind.Utc).AddTicks(9108), new DateTime(2025, 12, 16, 8, 9, 44, 809, DateTimeKind.Utc).AddTicks(9112) });

            migrationBuilder.UpdateData(
                table: "LocationStatuses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 12, 16, 8, 9, 44, 809, DateTimeKind.Utc).AddTicks(9121), new DateTime(2025, 12, 16, 8, 9, 44, 809, DateTimeKind.Utc).AddTicks(9121) });

            migrationBuilder.UpdateData(
                table: "LocationTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 12, 16, 8, 9, 44, 810, DateTimeKind.Utc).AddTicks(910), new DateTime(2025, 12, 16, 8, 9, 44, 810, DateTimeKind.Utc).AddTicks(911) });

            migrationBuilder.UpdateData(
                table: "LocationTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 12, 16, 8, 9, 44, 810, DateTimeKind.Utc).AddTicks(914), new DateTime(2025, 12, 16, 8, 9, 44, 810, DateTimeKind.Utc).AddTicks(916) });
        }
    }
}
