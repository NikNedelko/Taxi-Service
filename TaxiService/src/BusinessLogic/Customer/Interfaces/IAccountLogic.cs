using Entities.CustomerApi.Requests;
using Entities.General;

namespace TaxiService.BusinessLogic.Customer.Interfaces;

public interface IAccountLogic
{
    public Task<Response> CreateAccount(RegistrationForUser newUser);
    public Task<Response> DeleteAccount(string phoneNumber);
    public Task<Response> UpdateAccount(Entities.CustomerApi.CustomerData.Customer model);
    public Task<Response> AddMoneyToAccount(string id, decimal money);
}