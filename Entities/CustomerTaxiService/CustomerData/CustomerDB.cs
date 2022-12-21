using Entities.General;

namespace Entities.CustomerTaxiService.CustomerData;

public class CustomerDB
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public int FeedBack { get; set; }
    public int Status { get; set; }
    public DateTime RegistrationDate { get; set; }
    public decimal AvailableMoney { get; set; }
}