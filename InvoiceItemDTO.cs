public class InvoiceItemDTO
{
    public int Id { get; set; }
    public int InvoiceId { get; set; }
    public string? Description { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; } = 1;
    public double Total { get; set; }
    public double SubTotal { get; set; }
    public string? TaxName { get; set; }
    public int? TaxId { get; set; }
    public string? ProductName { get; set; }
    public int? ProductId { get; set; }
}
