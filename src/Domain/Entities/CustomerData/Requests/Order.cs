using System.ComponentModel.DataAnnotations;
using Domain.Entities.DriveData;
using Domain.Entities.DriverData;

namespace Domain.Entities.CustomerData.Requests;

[Serializable]
public class OrderModel
{
    [Required]
    public string PhoneNumber { get; set; }
    [Required]
    public string RideEndPoint { get; set; }
    [Required]
    [Range(0,50000)]
    public decimal Price { get; set; }
    [Required]
    public DriveClass DriveClass { get; set; }
}