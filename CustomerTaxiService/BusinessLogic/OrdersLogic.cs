using CustomerTaxiService.BusinessLogic.Interfaces;
using CustomerTaxiService.Constants;
using CustomerTaxiService.Repository.Interfaces;
using Entities.CustomerTaxiService.CustomerData;
using Entities.CustomerTaxiService.Requests;
using Entities.CustomerTaxiService.Response;
using Entities.CustomerTaxiService.RideData;

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

    private async Task<string> CreateNewOrder(Customer customer, Order order)
    {
        return await _rideRepository.AddNewOrder(customer.PhoneNumber, order.RideEndPoint);
    }
    
    public async Task<Response> CancelOrder(string phoneNumber)
    {
        var checkResult = await _rideRepository.CheckRideForExistence(phoneNumber);
        if (checkResult != CheckInformationConstants.Ok)
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

    private async Task<Customer?> GetUserByNumber(string number)
    {
        return await _userRepository.GetUserByPhoneNumber(number);
    }

    private async Task<string> CheckInformationAboutCustomer(string phoneNumber)
    {
        var entityOfUser = await _userRepository.PermissionToRide(phoneNumber);
        return entityOfUser == null ? CheckInformationConstants.UserNotFound : CheckInformationConstants.Ok;
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