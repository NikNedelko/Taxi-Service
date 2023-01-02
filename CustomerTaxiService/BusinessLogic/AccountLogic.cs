using CustomerTaxiService.BusinessLogic.Interfaces;
using CustomerTaxiService.Constants;
using CustomerTaxiService.Repository.Interfaces;
using Entities.CustomerTaxiService.CustomerData;
using Entities.CustomerTaxiService.Requests;
using Entities.CustomerTaxiService.Response;
using Entities.General;

namespace CustomerTaxiService.BusinessLogic;

public class AccountLogic : IAccountLogic
{
    private readonly IUserRepository _userRepository;

    public AccountLogic(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Response> CreateAccount(Registration newUser)
    {
        var userWithThisNumber = await _userRepository.GetUserByPhoneNumber(newUser.PhoneNumber);
        if (userWithThisNumber != null)
            return await CreateResponse(UserConstants.UserIsAlreadyExist);
        var responseFromCreate = await _userRepository.AddNewUser(new Customer
        {
            Name = newUser.Name,
            LastName = newUser.LastName,
            PhoneNumber = newUser.PhoneNumber,
            FeedBack = FeedBack.NoData,
            Status = AccountStatus.Active,
            RegistrationDate = DateTime.Now
        });
        if (responseFromCreate != UserConstants.Ok)
            await CreateResponse(responseFromCreate);

        return await CreateResponse(UserConstants.UserWasCreated);
    }

    public async Task<Response> DeleteAccount(string phoneNumber)
    {
        return await CreateResponse(await _userRepository.RemoveUser(phoneNumber));
    }

    public async Task<Response> UpdateAccount(Customer model)
    {
        var userWithThisNumber = await _userRepository.GetUserByPhoneNumber(model.PhoneNumber);
        if (userWithThisNumber == null)
            return await CreateResponse(UserConstants.UserNotFound);
        var updateResult = await _userRepository.UpdateUser(model, userWithThisNumber.PhoneNumber);
        if (updateResult != UserConstants.Ok)
            return await CreateResponse(updateResult);
        return await CreateResponse(UserConstants.UserWasUpdated);
    }

    public async Task<Response> AddMoneyToAccount(string phoneNumber, decimal money)
    {
        var userWithThisNumber = await _userRepository.GetUserByPhoneNumber(phoneNumber);
        if (userWithThisNumber == null)
            return await CreateResponse(UserConstants.UserNotFound);
        var addMoneyResult = await _userRepository.AddMoneyToAccount(phoneNumber, money);
        if (addMoneyResult != UserConstants.Ok)
            return await CreateResponse(addMoneyResult);

        return await CreateResponse(UserConstants.MoneyWasAdded);
    }

    public async Task<List<CustomerDB>> GetAllUsers()
    {
        return await _userRepository.GetAllUsers();
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
            UserConstants.UserWasCreated => UserConstants.SuccessfulCreate,
            UserConstants.DatabaseProblem => UserConstants.DatabaseProblemResponse,
            UserConstants.UserWasDeleted => UserConstants.UserWasUpdatedAdditionalText,
            UserConstants.MoneyWasAdded =>"",
            _ => ""
        };
    }
}