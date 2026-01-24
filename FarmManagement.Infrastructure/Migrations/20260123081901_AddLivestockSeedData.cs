using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FarmManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLivestockSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LivestockCareLogs_LivestockCareTypes_LivestockCareTypeId",
                table: "LivestockCareLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_LivestockHealthLogs_LivestockHealthStatuses_HealthStatusId",
                table: "LivestockHealthLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Livestocks_LivestockStatuses_LivestockStatusId",
                table: "Livestocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Livestocks_LivestockTypes_LivestockTypeId",
                table: "Livestocks");

            migrationBuilder.DropForeignKey(
                name: "FK_LivestockSales_SaleTypes_SaleTypeId",
                table: "LivestockSales");

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "Livestocks",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "LivestockHealthStatuses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "LivestockHealthStatuses",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "VetName",
                table: "LivestockHealthLogs",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Treatment",
                table: "LivestockHealthLogs",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Symptom",
                table: "LivestockHealthLogs",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "LivestockCareTypes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "LivestockCareTypes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Unit",
                table: "LivestockCareLogs",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "LivestockCareLogs",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

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

            migrationBuilder.InsertData(
                table: "LivestockCareTypes",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "FEED", "Cho ăn" },
                    { 2, "VACCINE", "Tiêm vaccine" },
                    { 3, "MEDICINE", "Cho uống thuốc" },
                    { 4, "CLEAN", "Vệ sinh" },
                    { 5, "WEIGH", "Cân trọng lượng" },
                    { 6, "CHECKUP", "Khám sức khỏe" },
                    { 7, "DEWORMING", "Tẩy giun" },
                    { 8, "MOVE", "Di chuyển" }
                });

            migrationBuilder.InsertData(
                table: "LivestockHealthStatuses",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "HEALTHY", "Khỏe mạnh" },
                    { 2, "SICK", "Ốm/Bệnh" },
                    { 3, "RECOVERING", "Đang hồi phục" },
                    { 4, "WEAK", "Yếu" },
                    { 5, "PREGNANT", "Mang thai" },
                    { 6, "NURSING", "Đang cho con bú" },
                    { 7, "INJURED", "Bị thương" }
                });

            migrationBuilder.InsertData(
                table: "LivestockStatuses",
                columns: new[] { "Id", "Code", "CreatedDate", "DeletedDate", "IsDeleted", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "RAISING", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Đang nuôi", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, "SOLD", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Đã bán", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, "BREEDING", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Đang sinh sản", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, "SICK", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Đang bệnh", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, "QUARANTINE", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Cách ly", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 6, "DEAD", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Đã chết", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 7, "SLAUGHTERED", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Đã giết mổ", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "LivestockTypes",
                columns: new[] { "Id", "Code", "CreatedDate", "DeletedDate", "Description", "IsDeleted", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "PIG", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Heo thịt, heo nái, heo giống", false, "Heo", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, "COW", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Bò thịt, bò sữa, bò giống", false, "Bò", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, "CHICKEN", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Gà thịt, gà đẻ, gà giống", false, "Gà", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, "DUCK", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Vịt thịt, vịt đẻ", false, "Vịt", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, "GOAT", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Dê thịt, dê sữa", false, "Dê", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 6, "SHEEP", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Cừu lấy lông, cừu thịt", false, "Cừu", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 7, "FISH", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Cá nuôi ao, hồ, bè", false, "Cá", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

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

            migrationBuilder.InsertData(
                table: "SaleTypes",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "RETAIL", "Bán lẻ" },
                    { 2, "WHOLESALE", "Bán sỉ" },
                    { 3, "CONTRACT", "Hợp đồng" },
                    { 4, "EXPORT", "Xuất khẩu" },
                    { 5, "SLAUGHTER", "Lò mổ" },
                    { 6, "BREEDING", "Giống" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_LivestockCareLogs_LivestockCareTypes_LivestockCareTypeId",
                table: "LivestockCareLogs",
                column: "LivestockCareTypeId",
                principalTable: "LivestockCareTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LivestockHealthLogs_LivestockHealthStatuses_HealthStatusId",
                table: "LivestockHealthLogs",
                column: "HealthStatusId",
                principalTable: "LivestockHealthStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Livestocks_LivestockStatuses_LivestockStatusId",
                table: "Livestocks",
                column: "LivestockStatusId",
                principalTable: "LivestockStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Livestocks_LivestockTypes_LivestockTypeId",
                table: "Livestocks",
                column: "LivestockTypeId",
                principalTable: "LivestockTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LivestockSales_SaleTypes_SaleTypeId",
                table: "LivestockSales",
                column: "SaleTypeId",
                principalTable: "SaleTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LivestockCareLogs_LivestockCareTypes_LivestockCareTypeId",
                table: "LivestockCareLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_LivestockHealthLogs_LivestockHealthStatuses_HealthStatusId",
                table: "LivestockHealthLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Livestocks_LivestockStatuses_LivestockStatusId",
                table: "Livestocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Livestocks_LivestockTypes_LivestockTypeId",
                table: "Livestocks");

            migrationBuilder.DropForeignKey(
                name: "FK_LivestockSales_SaleTypes_SaleTypeId",
                table: "LivestockSales");

            migrationBuilder.DeleteData(
                table: "LivestockCareTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "LivestockCareTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "LivestockCareTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "LivestockCareTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "LivestockCareTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "LivestockCareTypes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "LivestockCareTypes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "LivestockCareTypes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "LivestockHealthStatuses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "LivestockHealthStatuses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "LivestockHealthStatuses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "LivestockHealthStatuses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "LivestockHealthStatuses",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "LivestockHealthStatuses",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "LivestockHealthStatuses",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "LivestockStatuses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "LivestockStatuses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "LivestockStatuses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "LivestockStatuses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "LivestockStatuses",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "LivestockStatuses",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "LivestockStatuses",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "LivestockTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "LivestockTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "LivestockTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "LivestockTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "LivestockTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "LivestockTypes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "LivestockTypes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "SaleTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SaleTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SaleTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SaleTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "SaleTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "SaleTypes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "Livestocks",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "LivestockHealthStatuses",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "LivestockHealthStatuses",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "VetName",
                table: "LivestockHealthLogs",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Treatment",
                table: "LivestockHealthLogs",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Symptom",
                table: "LivestockHealthLogs",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "LivestockCareTypes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "LivestockCareTypes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Unit",
                table: "LivestockCareLogs",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "LivestockCareLogs",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_LivestockCareLogs_LivestockCareTypes_LivestockCareTypeId",
                table: "LivestockCareLogs",
                column: "LivestockCareTypeId",
                principalTable: "LivestockCareTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LivestockHealthLogs_LivestockHealthStatuses_HealthStatusId",
                table: "LivestockHealthLogs",
                column: "HealthStatusId",
                principalTable: "LivestockHealthStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Livestocks_LivestockStatuses_LivestockStatusId",
                table: "Livestocks",
                column: "LivestockStatusId",
                principalTable: "LivestockStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Livestocks_LivestockTypes_LivestockTypeId",
                table: "Livestocks",
                column: "LivestockTypeId",
                principalTable: "LivestockTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LivestockSales_SaleTypes_SaleTypeId",
                table: "LivestockSales",
                column: "SaleTypeId",
                principalTable: "SaleTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
