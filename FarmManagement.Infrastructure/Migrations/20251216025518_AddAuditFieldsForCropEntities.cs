using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditFieldsForCropEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "CropTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "CropTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CropTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "CropTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "CropStatuses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "CropStatuses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CropStatuses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "CropStatuses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Crops",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Crops",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Crops",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Crops",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "CropHarvests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "CropHarvests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CropHarvests",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "CropHarvests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "CropCosts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "CropCosts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CropCosts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "CropCosts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "CropCareLogs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "CropCareLogs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CropCareLogs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "CropCareLogs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "CostTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "CostTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CostTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "CostTypes",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "CropTypes");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "CropTypes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CropTypes");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "CropTypes");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "CropStatuses");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "CropStatuses");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CropStatuses");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "CropStatuses");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Crops");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Crops");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Crops");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Crops");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "CropHarvests");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "CropHarvests");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CropHarvests");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "CropHarvests");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "CropCosts");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "CropCosts");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CropCosts");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "CropCosts");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "CropCareLogs");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "CropCareLogs");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CropCareLogs");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "CropCareLogs");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "CostTypes");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "CostTypes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CostTypes");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "CostTypes");
        }
    }
}
