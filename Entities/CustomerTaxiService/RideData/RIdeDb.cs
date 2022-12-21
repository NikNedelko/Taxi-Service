namespace Entities.CustomerTaxiService.RideData;

public class RIdeDb
{
    public string Id { get; set; }
    // public string Driver { get; set; } soon
    public string CustomerId { get; set; }
    public string EndPointOfRide { get; set; }
    public int DriverFeedBack { get; set; }
    public int CustomerFeedBack { get; set; }
}