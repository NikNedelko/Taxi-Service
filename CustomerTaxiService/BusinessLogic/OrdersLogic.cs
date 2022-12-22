using CustomerTaxiService.Constants;
using CustomerTaxiService.Repository.Interfaces;
using Entities.CustomerTaxiService.CustomerData;
using Entities.CustomerTaxiService.Requests;
using Entities.CustomerTaxiService.Response;

namespace CustomerTaxiService.BusinessLogic;

public class OrderLogic
{
    private readonly IUserRepository _userRepository;
    private readonly IRideRepository _rideRepository;

    public OrderLogic(IUserRepository userRepository, IRideRepository rideRepository)
    {
        _userRepository = userRepository;
        _rideRepository = rideRepository;
    }

    /// <summary>
    /// Starts the order creation process with checks
    /// </summary>
    /// <param name="order"></param>
    /// <returns>Process result as string</returns>
    public async Task<Response> BeginNewOrder(Order order)
    {
        var checkCustomerResult = await CheckInformationAboutCustomer(order.PhoneNumber);
        if (checkCustomerResult != CheckInformationConstants.Ok)
            return await CreateResponse(checkCustomerResult, null);

        var userAccount = await GetUserByNumber(order.PhoneNumber);
        if (userAccount == null)
            return await CreateResponse(ResponseConstants.ProblemWithUsersEntity,
                await TakeAdditionalInfoByMessage(ResponseConstants.ProblemWithUsersEntity));

        var newOrderResponse = await CreateNewOrder(userAccount);
        if (newOrderResponse != CreateNewOrderConstants.Ok)
            return await CreateResponse(newOrderResponse, await TakeAdditionalInfoByMessage(newOrderResponse));

        return await CreateResponse(ResponseConstants.RideAccepted,
            await TakeAdditionalInfoByMessage(ResponseConstants.RideAcceptedAdditionalText)); // add ride number/id
    }

    /// <summary>
    /// Add new Ride entity to the Database
    /// </summary>
    /// <param name="customer"></param>
    /// <returns>Process result as string</returns>
    private async Task<string> CreateNewOrder(Customer? customer)
    {
        var dbResponse = await _rideRepository.AddNewOrder(customer);
        return dbResponse ? CreateNewOrderConstants.Ok : CreateNewOrderConstants.DataBaseProblems;
    }

    /// <summary>
    /// Cancel exist order by id of ride
    /// </summary>
    /// <param name="rideId"></param>
    /// <returns>Process result as string</returns>
    public async Task<string> CancelOrder(string rideId)
    {
        await _rideRepository.CancelOrder(rideId);

        return "cancel message";
    }

    private async Task<Customer?> GetUserByNumber(string number)
    {
        return await _userRepository.GetUserByPhoneNumber(number);
    }

    private async Task<string> CheckInformationAboutCustomer(string phoneNumber)
    {
        var entityOfUser = await _userRepository.PermissionToRide(phoneNumber);
        return entityOfUser == null ? CheckInformationConstants.UserNotFound : CheckInformationConstants.UserIsExist;
    }

    private async Task<Response> CreateResponse(string message, string? additionalInformation)
        => new Response
        {
            Message = message,
            AdditionalInformation = additionalInformation ?? ""
        };

    private async Task<string> TakeAdditionalInfoByMessage(string message)
    {
        switch (message)
        {
            case ResponseConstants.ProblemWithUsersEntity:
                return ResponseConstants.ProblemsWhenTryToTakeUser;
            default:
                break;
        }

        return "";
    }
}