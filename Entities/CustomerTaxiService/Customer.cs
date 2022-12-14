using Entities.General;

namespace Entities.CustomerTaxiService.User;

public class Customer
{
    public string id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public FeedBack FeedBack { get; set; }
    public AccountStatus Status { get; set; }
    public decimal AvailableMoney { get; set; }
}