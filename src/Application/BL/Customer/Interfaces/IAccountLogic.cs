using Domain.Entities.CustomerApi.Requests;
using Domain.Entities.General;

namespace Application.BL.Customer.Interfaces;

public interface IAccountLogic
{
    public Task<Response> CreateAccount(RegistrationForUser newUser);
    public Task<Response> DeleteAccount(string phoneNumber);
    public Task<Response> UpdateAccount(Domain.Entities.CustomerApi.CustomerData.CustomerModel model);
    public Task<Response> AddMoneyToAccount(string id, decimal money);
}