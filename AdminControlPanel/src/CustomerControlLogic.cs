using AdminControlPanel.Interfaces;
using CustomerTaxiService.Repository.Interfaces;
using Entities.CustomerTaxiService.CustomerData;
using Entities.CustomerTaxiService.Requests;
using Entities.General;

namespace AdminControlPanel;

internal abstract class CustomerControlLogic : IAccountLogicForAdmin
{
    private IUserRepository _userRepository;

    protected CustomerControlLogic(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<string> ChangeAccountStatus(string accountId, AccountStatus newAccountStatus)
    {
        
    }

    public abstract Task<Response> CreateAccount(RegistrationForUser newUser);
    public abstract Task<Response> DeleteAccount(string phoneNumber);
    public abstract Task<Response> UpdateAccount(Customer model);
    public abstract Task<Response> AddMoneyToAccount(string id, decimal money);
    public abstract Task<List<CustomerDB>> GetAllUsers();
}