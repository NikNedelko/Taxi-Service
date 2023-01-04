using Entities.General;

namespace Entities.DriverApi.Driver;

public class Driver
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string DriverLicenseNumber { get; set; }
    public string Car { get; set; }
    public DriveClass DriveClass { get; set; }
    public AccountStatus Status { get; set; }
    public FeedBack FeedBack { get; set; }
    public DateTime RegistrationDate { get; set; }
    public decimal Balance { get; set; }
}