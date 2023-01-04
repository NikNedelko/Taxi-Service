namespace Entities.DriverApi.Driver;

public class DriverDB
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string DriverLicenseNumber { get; set; }
    public string Car { get; set; }
    public int DriveClass { get; set; }
    public int Status { get; set; }
    public int FeedBack { get; set; }
    public DateTime RegistrationDate { get; set; }
    public decimal Balance { get; set; }
}