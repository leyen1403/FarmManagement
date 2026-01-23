using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCostTypeAndCropCareTypeProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "CropCareTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "CropCareTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CropCareTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "CropCareTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CropCareTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "CropCareTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CostTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "CostTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "CropCareTypes");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "CropCareTypes");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "CropCareTypes");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "CropCareTypes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CropCareTypes");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "CropCareTypes");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "CostTypes");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "CostTypes");

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
        }
    }
}
