using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddActivityLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActivityLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EntityType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EntityId = table.Column<int>(type: "int", nullable: false),
                    EntityName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PerformedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityLogs", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "CropStatuses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 3, 56, 1, 436, DateTimeKind.Utc).AddTicks(5027), new DateTime(2026, 1, 23, 3, 56, 1, 436, DateTimeKind.Utc).AddTicks(5032) });

            migrationBuilder.UpdateData(
                table: "CropStatuses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 3, 56, 1, 436, DateTimeKind.Utc).AddTicks(5040), new DateTime(2026, 1, 23, 3, 56, 1, 436, DateTimeKind.Utc).AddTicks(5041) });

            migrationBuilder.UpdateData(
                table: "CropStatuses",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 3, 56, 1, 436, DateTimeKind.Utc).AddTicks(5043), new DateTime(2026, 1, 23, 3, 56, 1, 436, DateTimeKind.Utc).AddTicks(5043) });

            migrationBuilder.UpdateData(
                table: "LocationStatuses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 3, 56, 1, 439, DateTimeKind.Utc).AddTicks(3016), new DateTime(2026, 1, 23, 3, 56, 1, 439, DateTimeKind.Utc).AddTicks(3019) });

            migrationBuilder.UpdateData(
                table: "LocationStatuses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 3, 56, 1, 439, DateTimeKind.Utc).AddTicks(3023), new DateTime(2026, 1, 23, 3, 56, 1, 439, DateTimeKind.Utc).AddTicks(3023) });

            migrationBuilder.UpdateData(
                table: "LocationTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 3, 56, 1, 439, DateTimeKind.Utc).AddTicks(4701), new DateTime(2026, 1, 23, 3, 56, 1, 439, DateTimeKind.Utc).AddTicks(4704) });

            migrationBuilder.UpdateData(
                table: "LocationTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 3, 56, 1, 439, DateTimeKind.Utc).AddTicks(4707), new DateTime(2026, 1, 23, 3, 56, 1, 439, DateTimeKind.Utc).AddTicks(4708) });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLogs_ActionDate",
                table: "ActivityLogs",
                column: "ActionDate");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLogs_EntityType_EntityId",
                table: "ActivityLogs",
                columns: new[] { "EntityType", "EntityId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityLogs");

            migrationBuilder.UpdateData(
                table: "CropStatuses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 3, 34, 37, 835, DateTimeKind.Utc).AddTicks(4638), new DateTime(2026, 1, 23, 3, 34, 37, 835, DateTimeKind.Utc).AddTicks(4642) });

            migrationBuilder.UpdateData(
                table: "CropStatuses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 3, 34, 37, 835, DateTimeKind.Utc).AddTicks(4648), new DateTime(2026, 1, 23, 3, 34, 37, 835, DateTimeKind.Utc).AddTicks(4648) });

            migrationBuilder.UpdateData(
                table: "CropStatuses",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 23, 3, 34, 37, 835, DateTimeKind.Utc).AddTicks(4650), new DateTime(2026, 1, 23, 3, 34, 37, 835, DateTimeKind.Utc).AddTicks(4650) });

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
    }
}
