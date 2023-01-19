using Entities.General;

namespace Entities.CustomerApi.CustomerData;

public class Customer
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public FeedBack FeedBack { get; set; }
    public AccountStatus Status { get; set; }
    public DateTime RegistrationDate { get; set; }
    public decimal AvailableMoney { get; set; }
}