namespace CustomerTaxiService.Repository.Interfaces;

public interface ICustomerRepository
{
    public Task<string> AddNewOrder(string str);
    public Task<bool> CancelOrder(string str);
}