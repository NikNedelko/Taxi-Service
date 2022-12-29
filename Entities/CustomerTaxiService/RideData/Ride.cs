using Entities.CustomerTaxiService.CustomerData;
using Entities.General;

namespace Entities.CustomerTaxiService.RideData;

public class Ride
{
    public string Id { get; set; }
    // public string Driver { get; set; } soon
    public string CustomerPhoneNumber { get; set; }
    public string EndPointOfRide { get; set; }
    public DateTime RideDate { get; set; }
    public FeedBack DriverFeedBack { get; set; }
    public FeedBack CustomerFeedBack { get; set; }
}