
using Entities.DriverApi;
using Entities.DriverApi.DriverData;

namespace Entities.General.RideData;

public class Ride
{
    public int Id { get; set; }
    public string DriverPhoneNumber { get; set; }
    public string CustomerPhoneNumber { get; set; }
    public decimal Price { get; set; }
    public DriveClass DriveClass { get; set; }
    public bool IsTaken { get; set; }
    public bool IsOnRide { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string EndPointOfRide { get; set; }
    public DateTime RideDate { get; set; }
    public FeedBack DriverFeedBack { get; set; }
    public FeedBack CustomerFeedBack { get; set; }
}