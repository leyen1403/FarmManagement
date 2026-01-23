using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FarmManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedCropStatuses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CropStatuses",
                columns: new[] { "Id", "Code", "CreatedDate", "DeletedDate", "Description", "IsActive", "IsDeleted", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "PLANTED", new DateTime(2026, 1, 23, 3, 34, 37, 835, DateTimeKind.Utc).AddTicks(4638), null, "Cây đang trong giai đoạn sinh trưởng", true, false, "Đang trồng", new DateTime(2026, 1, 23, 3, 34, 37, 835, DateTimeKind.Utc).AddTicks(4642) },
                    { 2, "HARVESTED", new DateTime(2026, 1, 23, 3, 34, 37, 835, DateTimeKind.Utc).AddTicks(4648), null, "Cây đã thu hoạch", true, false, "Đã thu hoạch", new DateTime(2026, 1, 23, 3, 34, 37, 835, DateTimeKind.Utc).AddTicks(4648) },
                    { 3, "INACTIVE", new DateTime(2026, 1, 23, 3, 34, 37, 835, DateTimeKind.Utc).AddTicks(4650), null, "Trạng thái không còn sử dụng", false, false, "Ngừng hoạt động", new DateTime(2026, 1, 23, 3, 34, 37, 835, DateTimeKind.Utc).AddTicks(4650) }
                });

            migrationBuilder.UpdateData(
                table: "LocationStatuses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 3, 34, 37, 838, DateTimeKind.Utc).AddTicks(1024), new DateTime(2026, 1, 23, 3, 34, 37, 838, DateTimeKind.Utc).AddTicks(1026) });

            migrationBuilder.UpdateData(
                table: "LocationStatuses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 3, 34, 37, 838, DateTimeKind.Utc).AddTicks(1030), new DateTime(2026, 1, 23, 3, 34, 37, 838, DateTimeKind.Utc).AddTicks(1030) });

            migrationBuilder.UpdateData(
                table: "LocationTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 3, 34, 37, 838, DateTimeKind.Utc).AddTicks(2648), new DateTime(2026, 1, 23, 3, 34, 37, 838, DateTimeKind.Utc).AddTicks(2650) });

            migrationBuilder.UpdateData(
                table: "LocationTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 3, 34, 37, 838, DateTimeKind.Utc).AddTicks(2652), new DateTime(2026, 1, 23, 3, 34, 37, 838, DateTimeKind.Utc).AddTicks(2652) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CropStatuses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CropStatuses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CropStatuses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "LocationStatuses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 2, 39, 20, 384, DateTimeKind.Utc).AddTicks(5438), new DateTime(2026, 1, 23, 2, 39, 20, 384, DateTimeKind.Utc).AddTicks(5442) });

            migrationBuilder.UpdateData(
                table: "LocationStatuses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 2, 39, 20, 384, DateTimeKind.Utc).AddTicks(5448), new DateTime(2026, 1, 23, 2, 39, 20, 384, DateTimeKind.Utc).AddTicks(5448) });

            migrationBuilder.UpdateData(
                table: "LocationTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 2, 39, 20, 384, DateTimeKind.Utc).AddTicks(7460), new DateTime(2026, 1, 23, 2, 39, 20, 384, DateTimeKind.Utc).AddTicks(7462) });

            migrationBuilder.UpdateData(
                table: "LocationTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 2, 39, 20, 384, DateTimeKind.Utc).AddTicks(7464), new DateTime(2026, 1, 23, 2, 39, 20, 384, DateTimeKind.Utc).AddTicks(7464) });
        }
    }
}
