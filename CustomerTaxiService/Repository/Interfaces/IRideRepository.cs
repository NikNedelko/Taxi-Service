using Entities.CustomerTaxiService.CustomerData;

namespace CustomerTaxiService.Repository.Interfaces;

public interface IRideRepository
{
    public Task<bool> AddNewOrder(Customer customer);
    public Task<bool> CancelOrder(string str);
}