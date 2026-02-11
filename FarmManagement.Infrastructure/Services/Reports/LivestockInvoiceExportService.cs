using FarmManagement.Application.DTOs.Livestocks;
using FarmManagement.Application.Interfaces.Services;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace FarmManagement.Infrastructure.Services.Reports;

/// <summary>
/// Service xuất hóa đơn bán hàng PDF
/// </summary>
public class LivestockInvoiceExportService : ILivestockInvoiceExportService
{
    private const string FARM_NAME = "TRANG TRẠI CHĂN NUÔI ABC";
    private const string FARM_ADDRESS = "123 Đường ABC, Phường XYZ, Quận 1, TP.HCM";
    private const string FARM_PHONE = "0123 456 789";
    private const string FARM_EMAIL = "contact@farmmanagement.vn";
    private const string FARM_TAX_CODE = "0123456789";

    public byte[] ExportSaleInvoice(LivestockSaleDto sale, LivestockDto livestock)
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

                page.Header().Element(CreateInvoiceHeader);
                page.Content().Element(content => CreateInvoiceContent(content, sale, livestock));
                page.Footer().Element(CreateInvoiceFooter);
            });
        });

        return document.GeneratePdf();
    }

    private void CreateInvoiceHeader(IContainer container)
    {
        container.Column(column =>
        {
            column.Item().Row(row =>
            {
                row.RelativeItem().Column(col =>
                {
                    col.Item().Text(FARM_NAME).FontSize(16).Bold().FontColor(Colors.Blue.Darken2);
                    col.Item().PaddingTop(5).Text(FARM_ADDRESS).FontSize(9);
                    col.Item().PaddingTop(3).Row(r =>
                    {
                        r.AutoItem().Text("☎ ").FontSize(9);
                        r.AutoItem().Text(FARM_PHONE).FontSize(9).Bold();
                        r.AutoItem().PaddingLeft(10).Text("✉ ").FontSize(9);
                        r.AutoItem().Text(FARM_EMAIL).FontSize(9);
                    });
                    col.Item().PaddingTop(2).Text($"MST: {FARM_TAX_CODE}").FontSize(9);
                });

                row.ConstantItem(100).AlignRight().Border(1).BorderColor(Colors.Grey.Lighten2)
                    .Width(100).Height(80).AlignCenter().AlignMiddle().Text("LOGO").FontSize(10);
            });

            column.Item().PaddingTop(20).AlignCenter().Text("PHIẾU XUẤT BÁN HÀNG")
                .FontSize(20).Bold().FontColor(Colors.Red.Darken2);
            column.Item().AlignCenter().Text("(Hóa đơn bán vật nuôi)").FontSize(10).Italic();
            column.Item().PaddingTop(10).LineHorizontal(2).LineColor(Colors.Blue.Darken1);
        });
    }

    private void CreateInvoiceContent(IContainer container, LivestockSaleDto sale, LivestockDto livestock)
    {
        container.PaddingTop(15).Column(column =>
        {
            column.Item().Row(row =>
            {
                row.RelativeItem().Column(col =>
                {
                    col.Item().Row(r =>
                    {
                        r.AutoItem().Width(100).Text("Số hóa đơn:").Bold();
                        r.AutoItem().Text(sale.OrderCode ?? $"INV-{sale.Id:D6}").Bold().FontColor(Colors.Red.Medium);
                    });
                    col.Item().PaddingTop(3).Row(r =>
                    {
                        r.AutoItem().Width(100).Text("Ngày bán:").Bold();
                        r.AutoItem().Text(sale.SaleDate.ToString("dd/MM/yyyy"));
                    });
                });

                row.RelativeItem().AlignRight().Text($"Ngày xuất: {DateTime.Now:dd/MM/yyyy HH:mm}").FontSize(9);
            });

            column.Item().PaddingTop(10).LineHorizontal(1).LineColor(Colors.Grey.Lighten1);

            column.Item().PaddingTop(10).Column(col =>
            {
                col.Item().Text("THÔNG TIN KHÁCH HÀNG").FontSize(12).Bold().FontColor(Colors.Blue.Darken1);
                col.Item().PaddingTop(5).Row(r =>
                {
                    r.AutoItem().Width(120).Text("Tên khách hàng:");
                    r.AutoItem().Text(sale.Buyer ?? "Khách lẻ").Bold();
                });
                if (!string.IsNullOrEmpty(sale.BuyerPhone))
                {
                    col.Item().PaddingTop(3).Row(r =>
                    {
                        r.AutoItem().Width(120).Text("Số điện thoại:");
                        r.AutoItem().Text(sale.BuyerPhone);
                    });
                }
            });

            column.Item().PaddingTop(10).LineHorizontal(1).LineColor(Colors.Grey.Lighten1);

            column.Item().PaddingTop(10).Column(col =>
            {
                col.Item().Text("THÔNG TIN SẢN PHẨM").FontSize(12).Bold().FontColor(Colors.Blue.Darken1);
                col.Item().PaddingTop(5).Row(r =>
                {
                    r.AutoItem().Width(120).Text("Loại vật nuôi:");
                    r.AutoItem().Text(livestock.LivestockTypeName).Bold();
                });
                col.Item().PaddingTop(3).Row(r =>
                {
                    r.AutoItem().Width(120).Text("Tên đàn/Mã lô:");
                    r.AutoItem().Text(livestock.Name ?? livestock.TagCode ?? $"VN-{livestock.Id:D4}");
                });
            });

            column.Item().PaddingTop(15).Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(40);
                    columns.RelativeColumn(3);
                    columns.ConstantColumn(70);
                    columns.ConstantColumn(90);
                    columns.ConstantColumn(100);
                    columns.ConstantColumn(120);
                });

                table.Header(header =>
                {
                    header.Cell().Background(Colors.Blue.Darken2).Padding(8).AlignCenter().Text("STT").FontColor(Colors.White).Bold();
                    header.Cell().Background(Colors.Blue.Darken2).Padding(8).Text("Mô tả sản phẩm").FontColor(Colors.White).Bold();
                    header.Cell().Background(Colors.Blue.Darken2).Padding(8).AlignCenter().Text("Số con").FontColor(Colors.White).Bold();
                    header.Cell().Background(Colors.Blue.Darken2).Padding(8).AlignCenter().Text("Trọng lượng (kg)").FontColor(Colors.White).Bold();
                    header.Cell().Background(Colors.Blue.Darken2).Padding(8).AlignRight().Text("Đơn giá (đ/kg)").FontColor(Colors.White).Bold();
                    header.Cell().Background(Colors.Blue.Darken2).Padding(8).AlignRight().Text("Thành tiền (đ)").FontColor(Colors.White).Bold();
                });

                int stt = 1;
                foreach (var detail in sale.Details.OrderBy(d => d.LineNumber))
                {
                    var genderText = detail.Gender switch
                    {
                        FarmManagement.Domain.Entities.Livestocks.GenderType.Male => "Đực",
                        FarmManagement.Domain.Entities.Livestocks.GenderType.Female => "Cái",
                        _ => "Hỗn hợp"
                    };

                    var description = $"{livestock.LivestockTypeName} - {genderText}";
                    if (!string.IsNullOrEmpty(detail.Note))
                        description += $"\n({detail.Note})";

                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(8).AlignCenter().Text(stt.ToString());
                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(8).Text(description);
                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(8).AlignCenter().Text(detail.Quantity.ToString());
                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(8).AlignCenter().Text(detail.Weight.ToString("N2"));
                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(8).AlignRight().Text(detail.UnitPrice.ToString("N0"));
                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(8).AlignRight().Text(detail.Amount.ToString("N0")).Bold().FontColor(Colors.Green.Darken1);
                    stt++;
                }

                table.Footer(footer =>
                {
                    footer.Cell().ColumnSpan(2).Background(Colors.Grey.Lighten3).Padding(8).AlignRight().Text("TỔNG CỘNG:").Bold().FontSize(11);
                    footer.Cell().Background(Colors.Grey.Lighten3).Padding(8).AlignCenter().Text(sale.TotalQuantity.ToString()).Bold();
                    footer.Cell().Background(Colors.Grey.Lighten3).Padding(8).AlignCenter().Text(sale.TotalWeight.ToString("N2")).Bold();
                    footer.Cell().Background(Colors.Grey.Lighten3).Padding(8);
                    footer.Cell().Background(Colors.Grey.Lighten3).Padding(8).AlignRight().Text(sale.TotalAmount.ToString("N0")).Bold().FontSize(12).FontColor(Colors.Red.Darken1);
                });
            });

            column.Item().PaddingTop(10).Border(1).BorderColor(Colors.Grey.Lighten1).Padding(10).Row(row =>
            {
                row.AutoItem().Width(150).Text("Tổng tiền (bằng chữ):").Bold();
                row.AutoItem().Text(ConvertNumberToWords(sale.TotalAmount)).Italic().FontColor(Colors.Blue.Darken1);
            });

            if (!string.IsNullOrEmpty(sale.Note))
            {
                column.Item().PaddingTop(10).Column(col =>
                {
                    col.Item().Text("Ghi chú:").Bold().FontSize(10);
                    col.Item().PaddingTop(3).Text(sale.Note).FontSize(9);
                });
            }

            column.Item().PaddingTop(30).Row(row =>
            {
                row.RelativeItem().Column(col =>
                {
                    col.Item().AlignCenter().Text("Người mua hàng").Bold();
                    col.Item().AlignCenter().PaddingTop(3).Text("(Ký, ghi rõ họ tên)").FontSize(8).Italic();
                    col.Item().Height(60);
                });

                row.RelativeItem().Column(col =>
                {
                    col.Item().AlignCenter().Text("Người bán hàng").Bold();
                    col.Item().AlignCenter().PaddingTop(3).Text("(Ký, ghi rõ họ tên)").FontSize(8).Italic();
                    col.Item().Height(60);
                });

                row.RelativeItem().Column(col =>
                {
                    col.Item().AlignCenter().Text("Người lập phiếu").Bold();
                    col.Item().AlignCenter().PaddingTop(3).Text("(Ký, ghi rõ họ tên)").FontSize(8).Italic();
                    col.Item().Height(60);
                });
            });
        });
    }

    private void CreateInvoiceFooter(IContainer container)
    {
        container.AlignCenter().Column(column =>
        {
            column.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten2);
            column.Item().PaddingTop(5).Text("Cảm ơn quý khách đã sử dụng sản phẩm của chúng tôi!")
                .FontSize(10).Italic().FontColor(Colors.Green.Medium).Bold();
            column.Item().PaddingTop(5).Text(text =>
            {
                text.Span("Trang ");
                text.CurrentPageNumber();
                text.Span(" / ");
                text.TotalPages();
                text.DefaultTextStyle(x => x.FontSize(8).FontColor(Colors.Grey.Medium));
            });
        });
    }

    private string ConvertNumberToWords(decimal number)
    {
        if (number == 0) return "Không đồng";

        long intPart = (long)number;
        string result = "";

        if (intPart >= 1000000000)
        {
            result += ConvertThreeDigits((int)(intPart / 1000000000)) + " tỷ ";
            intPart %= 1000000000;
        }

        if (intPart >= 1000000)
        {
            result += ConvertThreeDigits((int)(intPart / 1000000)) + " triệu ";
            intPart %= 1000000;
        }

        if (intPart >= 1000)
        {
            result += ConvertThreeDigits((int)(intPart / 1000)) + " nghìn ";
            intPart %= 1000;
        }

        if (intPart > 0)
        {
            result += ConvertThreeDigits((int)intPart) + " ";
        }

        result += "đồng";

        if (!string.IsNullOrEmpty(result))
        {
            result = char.ToUpper(result[0]) + result.Substring(1);
        }

        return result.Trim();
    }

    private string ConvertThreeDigits(int number)
    {
        if (number == 0) return "";

        string[] units = { "", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
        string result = "";

        int hundreds = number / 100;
        int remainder = number % 100;
        int tens = remainder / 10;
        int ones = remainder % 10;

        if (hundreds > 0)
        {
            result = units[hundreds] + " trăm";
            if (remainder > 0 && remainder < 10)
                result += " lẻ";
        }

        if (tens > 1)
        {
            result += " " + units[tens] + " mươi";
        }
        else if (tens == 1)
        {
            result += " mười";
        }

        if (ones > 0)
        {
            if (tens > 1 && ones == 1)
                result += " mốt";
            else if (tens > 0 && ones == 5)
                result += " lăm";
            else
                result += " " + units[ones];
        }

        return result.Trim();
    }
}
