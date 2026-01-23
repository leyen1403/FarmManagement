using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FarmManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedLocationTypeAndStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "LocationStatuses",
                columns: new[] { "Id", "Code", "CreatedDate", "DeletedDate", "IsDeleted", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "ACTIVE", new DateTime(2025, 12, 16, 4, 59, 42, 512, DateTimeKind.Utc).AddTicks(4248), null, false, "Đang hoạt động", new DateTime(2025, 12, 16, 4, 59, 42, 512, DateTimeKind.Utc).AddTicks(4255) },
                    { 2, "INACTIVE", new DateTime(2025, 12, 16, 4, 59, 42, 512, DateTimeKind.Utc).AddTicks(4261), null, false, "Ngừng hoạt động", new DateTime(2025, 12, 16, 4, 59, 42, 512, DateTimeKind.Utc).AddTicks(4261) }
                });

            migrationBuilder.InsertData(
                table: "LocationTypes",
                columns: new[] { "Id", "Code", "CreatedDate", "DeletedDate", "IsDeleted", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "FARM", new DateTime(2025, 12, 16, 4, 59, 42, 512, DateTimeKind.Utc).AddTicks(6799), null, false, "Nông trại", new DateTime(2025, 12, 16, 4, 59, 42, 512, DateTimeKind.Utc).AddTicks(6801) },
                    { 2, "WAREHOUSE", new DateTime(2025, 12, 16, 4, 59, 42, 512, DateTimeKind.Utc).AddTicks(6806), null, false, "Kho", new DateTime(2025, 12, 16, 4, 59, 42, 512, DateTimeKind.Utc).AddTicks(6806) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LocationStatuses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "LocationStatuses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "LocationTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "LocationTypes",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
