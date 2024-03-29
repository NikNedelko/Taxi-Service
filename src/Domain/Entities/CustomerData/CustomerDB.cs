using Domain.Entities.CustomerData.Interface;

namespace Domain.Entities.CustomerData;

public class CustomerDB : ICustomerBase
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public int FeedBack { get; set; }
    public int Status { get; set; }
    public DateTime RegistrationDate { get; set; }
    public decimal AvailableMoney { get; set; }
}