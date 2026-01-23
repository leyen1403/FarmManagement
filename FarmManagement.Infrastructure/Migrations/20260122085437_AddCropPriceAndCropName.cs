using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCropPriceAndCropName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Crops",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "CropPrices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CropId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CropPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CropPrices_Crops_CropId",
                        column: x => x.CropId,
                        principalTable: "Crops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "LocationStatuses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 22, 8, 54, 36, 372, DateTimeKind.Utc).AddTicks(4465), new DateTime(2026, 1, 22, 8, 54, 36, 372, DateTimeKind.Utc).AddTicks(4468) });

            migrationBuilder.UpdateData(
                table: "LocationStatuses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 22, 8, 54, 36, 372, DateTimeKind.Utc).AddTicks(4475), new DateTime(2026, 1, 22, 8, 54, 36, 372, DateTimeKind.Utc).AddTicks(4475) });

            migrationBuilder.UpdateData(
                table: "LocationTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 22, 8, 54, 36, 372, DateTimeKind.Utc).AddTicks(6405), new DateTime(2026, 1, 22, 8, 54, 36, 372, DateTimeKind.Utc).AddTicks(6407) });

            migrationBuilder.UpdateData(
                table: "LocationTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 22, 8, 54, 36, 372, DateTimeKind.Utc).AddTicks(6410), new DateTime(2026, 1, 22, 8, 54, 36, 372, DateTimeKind.Utc).AddTicks(6411) });

            migrationBuilder.CreateIndex(
                name: "IX_Crops_LocationId",
                table: "Crops",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_CropPrices_CropId",
                table: "CropPrices",
                column: "CropId");

            migrationBuilder.AddForeignKey(
                name: "FK_Crops_Locations_LocationId",
                table: "Crops",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Crops_Locations_LocationId",
                table: "Crops");

            migrationBuilder.DropTable(
                name: "CropPrices");

            migrationBuilder.DropIndex(
                name: "IX_Crops_LocationId",
                table: "Crops");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Crops");

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
    }
}
