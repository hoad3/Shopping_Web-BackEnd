using System.ComponentModel.DataAnnotations.Schema;

namespace Web_2.Models.VnPaymentRequest;

public class Invoice
{
    public int InvoiceId { get; set; }
    public int MemberId { get; set; }
    public DateTime InvoiceDate { get; set; }
    public string GivenName { get; set; } = null!;
    public string? Surname { get; set; }
    public int Phone { get; set; }
    public string Address { get; set; } = null!;
    [NotMapped]
    public decimal Amount { get; set; }
    public List<InvoiceDetail>? InvoiceDetails { get; set; }
}