namespace Web_2.Models.Thanhtoan;

public class ThanhtoanDto
{
    public int Idnguoimua { get; set; }
    public int Idnguoiban { get; set; }
    public int ProductId { get; set; }
    public int Soluong { get; set; }
    public int Dongia { get; set; }
    public DateTime Ngaythanhtoan { get; set; }
    public int trangthaidonhang { get; set; }

    public string nguoimua { get; set; }
    public string nguoiban { get; set; }
    
    public int trangthaithanhtoan  { get; set; }
}