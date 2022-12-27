using Entities.CustomerTaxiService.Requests;
using Entities.CustomerTaxiService.Response;

namespace CustomerTaxiService.BusinessLogic.Interfaces;

public interface IAccountLogic
{
    public Task<Response> CreateAccount(Registration newUser);
    public Task<Response> DeleteAccount(string model);
    public Task<Response> UpdateAccount(string model);
    public Task<Response> AddMoneyToAccount(string id);
}