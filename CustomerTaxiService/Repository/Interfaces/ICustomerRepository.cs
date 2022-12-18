using Entities.CustomerTaxiService.CustomerData;

namespace CustomerTaxiService.Repository.Interfaces;

public interface ICustomerRepository
{
    public Task<string> AddNewOrder(Customer customer);
    public Task<bool> CancelOrder(string str);
}