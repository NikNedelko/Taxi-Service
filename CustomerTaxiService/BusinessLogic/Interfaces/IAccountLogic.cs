using Entities.CustomerTaxiService.CustomerData;
using Entities.CustomerTaxiService.Requests;
using Entities.CustomerTaxiService.Response;

namespace CustomerTaxiService.BusinessLogic.Interfaces;

public interface IAccountLogic
{
    public Task<Response> CreateAccount(Registration newUser);
    public Task<Response> DeleteAccount(string phoneNumber);
    public Task<Response> UpdateAccount(Customer model);
    public Task<Response> AddMoneyToAccount(string id, decimal money);
    public Task<List<CustomerDB>> GetAllUsers();
}