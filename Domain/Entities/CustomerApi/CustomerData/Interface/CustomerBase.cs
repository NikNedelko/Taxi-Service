namespace Entities.CustomerApi.CustomerData.Interface;

public interface ICustomerBase
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
}