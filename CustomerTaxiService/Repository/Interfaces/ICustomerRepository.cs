namespace CustomerTaxiService.Repository.Interfaces;

public interface ICustomerRepository
{
    public Task<bool> AddNewOrder(string str);
    public Task<bool> DeclineOrder(string str);
}