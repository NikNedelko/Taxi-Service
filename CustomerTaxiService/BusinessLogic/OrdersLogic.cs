using CustomerTaxiService.BusinessLogic.Interfaces;
using CustomerTaxiService.Constants;
using CustomerTaxiService.Constants.General;
using CustomerTaxiService.Repository.Interfaces;
using Entities.CustomerTaxiService.CustomerData;
using Entities.CustomerTaxiService.Requests;
using Entities.General;
using Entities.General.RideData;

namespace CustomerTaxiService.BusinessLogic;

public class OrdersLogic : IOrdersLogic
{
    private readonly IUserRepository _userRepository;
    private readonly IRideRepository _rideRepository;

    public OrdersLogic(IUserRepository userRepository, IRideRepository rideRepository)
    {
        _userRepository = userRepository;
        _rideRepository = rideRepository;
    }

    public async Task<Response> BeginNewOrder(Order order)
    {
        var checkCustomerResult = await CheckInformationAboutCustomer(order.PhoneNumber);
        if (checkCustomerResult != OrdersConstants.Ok)
            return await CreateResponse(checkCustomerResult);

        var checkIfUserAlreadyHaveARide = await CheckIsAlreadyHaveAOrder(order.PhoneNumber);
        if (checkIfUserAlreadyHaveARide == OrdersConstants.UserIsAlreadyHaveAOrder)
            return await CreateResponse(OrdersConstants.UserIsAlreadyHaveAOrder);
        
        var userAccount = await GetUserByNumber(order.PhoneNumber);
        if (userAccount == null)
            return await CreateResponse(ResponseConstants.ProblemWithUsersEntity);

        var newOrderResponse = await CreateNewOrder(userAccount, order);
        if (newOrderResponse != OrdersConstants.Ok)
            return await CreateResponse(newOrderResponse);

        return await CreateResponse(ResponseConstants.RideAccepted);
    }

    private async Task<string> CreateNewOrder(Customer customer, Order order)
    {
        return await _rideRepository.AddNewOrder(customer.PhoneNumber, order.RideEndPoint);
    }

    public async Task<Response> CancelOrder(string phoneNumber)
    {
        var checkResult = await _rideRepository.CheckRideForExistence(phoneNumber);
        if (checkResult != OrdersConstants.Ok)
            return await CreateResponse(checkResult);

        var cancelOrderResult = await _rideRepository.CancelOrder(phoneNumber);
        return await CreateResponse(cancelOrderResult);
    }

    public async Task<Ride?> GetRideInfo(string phoneNumber)
    {
        return await _rideRepository.GetRideInfo(phoneNumber);
    }

    public async Task<List<RideDb>> GetAllRides()
    {
        return await _rideRepository.GetAllRides();
    }

    private async Task<string> CheckIsAlreadyHaveAOrder(string phoneNumber)
    {
        var allRides = await _rideRepository.GetAllRides();
        var rideEntity = allRides
            .FirstOrDefault(ride => ride.CustomerPhoneNumber == phoneNumber
                                    && ride.IsEnd != true);
        
        return rideEntity == null ? OrdersConstants.RideNotFound  : OrdersConstants.UserIsAlreadyHaveAOrder;
    }

    private async Task<Customer?> GetUserByNumber(string number)
    {
        return await _userRepository.GetUserByPhoneNumber(number);
    }

    private async Task<string> CheckInformationAboutCustomer(string phoneNumber)
    {
        var entityOfUser = await _userRepository.PermissionToRide(phoneNumber);
        return entityOfUser == null ? OrdersConstants.UserNotFound : OrdersConstants.Ok;
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
            OrdersConstants.UserNotFound => OrdersConstants.UserNotFoundAdditionalText,
            OrdersConstants.RideNotFound => OrdersConstants.RideNotFoundAdditionalText,
            ResponseConstants.RideAccepted => ResponseConstants.RideAcceptedAdditionalText,
            OrdersConstants.UserIsAlreadyHaveAOrder => OrdersConstants.UserIsAlreadyHaveAOrderAdditionalText
            ,
            _ => "Something went wrong"
        };
    }
}