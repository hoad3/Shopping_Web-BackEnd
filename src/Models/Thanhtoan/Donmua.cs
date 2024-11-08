using System.ComponentModel.DataAnnotations.Schema;

namespace Web_2.Models.Thanhtoan;

public class Donmua
{
    public int Iddonmua { get; set; }
    public int idnguoiban { get; set; }
    public int idnguoimua { get; set; }
    [ForeignKey("Product")]
    public int idproduct { get; set; }
    public DateTime ngaydat { get; set; }
    public int dongia { get; set; }
    public int soluong { get; set; }
    public string name { get; set; }
    public int tongtien { get; set; }
    public string nguoimua { get; set; }
    public string nguoiban { get; set; }
    public int phuongthucthanhtoan { get; set; }
    public int trangthaithanhtoan  { get; set; }
    // Constructor
    public Product.Product Product { get; set; }
}