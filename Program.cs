// See https://aka.ms/new-console-template for more information
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;


QuestPDF.Settings.License = LicenseType.Community;



Console.WriteLine("Hello, World!");

InvoiceDTO invoice = GenrateInvoice();

InvoiceDTO GenrateInvoice()
{
    return new InvoiceDTO
    {
        Id = 1,
        InvoiceNumber = "123456",
        CompanyName = "Safa",
        Date = DateTime.Now,
        Total = 1000,
        SubTotal = 900,
        Details = "Details",
        CertificateNumber = "CertificateNumber",
        ParcelsNumber = "ParcelsNumber",
        Weight = 100,
        Covered = "Covered",
        SupplierInvoice = "SupplierInvoice",
        SupplierName = "SupplierName",
        SubsidiaryPolicy = "SubsidiaryPolicy",
        PurchaseOrder = "PurchaseOrder",
        Reservation = "Reservation",
        CompanyId = "CompanyId",
        InvoiceItems = new List<InvoiceItemDTO>
        {
            new InvoiceItemDTO
            {
                Id = 1,
                InvoiceId = 1,
                Description = "Description",
                Price = 100,
                Quantity = 1,
                Total = 100,
                SubTotal = 90,
                TaxName = "TaxName",
                TaxId = 1,
                ProductName = "ProductName",
                ProductId = 1
            },
            new InvoiceItemDTO
            {
                Id = 2,
                InvoiceId = 1,
                Description = "Description",
                Price = 100,
                Quantity = 1,
                Total = 100,
                SubTotal = 90,
                TaxName = "TaxName",
                TaxId = 1,
                ProductName = "ProductName",
                ProductId = 1
            }
        }

    };
}

var document = new InvoiceDocument(invoice,"");
document.GeneratePdf($"{Guid.NewGuid()}.pdf");

