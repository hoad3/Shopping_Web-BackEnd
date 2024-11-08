namespace Web_2.Models.Delivery;

public class shipper
{
    public int idshipper { get; set; }
    public string shippername { get; set; }
    public int shipperphone { get; set; }
    public int status { get; set; }
    public int userid { get; set; }
    public User UserInfo { get; set; }
}