
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;



public class InvoiceDocument : IDocument
{
    private readonly InvoiceDTO _invoice;
    private readonly string? _imagePath;
    public InvoiceDocument(InvoiceDTO invoice, string? imagePath)
    {
        _invoice = invoice;
        _imagePath = imagePath;
    }

    public DocumentMetadata GetMetadata() => new DocumentMetadata
    {
        Title = "SafaInvoice",
        Subject = "SafaInvoice",
        Author = "MM",
        Creator = "MM",
        Keywords = "Invoice",
    };

    public DocumentSettings GetSettings() => new DocumentSettings
    {
        ContentDirection = ContentDirection.RightToLeft,
    };

    public void Compose(IDocumentContainer container)
    {
        container
           .Page(page =>
           {
               page.Margin(50);
               page.Size(PageSizes.A4);
               page.DefaultTextStyle(x => x.FontFamily("Calibri").FontSize(14));
               page.ContentFromRightToLeft();


               page.Header().Element(ComposeHeader);
               page.Content().Element(ComposeContent);


               page.Footer().AlignCenter().Text(x =>
               {
                   x.CurrentPageNumber();
                   x.Span(" / ");
                   x.TotalPages();
               });
           });
    }

    void ComposeHeader(IContainer container)
    {
        var titleStyle = TextStyle.Default.FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);
        container.Column(col =>
        {
            col.Spacing(5);
            col.Item().PaddingBottom(10).Background(Colors.BlueGrey.Lighten4).PaddingVertical(10).AlignMiddle().AlignCenter().Text("استرداد مبالغ تم سدادها نيابة عن الشركة").FontSize(20).ExtraBold();
            col.Item().Row(row =>
            {
                row.RelativeItem().PaddingTop(20).Column(column =>
                {
                    column.Item().Text($"فتورة رقم {_invoice.InvoiceNumber}").Style(titleStyle);

                    column.Item().Text(text =>
                    {
                        text.Element().Text("تاريخ ").SemiBold();
                        text.Element().ArabicDate(_invoice.Date).SemiBold();
                    });
                });
                //if (string.IsNullOrEmpty(_imagePath))
                //{
                //    row.ConstantItem(100).Image(_imagePath).WithCompressionQuality(ImageCompressionQuality.High);
                //}
            });
        });

    }

    void ComposeContent(IContainer container)
    {
        container.PaddingVertical(20).Column(column =>
        {
            column.Spacing(5);
            column.Item().PaddingBottom(10).Text(e =>
            {
                e.Span("المطلوب من السادة / ").FontSize(18).Bold();
                e.Span(_invoice.CompanyName).FontSize(18).Bold();
            });
            //column.Item().AlignMiddle().AlignCenter().Background(Colors.Grey.Lighten3).Padding(10).Text($"شهادة رقم : {123514}").Bold().FontSize(16);
            column.Item().Row(row =>
            {
                row.RelativeItem().Column(col =>
                {
                    col.Spacing(5);

                    col.Item().Text(span =>
                    {
                        span.Element().Text("بوليصة كلية : ").FontSize(16).SemiBold();
                        span.Element().Text(_invoice.SubsidiaryPolicy).FontSize(16).Bold();
                    });
                    col.Item().Text(span =>
                    {
                        span.Element().Text("عدد الطرود : ").FontSize(16).Bold();
                        span.Element().Text(_invoice.ParcelsNumber).FontSize(16).Bold();
                    });
                    col.Item().Text(span =>
                    {
                        span.Element().Text("المشمول : ").FontSize(16).Bold();
                        span.Element().Text(_invoice.Covered).FontSize(16).Bold();
                    });

                });
                //row.RelativeItem().Component(new AddressComponent("العنوان", adders));
                row.ConstantItem(50);
                row.RelativeItem().Column(col =>
                {
                    col.Spacing(5);

                    col.Item().Text(span =>
                    {
                        span.Element().Text("فاتورة رقم : ").FontSize(16).SemiBold();
                        span.Element().Text(_invoice.SupplierInvoice).FontSize(16).SemiBold();
                    });
                    col.Item().Text(span =>
                    {
                        span.Element().Text("الوزن : ").FontSize(16).SemiBold();
                        span.Element().Text(_invoice.Weight).FontSize(16).SemiBold();
                    });
                });
                //row.RelativeItem().Component(new AddressComponent("العنوان", adders));
            });

            column.Item().Element(ComposeTable);

            column.Item().AlignLeft().Text(span =>
            {
                span.Element().Text("المجموع : ").FontSize(14);
                span.Element().Text(_invoice.Total).FontSize(14);
            });

            column.Item().PaddingTop(25).Element(ComposeComments);
        });

    }

    void ComposeComments(IContainer container)
    {
        container.Background(Colors.Grey.Lighten3).Padding(10).Column(column =>
        {
            column.Spacing(5);
            column.Item().Text("المرفقات").FontSize(16);
            column.Item().Text(_invoice.Details);
        });
    }

    void ComposeTable(IContainer container)
    {
        container.Table(table =>
        {
            table.ColumnsDefinition(columns =>
            {
                columns.ConstantColumn(25);
                columns.RelativeColumn(3);
                columns.RelativeColumn();
                columns.RelativeColumn();
                columns.RelativeColumn();
            });

            table.Header(header =>
            {
                header.Cell().Element(CellStyle).Text("#").Bold();
                header.Cell().Element(CellStyle).Text("البيان").Bold();
                header.Cell().Element(CellStyle).AlignLeft().Text("سعر الوحدة").Bold();
                header.Cell().Element(CellStyle).AlignLeft().Text("الكمية").Bold();
                header.Cell().Element(CellStyle).AlignLeft().Text("المجموع").Bold();

                static IContainer CellStyle(IContainer container)
                {
                    return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                }
            });

            foreach (var item in _invoice.InvoiceItems)
            {

                table.Cell().Element(CellStyle).ArabicNumerals(_invoice.InvoiceItems.IndexOf(item) + 1, true);
                table.Cell().Element(CellStyle).Text(item.ProductName);
                table.Cell().Element(CellStyle).AlignLeft().Text($"{item.Price}$");
                table.Cell().Element(CellStyle).AlignLeft().Text(item.Quantity);
                table.Cell().Element(CellStyle).AlignLeft().Text($"{item.Price * item.Quantity}$");

                if (item.TaxId is not null)
                {
                    table.Cell().Element(CellStyle).Text("");
                    table.Cell().Element(CellStyle).Text(item.TaxName);
                    table.Cell().Element(CellStyle).AlignLeft().Text($"");
                    table.Cell().Element(CellStyle).AlignLeft().Text("");
                    table.Cell().Element(CellStyle).AlignLeft().Text($"{item.Total - item.SubTotal}$");
                }

                static IContainer CellStyle(IContainer container)
                {
                    return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                }
            }
        });
    }
}
