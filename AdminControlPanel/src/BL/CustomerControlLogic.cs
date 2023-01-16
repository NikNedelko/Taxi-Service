using AdminControlPanel.BL.Interfaces;
using AdminControlPanel.Repository.Interfaces;
using CustomerTaxiService.Repository.Interfaces;
using Entities.CustomerTaxiService.CustomerData;
using Entities.CustomerTaxiService.Requests;
using Entities.General;

namespace AdminControlPanel.BL;

internal abstract class CustomerControlLogic : IAccountLogicForAdmin
{
    private readonly ICustomerAdminRepository _customerAdminRepository;
    private readonly IUserRepository _userRepository;

    protected CustomerControlLogic(ICustomerAdminRepository customerAdminRepository, IUserRepository userRepository)
    {
        _customerAdminRepository = customerAdminRepository;
        _userRepository = userRepository;
    }

    public async Task<Response> DeleteUserById(int id)
    {
        var isUserExist = await CheckUserIsExistById(id);
        if (isUserExist != "Ok")
            return await CreateResponse(isUserExist);
        return await CreateResponse( await _customerAdminRepository.DeleteUserById(id));
    }

    public Task<List<CustomerDB>> GetAllUsersWithId()
    {
        return _customerAdminRepository.GetAllUserWIthId();
    }

    public async Task<Response> ChangeAccountStatus(string phoneNumber, AccountStatus newAccountStatus)
    {
       var userWithThisNumber = await _userRepository.CheckOfExist(phoneNumber);
       if (userWithThisNumber == "User with this phone number is not exist")
           return await CreateResponse("");
       return await CreateResponse(await _customerAdminRepository.ChangeCustomerStatus(phoneNumber, newAccountStatus));
    }

    private async Task<string> CheckUserIsExistById(int id)
    {
        var entity = await _customerAdminRepository.GetUserById(id);
        return entity == null ? "User is not exist" : "Ok";
    }
    
    private async Task<Response> CreateResponse(string message)
        => new Response
        {
            Message = message,
            AdditionalInformation = await TakeAdditionalInfoByMessage(message) ?? ""
        };

    private async Task<string?> TakeAdditionalInfoByMessage(string message)
    {
        return message switch
        {
            _ => ""
        };
    }

    public abstract Task<Response> CreateAccount(RegistrationForUser newUser);
    public abstract Task<Response> DeleteAccount(string phoneNumber);
    public abstract Task<Response> UpdateAccount(Customer model);
    public abstract Task<Response> AddMoneyToAccount(string id, decimal money);
    public abstract Task<List<CustomerDB>> GetAllUsers();
}