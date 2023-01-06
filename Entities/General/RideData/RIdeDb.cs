namespace Entities.CustomerTaxiService.RideData;

public class RideDb
{
    public int Id { get; set; }
    // public string Driver { get; set; } soon
    public string CustomerPhoneNumber { get; set; }
    public string EndPointOfRide { get; set; }
    public DateTime RideDate { get; set; }
    public int DriverFeedBack { get; set; }
    public int CustomerFeedBack { get; set; }
}