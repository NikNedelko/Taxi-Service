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
            return await CreateResponse(checkCustomerResult);

        var userAccount = await GetUserByNumber(order.PhoneNumber);
        if (userAccount == null)
            return await CreateResponse(ResponseConstants.ProblemWithUsersEntity);

        var newOrderResponse = await CreateNewOrder(userAccount, order);
        if (newOrderResponse != CreateNewOrderConstants.Ok)
            return await CreateResponse(newOrderResponse);

        return await CreateResponse(ResponseConstants.RideAccepted);
    }

    /// <summary>
    /// Add new Ride to the Database
    /// </summary>
    /// <param name="customer"></param>
    /// <param name="order"></param>
    /// <returns>Process result as string</returns>
    private async Task<string> CreateNewOrder(Customer customer, Order order)
    {
        return await _rideRepository.AddNewOrder(customer.id, order.RideEndPoint);
    }

    /// <summary>
    /// Cancel exist order by id of ride
    /// </summary>
    /// <param name="rideId"></param>
    /// <returns>Process result as string</returns>
    public async Task<Response> CancelOrder(string rideId)
    {
        var checkResult = await _rideRepository.CheckRideForExistence(rideId);
        if (checkResult != CheckInformationConstants.Ok)
            return await CreateResponse(checkResult);

        var cancelOrderResult = await _rideRepository.CancelOrder(rideId);
        return await CreateResponse(cancelOrderResult);
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
            ResponseConstants.ProblemWithUsersEntity => ResponseConstants.ProblemsWhenTryToTakeUser,
            CreateNewOrderConstants.DatabaseProblems => "",
            ResponseConstants.RideAccepted => ResponseConstants.RideAcceptedAdditionalText // add ride number/id
            ,
            _ => ""
        };
    }
}