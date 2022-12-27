using CustomerTaxiService.BusinessLogic.Interfaces;
using Entities.CustomerTaxiService.Requests;
using Entities.CustomerTaxiService.Response;

namespace CustomerTaxiService.BusinessLogic;

public class AccountLogic : IAccountLogic
{
    public async Task<Response> CreateAccount(Registration newUser)
    {
        throw new NotImplementedException();
    }

    public async Task<Response> DeleteAccount(string model)
    {
        throw new NotImplementedException();
    }

    public async Task<Response> UpdateAccount(string model)
    {
        throw new NotImplementedException();
    }

    public async Task<Response> AddMoneyToAccount(string id)
    {
        throw new NotImplementedException();
    }
}