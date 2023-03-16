using Entities.CustomerApi.Requests;
using Entities.DriverApi;
using Entities.DriverApi.DriverData;
using Entities.General;
using Entities.General.RideData;
using TaxiService.BusinessLogic.Customer.Interfaces;
using TaxiService.BusinessLogic.General;
using TaxiService.Constants.Customer;
using TaxiService.Repository.Customer.Interfaces;

namespace TaxiService.BusinessLogic.Customer;

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
        var checkCustomerResult = await CheckIsUserExist(order.PhoneNumber);
        if (checkCustomerResult != CustomerConstants.Ok)
            return await GeneralMethods.CreateResponse(checkCustomerResult);

        var checkIfUserAlreadyHaveARide = await CheckIsAlreadyHaveAnOrder(order.PhoneNumber);
        if (checkIfUserAlreadyHaveARide == CustomerConstants.UserIsAlreadyHaveAOrder)
            return await GeneralMethods.CreateResponse(CustomerConstants.UserIsAlreadyHaveAOrder);

        var userAccount = await GetUserByNumber(order.PhoneNumber);
        if (userAccount == null)
            return await GeneralMethods.CreateResponse(CustomerConstants.ProblemWithUsersEntity);

        var checkMoneyResult = await CheckMoneyForRide(order.PhoneNumber, order.Price);
        if (checkMoneyResult != CustomerConstants.Ok)
            return await GeneralMethods.CreateResponse(checkMoneyResult);
        
        var checkClassForMoney = await CheckMoneyForDriveClass(order.PhoneNumber, order.DriveClass);
        if (checkClassForMoney != CustomerConstants.Ok)
            return await GeneralMethods.CreateResponse(checkClassForMoney);

        var newOrderResponse = await CreateNewOrder(order);
        if (newOrderResponse != CustomerConstants.Ok)
            return await GeneralMethods.CreateResponse(newOrderResponse);

        return await GeneralMethods.CreateResponse(CustomerConstants.RideAccepted);
    }

    private async Task<string> CheckMoneyForRide(string phoneNumber, decimal count)
    {
        var userEntity = await _userRepository.GetUserByPhoneNumber(phoneNumber);
        return userEntity.AvailableMoney >= count ? CustomerConstants.Ok : CustomerConstants.NotEnoughMoney;
    }

    private async Task<string> CheckMoneyForDriveClass(string phoneNumber, DriveClass driveClass)
    {
        var userEntity = await _userRepository.GetUserByPhoneNumber(phoneNumber);
        return userEntity.AvailableMoney >= (int)driveClass ? CustomerConstants.Ok : CustomerConstants.NotEnoughMoneyForRideClass;
    }
    
    private async Task<string> CreateNewOrder(Order order)
    {
        return await _rideRepository.AddNewOrder(order);
    }

    public async Task<Response> CancelOrder(string phoneNumber)
    {
        var checkResult = await _rideRepository.CheckRideForExistence(phoneNumber);
        if (checkResult != CustomerConstants.Ok)
            return await GeneralMethods.CreateResponse(checkResult);

        var cancelOrderResult = await _rideRepository.CancelOrder(phoneNumber);
        return await GeneralMethods.CreateResponse(cancelOrderResult);
    }

    public async Task<Ride?> GetRideInfo(string phoneNumber)
    {
        return await _rideRepository.GetRideInfo(phoneNumber);
    }

    public async Task<List<RideDb>> GetAllRides()
    {
        return await _rideRepository.GetAllRides();
    }

    private async Task<string> CheckIsAlreadyHaveAnOrder(string phoneNumber)
    {
        var allRides = await _rideRepository.GetAllRides();
        var rideEntity = allRides
            .FirstOrDefault(ride => ride.CustomerPhoneNumber == phoneNumber
                                    && ride.IsEnd != true);

        return rideEntity == null ? CustomerConstants.RideNotFound : CustomerConstants.UserIsAlreadyHaveAOrder;
    }

    private async Task<Entities.CustomerApi.CustomerData.Customer?> GetUserByNumber(string number)
    {
        return await _userRepository.GetUserByPhoneNumber(number);
    }

    private async Task<string> CheckIsUserExist(string phoneNumber)
    {
        var entityOfUser = await _userRepository.PermissionToRide(phoneNumber);
        return entityOfUser == null ? CustomerConstants.UserNotFound : CustomerConstants.Ok;
    }
}