using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RedesignLivestockSaleWithDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "LivestockSales");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "LivestockSales");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "LivestockSales");

            migrationBuilder.RenameColumn(
                name: "SetQuantity",
                table: "LivestockSales",
                newName: "TotalQuantity");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalAmount",
                table: "LivestockSales",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "LivestockSales",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Buyer",
                table: "LivestockSales",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BuyerPhone",
                table: "LivestockSales",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "LivestockSales",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "OrderCode",
                table: "LivestockSales",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "LivestockSales",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalWeight",
                table: "LivestockSales",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "LivestockSaleDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LivestockSaleId = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    LineNumber = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LivestockSaleDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LivestockSaleDetails_LivestockSales_LivestockSaleId",
                        column: x => x.LivestockSaleId,
                        principalTable: "LivestockSales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "CropStatuses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 9, 34, 15, 947, DateTimeKind.Utc).AddTicks(812), new DateTime(2026, 1, 23, 9, 34, 15, 947, DateTimeKind.Utc).AddTicks(816) });

            migrationBuilder.UpdateData(
                table: "CropStatuses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 9, 34, 15, 947, DateTimeKind.Utc).AddTicks(822), new DateTime(2026, 1, 23, 9, 34, 15, 947, DateTimeKind.Utc).AddTicks(822) });

            migrationBuilder.UpdateData(
                table: "CropStatuses",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 9, 34, 15, 947, DateTimeKind.Utc).AddTicks(824), new DateTime(2026, 1, 23, 9, 34, 15, 947, DateTimeKind.Utc).AddTicks(825) });

            migrationBuilder.UpdateData(
                table: "LocationStatuses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 9, 34, 15, 951, DateTimeKind.Utc).AddTicks(5881), new DateTime(2026, 1, 23, 9, 34, 15, 951, DateTimeKind.Utc).AddTicks(5884) });

            migrationBuilder.UpdateData(
                table: "LocationStatuses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 9, 34, 15, 951, DateTimeKind.Utc).AddTicks(5891), new DateTime(2026, 1, 23, 9, 34, 15, 951, DateTimeKind.Utc).AddTicks(5892) });

            migrationBuilder.UpdateData(
                table: "LocationTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 9, 34, 15, 951, DateTimeKind.Utc).AddTicks(8049), new DateTime(2026, 1, 23, 9, 34, 15, 951, DateTimeKind.Utc).AddTicks(8053) });

            migrationBuilder.UpdateData(
                table: "LocationTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 9, 34, 15, 951, DateTimeKind.Utc).AddTicks(8055), new DateTime(2026, 1, 23, 9, 34, 15, 951, DateTimeKind.Utc).AddTicks(8056) });

            migrationBuilder.CreateIndex(
                name: "IX_LivestockSales_OrderCode",
                table: "LivestockSales",
                column: "OrderCode",
                unique: true,
                filter: "[OrderCode] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LivestockSales_SaleDate",
                table: "LivestockSales",
                column: "SaleDate");

            migrationBuilder.CreateIndex(
                name: "IX_LivestockSaleDetails_LivestockSaleId_LineNumber",
                table: "LivestockSaleDetails",
                columns: new[] { "LivestockSaleId", "LineNumber" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LivestockSaleDetails");

            migrationBuilder.DropIndex(
                name: "IX_LivestockSales_OrderCode",
                table: "LivestockSales");

            migrationBuilder.DropIndex(
                name: "IX_LivestockSales_SaleDate",
                table: "LivestockSales");

            migrationBuilder.DropColumn(
                name: "BuyerPhone",
                table: "LivestockSales");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "LivestockSales");

            migrationBuilder.DropColumn(
                name: "OrderCode",
                table: "LivestockSales");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "LivestockSales");

            migrationBuilder.DropColumn(
                name: "TotalWeight",
                table: "LivestockSales");

            migrationBuilder.RenameColumn(
                name: "TotalQuantity",
                table: "LivestockSales",
                newName: "SetQuantity");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalAmount",
                table: "LivestockSales",
                type: "decimal(10,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2,
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "LivestockSales",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Buyer",
                table: "LivestockSales",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "LivestockSales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                table: "LivestockSales",
                type: "decimal(10,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Weight",
                table: "LivestockSales",
                type: "decimal(10,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

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
        }
    }
}
