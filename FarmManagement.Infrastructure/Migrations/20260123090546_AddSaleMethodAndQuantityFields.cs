using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FarmManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSaleMethodAndQuantityFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SaleTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "SaleTypes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.AddColumn<int>(
                name: "SaleMethod",
                table: "SaleTypes",
                type: "int",
                nullable: false,
                defaultValue: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Weight",
                table: "LivestockSales",
                type: "decimal(10,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "LivestockSales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SetQuantity",
                table: "LivestockSales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "CropStatuses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 9, 5, 46, 172, DateTimeKind.Utc).AddTicks(2223), new DateTime(2026, 1, 23, 9, 5, 46, 172, DateTimeKind.Utc).AddTicks(2228) });

            migrationBuilder.UpdateData(
                table: "CropStatuses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 9, 5, 46, 172, DateTimeKind.Utc).AddTicks(2234), new DateTime(2026, 1, 23, 9, 5, 46, 172, DateTimeKind.Utc).AddTicks(2234) });

            migrationBuilder.UpdateData(
                table: "CropStatuses",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 9, 5, 46, 172, DateTimeKind.Utc).AddTicks(2236), new DateTime(2026, 1, 23, 9, 5, 46, 172, DateTimeKind.Utc).AddTicks(2237) });

            migrationBuilder.UpdateData(
                table: "LocationStatuses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 9, 5, 46, 175, DateTimeKind.Utc).AddTicks(4046), new DateTime(2026, 1, 23, 9, 5, 46, 175, DateTimeKind.Utc).AddTicks(4049) });

            migrationBuilder.UpdateData(
                table: "LocationStatuses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 9, 5, 46, 175, DateTimeKind.Utc).AddTicks(4053), new DateTime(2026, 1, 23, 9, 5, 46, 175, DateTimeKind.Utc).AddTicks(4054) });

            migrationBuilder.UpdateData(
                table: "LocationTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 9, 5, 46, 175, DateTimeKind.Utc).AddTicks(5635), new DateTime(2026, 1, 23, 9, 5, 46, 175, DateTimeKind.Utc).AddTicks(5636) });

            migrationBuilder.UpdateData(
                table: "LocationTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 9, 5, 46, 175, DateTimeKind.Utc).AddTicks(5639), new DateTime(2026, 1, 23, 9, 5, 46, 175, DateTimeKind.Utc).AddTicks(5639) });

            migrationBuilder.UpdateData(
                table: "SaleTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Code", "Name", "SaleMethod" },
                values: new object[] { "PER_HEAD", "Bán theo con", 1 });

            migrationBuilder.UpdateData(
                table: "SaleTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Code", "Name", "SaleMethod" },
                values: new object[] { "PER_KG", "Bán theo kg", 2 });

            migrationBuilder.UpdateData(
                table: "SaleTypes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Code", "Name", "SaleMethod" },
                values: new object[] { "PER_LOT", "Bán theo lô/đàn", 3 });

            migrationBuilder.UpdateData(
                table: "SaleTypes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Code", "Name", "SaleMethod" },
                values: new object[] { "PER_SET", "Bán theo bộ/cặp", 4 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SaleMethod",
                table: "SaleTypes");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "LivestockSales");

            migrationBuilder.DropColumn(
                name: "SetQuantity",
                table: "LivestockSales");

            migrationBuilder.AlterColumn<decimal>(
                name: "Weight",
                table: "LivestockSales",
                type: "decimal(10,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 18,
                oldScale: 2,
                oldDefaultValue: 0m);

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

            migrationBuilder.UpdateData(
                table: "SaleTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Code", "Name" },
                values: new object[] { "RETAIL", "Bán lẻ" });

            migrationBuilder.UpdateData(
                table: "SaleTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Code", "Name" },
                values: new object[] { "WHOLESALE", "Bán sỉ" });

            migrationBuilder.UpdateData(
                table: "SaleTypes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Code", "Name" },
                values: new object[] { "CONTRACT", "Hợp đồng" });

            migrationBuilder.UpdateData(
                table: "SaleTypes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Code", "Name" },
                values: new object[] { "EXPORT", "Xuất khẩu" });

            migrationBuilder.InsertData(
                table: "SaleTypes",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 5, "SLAUGHTER", "Lò mổ" },
                    { 6, "BREEDING", "Giống" }
                });
        }
    }
}
