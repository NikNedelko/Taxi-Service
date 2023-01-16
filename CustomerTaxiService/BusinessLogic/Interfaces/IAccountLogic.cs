using Entities.CustomerTaxiService.CustomerData;
using Entities.CustomerTaxiService.Requests;
using Entities.General;

namespace CustomerTaxiService.BusinessLogic.Interfaces;

public interface IAccountLogic
{
    public Task<Response> CreateAccount(RegistrationForUser newUser);
    public Task<Response> DeleteAccount(string phoneNumber);
    public Task<Response> UpdateAccount(Customer model);
    public Task<Response> AddMoneyToAccount(string id, decimal money);
}