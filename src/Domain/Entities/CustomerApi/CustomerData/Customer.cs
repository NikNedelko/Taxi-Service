using Domain.Entities.General;
using Entities.CustomerApi.CustomerData.Interface;
using Entities.General;

namespace Domain.Entities.CustomerApi.CustomerData;

public class Customer : ICustomerBase
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }

    public FeedBack FeedBack { get; set; }
    public AccountStatus Status { get; set; }
    public DateTime RegistrationDate { get; set; }
    public decimal AvailableMoney { get; set; }
}