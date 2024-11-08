namespace Web_2.Models.Delivery;

public class delivery
{
    public int deliveryid {get;set;}
    public int idshipper {get;set;}
    public int thanhtoanid {get;set;}
    public DateTime pickuptime {get;set;}
    public DateTime deliverytime {get;set;}
    public int deliverystatus {get;set;}
    public int idnguoiban {get;set;}
    public int idnguoimua {get;set;}
    
    public shipper shipper {get;set;}
    public Thanhtoan.ThanhToan Thanhtoan {get;set;}
}