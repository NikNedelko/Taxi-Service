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

        return await CreateResponse(UserConstants.Ok);
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
            UserConstants.Ok => UserConstants.SuccessfulCreate,
            UserConstants.UserIsAlreadyExist => "",
            UserConstants.DatabaseProblem => UserConstants.DatabaseProblemResponse
            
            ,
            _ => ""
        };
    }
}