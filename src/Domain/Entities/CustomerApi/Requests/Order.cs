using System.ComponentModel.DataAnnotations;
using Domain.Entities.DriverApi.DriverData;

namespace Domain.Entities.CustomerApi.Requests;

[Serializable]
public class OrderEntity
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