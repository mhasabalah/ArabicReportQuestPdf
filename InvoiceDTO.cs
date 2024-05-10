public class InvoiceDTO
{
    public int Id { get; set; }
    public string? InvoiceNumber { get; set; }
    public string? CompanyName { get; set; }
    public DateTime Date { get; set; }
    public double Total { get; set; }
    public double SubTotal { get; set; }
    public string? Details { get; set; }

    public string? CertificateNumber { get; set; }
    public string? ParcelsNumber { get; set; }
    public float Weight { get; set; }
    public string? Covered { get; set; }

    public string? SupplierInvoice { get; set; }
    public string? SupplierName { get; set; }
    public string? SubsidiaryPolicy { get; set; }
    public string? PurchaseOrder { get; set; }
    public string? Reservation { get; set; }

    public string? CompanyId { get; set; }
    public List<InvoiceItemDTO> InvoiceItems { get; set; } = new List<InvoiceItemDTO>();
}
