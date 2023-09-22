using System.ComponentModel.DataAnnotations;
using Entities.DriverApi;
using Entities.DriverApi.DriverData;

namespace Entities.CustomerApi.Requests;

[Serializable]
public class Order
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