using Entities.CustomerApi.Requests;
using Entities.General;
using TaxiService.BusinessLogic.Customer.Interfaces;
using TaxiService.BusinessLogic.General;
using TaxiService.Constants.Customer.Account;
using TaxiService.Repository.Customer.Interfaces;

namespace TaxiService.BusinessLogic.Customer;

public class AccountLogic : IAccountLogic
{
    private readonly IUserRepository _userRepository;
    private readonly IRideRepository _rideRepository;

    public AccountLogic(IUserRepository userRepository, IRideRepository rideRepository)
    {
        _userRepository = userRepository;
        _rideRepository = rideRepository;
    }

    public async Task<Response> CreateAccount(RegistrationForUser newUser)
    {
        var userWithThisNumber = await _userRepository.GetUserByPhoneNumber(newUser.PhoneNumber);
        if (userWithThisNumber != null)
            return await GeneralMethods.CreateResponse(UserConstants.UserIsAlreadyExist);
        var responseFromCreate = await _userRepository.AddNewUser(new Entities.CustomerApi.CustomerData.Customer
        {
            Name = newUser.Name,
            LastName = newUser.LastName,
            PhoneNumber = newUser.PhoneNumber,
            FeedBack = FeedBack.NoData,
            Status = AccountStatus.Active,
            RegistrationDate = DateTime.Now
        });
        if (responseFromCreate != UserConstants.Ok)
            await GeneralMethods.CreateResponse(responseFromCreate);

        return await GeneralMethods.CreateResponse(UserConstants.UserWasCreated);
    }

    public async Task<Response> DeleteAccount(string phoneNumber)
    {
        var checkIfInRideResult = await CheckIfUserInRide(phoneNumber);
        if (checkIfInRideResult != UserConstants.Ok)
            return await GeneralMethods.CreateResponse(checkIfInRideResult);
        
        return await GeneralMethods.CreateResponse(await _userRepository.RemoveUser(phoneNumber));
    }

    public async Task<Response> UpdateAccount(Entities.CustomerApi.CustomerData.Customer model)
    {
        var userWithThisNumber = await _userRepository.GetUserByPhoneNumber(model.PhoneNumber);
        if (userWithThisNumber == null)
            return await GeneralMethods.CreateResponse(UserConstants.UserNotFound);
        var updateResult = await _userRepository.UpdateUser(model, userWithThisNumber.PhoneNumber);
        if (updateResult != UserConstants.Ok)
            return await GeneralMethods.CreateResponse(updateResult);
        return await GeneralMethods.CreateResponse(UserConstants.UserWasUpdated);
    }

    public async Task<Response> AddMoneyToAccount(string phoneNumber, decimal money)
    {
        var userWithThisNumber = await _userRepository.GetUserByPhoneNumber(phoneNumber);
        if (userWithThisNumber == null)
            return await GeneralMethods.CreateResponse(UserConstants.UserNotFound);
        var addMoneyResult = await _userRepository.AddMoneyToAccount(phoneNumber, money);
        if (addMoneyResult != UserConstants.Ok)
            return await GeneralMethods.CreateResponse(addMoneyResult);

        return await GeneralMethods.CreateResponse(UserConstants.MoneyWasAdded);
    }

    private async Task<string> CheckIfUserInRide(string phoneNumber)
    {
        var allRides = await _rideRepository.GetAllRides();
        var rideWithThisNumber = allRides
            .FirstOrDefault(x => x.CustomerPhoneNumber == phoneNumber
                                 && x is { IsTaken: true, IsEnd: false });
        return rideWithThisNumber == null ? UserConstants.Ok : UserConstants.UserIsInRide;
    }
}