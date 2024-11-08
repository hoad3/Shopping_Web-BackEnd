using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Web_2.Models.Delivery;

namespace Web_2.Models;

public class User
{
    public int id { get; set; }

    // [Required]
    public string account { get; set; }

    // [Required]
    public string password { get; set; }
    
    public int role { get; set; }
    
    public shipper Shipper { get; set; }
    
    
    public virtual InformationUser InformationUser { get; set; }
}