using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedLocationTypeAndStatus2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "CropTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

            migrationBuilder.CreateIndex(
                name: "IX_CropTypes_Code",
                table: "CropTypes",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CropTypes_Code",
                table: "CropTypes");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "CropTypes");

            migrationBuilder.UpdateData(
                table: "LocationStatuses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 12, 16, 4, 59, 42, 512, DateTimeKind.Utc).AddTicks(4248), new DateTime(2025, 12, 16, 4, 59, 42, 512, DateTimeKind.Utc).AddTicks(4255) });

            migrationBuilder.UpdateData(
                table: "LocationStatuses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 12, 16, 4, 59, 42, 512, DateTimeKind.Utc).AddTicks(4261), new DateTime(2025, 12, 16, 4, 59, 42, 512, DateTimeKind.Utc).AddTicks(4261) });

            migrationBuilder.UpdateData(
                table: "LocationTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 12, 16, 4, 59, 42, 512, DateTimeKind.Utc).AddTicks(6799), new DateTime(2025, 12, 16, 4, 59, 42, 512, DateTimeKind.Utc).AddTicks(6801) });

            migrationBuilder.UpdateData(
                table: "LocationTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2025, 12, 16, 4, 59, 42, 512, DateTimeKind.Utc).AddTicks(6806), new DateTime(2025, 12, 16, 4, 59, 42, 512, DateTimeKind.Utc).AddTicks(6806) });
        }
    }
}
