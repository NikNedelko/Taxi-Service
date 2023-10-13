using Application.BL.General;
using DAL.Interfaces.Customer;
using DAL.Repository.Customer.Interfaces;
using Domain.Entities.CustomerApi.CustomerData;
using Domain.Entities.CustomerData;
using Domain.Entities.CustomerData.Requests;
using Domain.Entities.General;
using TaxiService.Constants.Customer;

namespace Application.BL.Customer;

public class AccountLogic : IAccountLogic
{
    private readonly IUserRepository _userRepository;
    private readonly IRideRepository _rideRepository;
    private readonly GeneralMethods _generalMethods;

    public AccountLogic(IUserRepository userRepository, IRideRepository rideRepository, GeneralMethods generalMethods)
    {
        _userRepository = userRepository;
        _rideRepository = rideRepository;
        _generalMethods = generalMethods;
    }

    public async Task<Response> CreateAccount(RegistrationForUser newUser)
    {
        var userWithThisNumber = await _userRepository.GetUserByPhoneNumber(newUser.PhoneNumber);
        if (userWithThisNumber != null)
            return await _generalMethods.CreateResponse(CustomerConstants.UserIsAlreadyExist);

        var responseFromCreate = await _userRepository.AddNewUser(new CustomerModel
        {
            Name = newUser.Name,
            LastName = newUser.LastName,
            PhoneNumber = newUser.PhoneNumber,
            FeedBack = FeedBack.NoData,
            Email = newUser.Email,
            Status = AccountStatus.Active,
            RegistrationDate = DateTime.Now
        });

        if (responseFromCreate != CustomerConstants.Ok)
            return await _generalMethods.CreateResponse(responseFromCreate);

        return await _generalMethods.CreateResponse(CustomerConstants.Ok);
    }

    public async Task<Response> DeleteAccount(string phoneNumber)
    {
        var checkIfInRideResult = await CheckIfUserInRide(phoneNumber);
        if (checkIfInRideResult != CustomerConstants.Ok)
            return await _generalMethods.CreateResponse(checkIfInRideResult);

        var deleteResult = await _userRepository.RemoveUser(phoneNumber);

        return await _generalMethods.CreateResponse(deleteResult);
    }

    public async Task<Response> UpdateAccount(CustomerModel model)
    {
        var userWithThisNumber = await _userRepository.GetUserByPhoneNumber(model.PhoneNumber);

        if (userWithThisNumber == null)
            return await _generalMethods.CreateResponse(CustomerConstants.UserNotFound);

        var updateResult = await _userRepository.UpdateUser(model, userWithThisNumber.PhoneNumber);
        if (updateResult != CustomerConstants.Ok)
            return await _generalMethods.CreateResponse(updateResult);

        return await _generalMethods.CreateResponse(CustomerConstants.Ok);
    }

    public async Task<Response> AddMoneyToAccount(string phoneNumber, decimal money)
    {
        var userWithThisNumber = await _userRepository.GetUserByPhoneNumber(phoneNumber);

        if (userWithThisNumber == null)
            return await _generalMethods.CreateResponse(CustomerConstants.UserNotFound);

        var addMoneyResult = await _userRepository.AddMoneyToAccount(phoneNumber, money);

        if (addMoneyResult != CustomerConstants.Ok)
            return await _generalMethods.CreateResponse(addMoneyResult);

        return await _generalMethods.CreateResponse(CustomerConstants.Ok);
    }

    private async Task<string> CheckIfUserInRide(string phoneNumber)
    {
        var allRides = await _rideRepository.GetAllRides();

        var rideWithThisNumber = allRides
            .FirstOrDefault(x => x.CustomerPhoneNumber == phoneNumber
                                 && x is { IsTaken: true, IsEnd: false });
        return rideWithThisNumber == null ? CustomerConstants.Ok : CustomerConstants.UserIsInRide;
    }
}