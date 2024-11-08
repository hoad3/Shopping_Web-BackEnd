using System.ComponentModel.DataAnnotations.Schema;

namespace Web_2.Models.VnPaymentRequest;


[Table("InvoiceDetail")]
public class InvoiceDetail
{
    public int InvoiceId { get; set; }
    public int ProductId { get; set; }
    public decimal Price { get; set; }
    public short Quantity { get; set; }
}