using FarmManagement.Application.DTOs.Livestocks;
using FarmManagement.Application.Interfaces.Services;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace FarmManagement.Infrastructure.Services.Reports;

/// <summary>
/// Service xuất báo cáo PDF tổng quan về vật nuôi
/// </summary>
public class LivestockPdfExportService : ILivestockPdfExportService
{
    public byte[] ExportLivestockDetails(
        LivestockDto livestock,
        IEnumerable<LivestockCareLogDto> careLogs,
        IEnumerable<LivestockHealthLogDto> healthLogs,
        IEnumerable<LivestockSaleDto> sales)
    {
        QuestPDF.Settings.License = LicenseType.Community;

        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(11).FontFamily("Arial"));

                page.Header()
                    .Height(100)
                    .Background(Colors.Blue.Lighten3)
                    .Padding(20)
                    .Column(column =>
                    {
                        column.Item().Text("BÁO CÁO CHI TIẾT VẬT NUÔI")
                            .FontSize(20)
                            .Bold()
                            .FontColor(Colors.Blue.Darken2);

                        column.Item().Text($"Ngày xuất: {DateTime.Now:dd/MM/yyyy HH:mm}")
                            .FontSize(10)
                            .FontColor(Colors.Grey.Darken1);
                    });

                page.Content()
                    .PaddingVertical(1, Unit.Centimetre)
                    .Column(column =>
                    {
                        column.Item().Element(container => CreateOverviewSection(container, livestock));
                        column.Item().PaddingTop(15);

                        column.Item().Element(container => CreateStatisticsSection(container, livestock, careLogs, healthLogs, sales));
                        column.Item().PaddingTop(15);

                        if (careLogs?.Any() == true)
                        {
                            column.Item().Element(container => CreateCareLogSection(container, careLogs));
                            column.Item().PaddingTop(15);
                        }

                        if (healthLogs?.Any() == true)
                        {
                            column.Item().Element(container => CreateHealthLogSection(container, healthLogs));
                            column.Item().PaddingTop(15);
                        }

                        if (sales?.Any() == true)
                        {
                            column.Item().Element(container => CreateSalesSection(container, sales));
                        }
                    });

