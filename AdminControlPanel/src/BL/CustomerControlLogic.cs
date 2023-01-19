using AdminControlPanel.BL.Interfaces;
using AdminControlPanel.Repository.Interfaces;
using Entities.CustomerApi.CustomerData;
using Entities.CustomerApi.Requests;
using Entities.General;
using TaxiService.BusinessLogic.Customer.Interfaces;
using TaxiService.Repository.Customer.Interfaces;

namespace AdminControlPanel.BL;

public class CustomerControlLogic : IAccountLogicForAdmin
{
    private readonly ICustomerAdminRepository _customerAdminRepository;
    private readonly IUserRepository _userRepository;

    public CustomerControlLogic(ICustomerAdminRepository customerAdminRepository, IUserRepository userRepository)
    {
        _customerAdminRepository = customerAdminRepository;
        _userRepository = userRepository;
    }

    public async Task<Response> DeleteUserById(int id)
    {
        var isUserExist = await CheckUserIsExistById(id);
        if (isUserExist != "Ok")
            return await CreateResponse(isUserExist);
        return await CreateResponse(await _customerAdminRepository.DeleteUserById(id));
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

    async Task<Response> IAccountLogic.CreateAccount(RegistrationForUser newUser)
    {
        throw new NotImplementedException();
    }

    async Task<Response> IAccountLogic.DeleteAccount(string phoneNumber)
    {
        throw new NotImplementedException();
    }

    async Task<Response> IAccountLogic.UpdateAccount(Customer model)
    {
        throw new NotImplementedException();
    }

    async Task<Response> IAccountLogic.AddMoneyToAccount(string id, decimal money)
    {
        throw new NotImplementedException();
    }
}