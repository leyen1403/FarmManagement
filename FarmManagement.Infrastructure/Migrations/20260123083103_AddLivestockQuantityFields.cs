using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLivestockQuantityFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FemaleCount",
                table: "Livestocks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaleCount",
                table: "Livestocks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Livestocks",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Livestocks",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalImportPrice",
                table: "Livestocks",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalImportWeight",
                table: "Livestocks",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "CropStatuses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 8, 31, 2, 488, DateTimeKind.Utc).AddTicks(8995), new DateTime(2026, 1, 23, 8, 31, 2, 488, DateTimeKind.Utc).AddTicks(9003) });

            migrationBuilder.UpdateData(
                table: "CropStatuses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 8, 31, 2, 488, DateTimeKind.Utc).AddTicks(9010), new DateTime(2026, 1, 23, 8, 31, 2, 488, DateTimeKind.Utc).AddTicks(9010) });

            migrationBuilder.UpdateData(
                table: "CropStatuses",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 8, 31, 2, 488, DateTimeKind.Utc).AddTicks(9012), new DateTime(2026, 1, 23, 8, 31, 2, 488, DateTimeKind.Utc).AddTicks(9013) });

            migrationBuilder.UpdateData(
                table: "LocationStatuses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 8, 31, 2, 493, DateTimeKind.Utc).AddTicks(4988), new DateTime(2026, 1, 23, 8, 31, 2, 493, DateTimeKind.Utc).AddTicks(4991) });

            migrationBuilder.UpdateData(
                table: "LocationStatuses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 8, 31, 2, 493, DateTimeKind.Utc).AddTicks(4998), new DateTime(2026, 1, 23, 8, 31, 2, 493, DateTimeKind.Utc).AddTicks(4998) });

            migrationBuilder.UpdateData(
                table: "LocationTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 8, 31, 2, 493, DateTimeKind.Utc).AddTicks(8250), new DateTime(2026, 1, 23, 8, 31, 2, 493, DateTimeKind.Utc).AddTicks(8252) });

            migrationBuilder.UpdateData(
                table: "LocationTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 8, 31, 2, 493, DateTimeKind.Utc).AddTicks(8256), new DateTime(2026, 1, 23, 8, 31, 2, 493, DateTimeKind.Utc).AddTicks(8256) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FemaleCount",
                table: "Livestocks");

            migrationBuilder.DropColumn(
                name: "MaleCount",
                table: "Livestocks");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Livestocks");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Livestocks");

            migrationBuilder.DropColumn(
                name: "TotalImportPrice",
                table: "Livestocks");

            migrationBuilder.DropColumn(
                name: "TotalImportWeight",
                table: "Livestocks");

            migrationBuilder.UpdateData(
                table: "CropStatuses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 8, 19, 0, 903, DateTimeKind.Utc).AddTicks(9790), new DateTime(2026, 1, 23, 8, 19, 0, 903, DateTimeKind.Utc).AddTicks(9793) });

            migrationBuilder.UpdateData(
                table: "CropStatuses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 8, 19, 0, 903, DateTimeKind.Utc).AddTicks(9799), new DateTime(2026, 1, 23, 8, 19, 0, 903, DateTimeKind.Utc).AddTicks(9799) });

            migrationBuilder.UpdateData(
                table: "CropStatuses",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 8, 19, 0, 903, DateTimeKind.Utc).AddTicks(9801), new DateTime(2026, 1, 23, 8, 19, 0, 903, DateTimeKind.Utc).AddTicks(9801) });

            migrationBuilder.UpdateData(
                table: "LocationStatuses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 8, 19, 0, 907, DateTimeKind.Utc).AddTicks(25), new DateTime(2026, 1, 23, 8, 19, 0, 907, DateTimeKind.Utc).AddTicks(29) });

            migrationBuilder.UpdateData(
                table: "LocationStatuses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 8, 19, 0, 907, DateTimeKind.Utc).AddTicks(32), new DateTime(2026, 1, 23, 8, 19, 0, 907, DateTimeKind.Utc).AddTicks(33) });

            migrationBuilder.UpdateData(
                table: "LocationTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 8, 19, 0, 907, DateTimeKind.Utc).AddTicks(1742), new DateTime(2026, 1, 23, 8, 19, 0, 907, DateTimeKind.Utc).AddTicks(1743) });

            migrationBuilder.UpdateData(
                table: "LocationTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 8, 19, 0, 907, DateTimeKind.Utc).AddTicks(1746), new DateTime(2026, 1, 23, 8, 19, 0, 907, DateTimeKind.Utc).AddTicks(1746) });
        }
    }
}