                page.Footer()
                    .AlignCenter()
                    .Text(text =>
                    {
                        text.Span("Trang ");
                        text.CurrentPageNumber();
                        text.Span(" / ");
                        text.TotalPages();
                        text.DefaultTextStyle(x => x.FontSize(9).FontColor(Colors.Grey.Medium));
                    });
            });
        });

        return document.GeneratePdf();
    }

    private void CreateOverviewSection(IContainer container, LivestockDto livestock)
    {
        container.Column(column =>
        {
            column.Item().Text("THÔNG TIN TỔNG QUAN")
                .FontSize(14)
                .Bold()
                .FontColor(Colors.Blue.Darken1);

            column.Item().PaddingTop(10);

            column.Item().Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(150);
                    columns.RelativeColumn();
                });

                AddInfoRow(table, "Mã định danh:", livestock.Id.ToString());
                if (!string.IsNullOrEmpty(livestock.Name))
                    AddInfoRow(table, "Tên/Mô tả:", livestock.Name);
                if (!string.IsNullOrEmpty(livestock.TagCode))
                    AddInfoRow(table, "Mã thẻ:", livestock.TagCode);
                AddInfoRow(table, "Loại vật nuôi:", livestock.LivestockTypeName);
                AddInfoRow(table, "Trạng thái:", livestock.LivestockStatusName);
                AddInfoRow(table, "Vị trí nuôi:", livestock.LocationName);
                AddInfoRow(table, "Ngày nhập:", livestock.ImportDate.ToString("dd/MM/yyyy"));
                AddInfoRow(table, "Số lượng:", $"{livestock.Quantity} con (♂ {livestock.MaleCount} | ♀ {livestock.FemaleCount})");
                AddInfoRow(table, "Trọng lượng:", $"{livestock.TotalImportWeight:N1} kg (TB: {livestock.ImportWeight:N1} kg/con)");
                AddInfoRow(table, "Giá trị nhập:", $"{livestock.TotalImportPrice:N0} VNĐ (TB: {livestock.ImportPrice:N0} VNĐ/con)");
                if (!string.IsNullOrEmpty(livestock.Note))
                    AddInfoRow(table, "Ghi chú:", livestock.Note);
            });
        });
    }

    private void CreateStatisticsSection(
        IContainer container,
        LivestockDto livestock,
        IEnumerable<LivestockCareLogDto> careLogs,
        IEnumerable<LivestockHealthLogDto> healthLogs,
        IEnumerable<LivestockSaleDto> sales)
    {
        var totalCareCost = careLogs?.Sum(c => c.Cost) ?? 0;
        var totalHealthCost = healthLogs?.Sum(h => h.MedicineCost) ?? 0;
        var totalSales = sales?.Sum(s => s.TotalAmount) ?? 0;

        container.Column(column =>
        {
            column.Item().Text("THỐNG KÊ")
                .FontSize(14)
                .Bold()
                .FontColor(Colors.Blue.Darken1);

            column.Item().PaddingTop(10);

            column.Item().Row(row =>
            {
                row.RelativeItem().Border(1).BorderColor(Colors.Grey.Lighten2).Padding(10).Column(col =>
                {
                    col.Item().Text("Chăm sóc").FontSize(10).FontColor(Colors.Grey.Darken1);
                    col.Item().Text($"{careLogs?.Count() ?? 0} lần").FontSize(16).Bold().FontColor(Colors.Green.Medium);
                    col.Item().Text($"Chi phí: {totalCareCost:N0} ₫").FontSize(9).FontColor(Colors.Grey.Medium);
                });

                row.Spacing(10);

                row.RelativeItem().Border(1).BorderColor(Colors.Grey.Lighten2).Padding(10).Column(col =>
                {
                    col.Item().Text("Sức khỏe").FontSize(10).FontColor(Colors.Grey.Darken1);
                    col.Item().Text($"{healthLogs?.Count() ?? 0} lần").FontSize(16).Bold().FontColor(Colors.Orange.Medium);
                    col.Item().Text($"Chi phí: {totalHealthCost:N0} ₫").FontSize(9).FontColor(Colors.Grey.Medium);
                });

                row.Spacing(10);

                row.RelativeItem().Border(1).BorderColor(Colors.Grey.Lighten2).Padding(10).Column(col =>
                {
                    col.Item().Text("Bán hàng").FontSize(10).FontColor(Colors.Grey.Darken1);
                    col.Item().Text($"{sales?.Count() ?? 0} đơn").FontSize(16).Bold().FontColor(Colors.Blue.Medium);
                    col.Item().Text($"Doanh thu: {totalSales:N0} ₫").FontSize(9).FontColor(Colors.Grey.Medium);
                });
            });
        });
    }

    private void CreateCareLogSection(IContainer container, IEnumerable<LivestockCareLogDto> careLogs)
    {
        container.Column(column =>
        {
            column.Item().Text("NHẬT KÝ CHĂM SÓC")
                .FontSize(14)
                .Bold()
                .FontColor(Colors.Blue.Darken1);

            column.Item().PaddingTop(10);

            column.Item().Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(80);
                    columns.RelativeColumn(2);
                    columns.ConstantColumn(80);
                    columns.ConstantColumn(90);
                    columns.RelativeColumn(3);
                });

                table.Header(header =>
                {
                    header.Cell().Background(Colors.Blue.Lighten3).Padding(5).Text("Ngày").Bold();
                    header.Cell().Background(Colors.Blue.Lighten3).Padding(5).Text("Loại chăm sóc").Bold();
                    header.Cell().Background(Colors.Blue.Lighten3).Padding(5).Text("Số lượng").Bold();
                    header.Cell().Background(Colors.Blue.Lighten3).Padding(5).Text("Chi phí (₫)").Bold();
                    header.Cell().Background(Colors.Blue.Lighten3).Padding(5).Text("Ghi chú").Bold();
                });

                foreach (var log in careLogs.OrderByDescending(x => x.CareDate).Take(20))
                {
                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5)
                        .Text(log.CareDate.ToString("dd/MM/yyyy")).FontSize(9);
                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5)
                        .Text(log.LivestockCareTypeName).FontSize(9);
                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5)
                        .Text($"{log.Quantity} {log.Unit}").FontSize(9);
                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5)
                        .Text($"{log.Cost:N0}").FontSize(9).FontColor(Colors.Green.Medium);
                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5)
                        .Text(log.Note ?? "—").FontSize(9).FontColor(Colors.Grey.Medium);
                }

                table.Footer(footer =>
                {
                    footer.Cell().ColumnSpan(3).Background(Colors.Grey.Lighten3).Padding(5)
                        .AlignRight().Text("Tổng chi phí:").Bold();
                    footer.Cell().Background(Colors.Grey.Lighten3).Padding(5)
                        .Text($"{careLogs.Sum(c => c.Cost):N0}").Bold().FontColor(Colors.Green.Medium);
                    footer.Cell().Background(Colors.Grey.Lighten3);
                });
            });

            if (careLogs.Count() > 20)
            {
                column.Item().PaddingTop(5).Text($"(Chỉ hiển thị 20/{careLogs.Count()} bản ghi gần nhất)")
                    .FontSize(8).Italic().FontColor(Colors.Grey.Medium);
            }
        });
    }

    private void CreateHealthLogSection(IContainer container, IEnumerable<LivestockHealthLogDto> healthLogs)
    {
        container.Column(column =>
        {
            column.Item().Text("NHẬT KÝ SỨC KHỎE")
                .FontSize(14)
                .Bold()
                .FontColor(Colors.Blue.Darken1);

            column.Item().PaddingTop(10);

            column.Item().Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(80);
                    columns.RelativeColumn(2);
                    columns.RelativeColumn(3);
                    columns.RelativeColumn(3);
                    columns.ConstantColumn(90);
                });

                table.Header(header =>
                {
                    header.Cell().Background(Colors.Orange.Lighten3).Padding(5).Text("Ngày").Bold();
                    header.Cell().Background(Colors.Orange.Lighten3).Padding(5).Text("Tình trạng").Bold();
                    header.Cell().Background(Colors.Orange.Lighten3).Padding(5).Text("Triệu chứng").Bold();
                    header.Cell().Background(Colors.Orange.Lighten3).Padding(5).Text("Điều trị").Bold();
                    header.Cell().Background(Colors.Orange.Lighten3).Padding(5).Text("Chi phí (₫)").Bold();
                });

                foreach (var log in healthLogs.OrderByDescending(x => x.CheckDate).Take(20))
                {
                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5)
                        .Text(log.CheckDate.ToString("dd/MM/yyyy")).FontSize(9);
                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5)
                        .Text(log.HealthStatusName).FontSize(9);
                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5)
                        .Text(log.Symptom ?? "—").FontSize(9);
                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5)
                        .Text(log.Treatment ?? "—").FontSize(9);
                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5)
                        .Text($"{log.MedicineCost:N0}").FontSize(9).FontColor(Colors.Orange.Medium);
                }

                table.Footer(footer =>
                {
                    footer.Cell().ColumnSpan(4).Background(Colors.Grey.Lighten3).Padding(5)
                        .AlignRight().Text("Tổng chi phí thuốc:").Bold();
                    footer.Cell().Background(Colors.Grey.Lighten3).Padding(5)
                        .Text($"{healthLogs.Sum(h => h.MedicineCost):N0}").Bold().FontColor(Colors.Orange.Medium);
                });
            });

            if (healthLogs.Count() > 20)
            {
                column.Item().PaddingTop(5).Text($"(Chỉ hiển thị 20/{healthLogs.Count()} bản ghi gần nhất)")
                    .FontSize(8).Italic().FontColor(Colors.Grey.Medium);
            }
        });
    }

    private void CreateSalesSection(IContainer container, IEnumerable<LivestockSaleDto> sales)
    {
        container.Column(column =>
        {
            column.Item().Text("LỊCH SỬ BÁN HÀNG")
                .FontSize(14)
                .Bold()
                .FontColor(Colors.Blue.Darken1);

            column.Item().PaddingTop(10);

            column.Item().Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(80);
                    columns.ConstantColumn(90);
                    columns.ConstantColumn(70);
                    columns.ConstantColumn(80);
                    columns.ConstantColumn(100);
                    columns.RelativeColumn();
                });

                table.Header(header =>
                {
                    header.Cell().Background(Colors.Green.Lighten3).Padding(5).Text("Ngày bán").Bold();
                    header.Cell().Background(Colors.Green.Lighten3).Padding(5).Text("Mã đơn").Bold();
                    header.Cell().Background(Colors.Green.Lighten3).Padding(5).Text("Số con").Bold();
                    header.Cell().Background(Colors.Green.Lighten3).Padding(5).Text("Trọng lượng").Bold();
                    header.Cell().Background(Colors.Green.Lighten3).Padding(5).Text("Tổng tiền (₫)").Bold();
                    header.Cell().Background(Colors.Green.Lighten3).Padding(5).Text("Người mua").Bold();
                });

                foreach (var sale in sales.OrderByDescending(x => x.SaleDate).Take(20))
                {
                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5)
                        .Text(sale.SaleDate.ToString("dd/MM/yyyy")).FontSize(9);
                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5)
                        .Text(sale.OrderCode).FontSize(9);
                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5)
                        .Text($"{sale.TotalQuantity}").FontSize(9);
                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5)
                        .Text($"{sale.TotalWeight:N2} kg").FontSize(9);
                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5)
                        .Text($"{sale.TotalAmount:N0}").FontSize(9).FontColor(Colors.Green.Medium);
                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5)
                        .Text(sale.Buyer ?? "—").FontSize(9);
                }

                table.Footer(footer =>
                {
                    footer.Cell().ColumnSpan(4).Background(Colors.Grey.Lighten3).Padding(5)
                        .AlignRight().Text("Tổng doanh thu:").Bold();
                    footer.Cell().Background(Colors.Grey.Lighten3).Padding(5)
                        .Text($"{sales.Sum(s => s.TotalAmount):N0}").Bold().FontColor(Colors.Green.Medium);
                    footer.Cell().Background(Colors.Grey.Lighten3);
                });
            });

            if (sales.Count() > 20)
            {
                column.Item().PaddingTop(5).Text($"(Chỉ hiển thị 20/{sales.Count()} đơn hàng gần nhất)")
                    .FontSize(8).Italic().FontColor(Colors.Grey.Medium);
            }
        });
    }

    private void AddInfoRow(TableDescriptor table, string label, string value)
    {
        table.Cell()
            .BorderBottom(1)
            .BorderColor(Colors.Grey.Lighten2)
            .Padding(5)
            .Text(label)
            .Bold()
            .FontSize(10);

        table.Cell()
            .BorderBottom(1)
            .BorderColor(Colors.Grey.Lighten2)
            .Padding(5)
            .Text(value)
            .FontSize(10);
    }
}
