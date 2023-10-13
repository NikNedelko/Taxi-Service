using Domain.Entities.CustomerData;
using Domain.Entities.CustomerData.Requests;
using Domain.Entities.General;

namespace DAL.Interfaces.Customer;

public interface IAccountLogic
{
    public Task<Response> CreateAccount(RegistrationForUser newUser);
    public Task<Response> DeleteAccount(string phoneNumber);
    public Task<Response> UpdateAccount(CustomerModel model);
    public Task<Response> AddMoneyToAccount(string id, decimal money);
}