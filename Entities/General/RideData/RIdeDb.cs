namespace Entities.General.RideData;

public class RideDb
{
    public int Id { get; set; }
    public string DriverPhoneNumber { get; set; }
    public string CustomerPhoneNumber { get; set; }
    public bool IsTaken { get; set; }
    public bool IsEnd { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string EndPointOfRide { get; set; }
    public DateTime RideDate { get; set; }
    public int DriverFeedBack { get; set; }
    public int CustomerFeedBack { get; set; }
}