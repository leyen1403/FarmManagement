using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLivestockPrices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LivestockPrice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LivestockId = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    EffectiveFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EffectiveTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LivestockPrice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LivestockPrice_Livestocks_LivestockId",
                        column: x => x.LivestockId,
                        principalTable: "Livestocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "CropStatuses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 10, 1, 20, 533, DateTimeKind.Utc).AddTicks(770), new DateTime(2026, 1, 23, 10, 1, 20, 533, DateTimeKind.Utc).AddTicks(776) });

            migrationBuilder.UpdateData(
                table: "CropStatuses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 10, 1, 20, 533, DateTimeKind.Utc).AddTicks(784), new DateTime(2026, 1, 23, 10, 1, 20, 533, DateTimeKind.Utc).AddTicks(784) });

            migrationBuilder.UpdateData(
                table: "CropStatuses",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 10, 1, 20, 533, DateTimeKind.Utc).AddTicks(787), new DateTime(2026, 1, 23, 10, 1, 20, 533, DateTimeKind.Utc).AddTicks(788) });

            migrationBuilder.UpdateData(
                table: "LocationStatuses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 10, 1, 20, 538, DateTimeKind.Utc).AddTicks(8273), new DateTime(2026, 1, 23, 10, 1, 20, 538, DateTimeKind.Utc).AddTicks(8276) });

            migrationBuilder.UpdateData(
                table: "LocationStatuses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 10, 1, 20, 538, DateTimeKind.Utc).AddTicks(8281), new DateTime(2026, 1, 23, 10, 1, 20, 538, DateTimeKind.Utc).AddTicks(8281) });

            migrationBuilder.UpdateData(
                table: "LocationTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 10, 1, 20, 539, DateTimeKind.Utc).AddTicks(218), new DateTime(2026, 1, 23, 10, 1, 20, 539, DateTimeKind.Utc).AddTicks(218) });

            migrationBuilder.UpdateData(
                table: "LocationTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 10, 1, 20, 539, DateTimeKind.Utc).AddTicks(221), new DateTime(2026, 1, 23, 10, 1, 20, 539, DateTimeKind.Utc).AddTicks(221) });

            migrationBuilder.CreateIndex(
                name: "IX_LivestockPrice_LivestockId_Gender_EffectiveFrom",
                table: "LivestockPrice",
                columns: new[] { "LivestockId", "Gender", "EffectiveFrom" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LivestockPrice");

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
        }
    }
}
