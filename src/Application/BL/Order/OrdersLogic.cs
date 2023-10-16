using Application.BL.General;
using DAL.Interfaces.Order;
using DAL.Repository.Customer.Interfaces;
using Domain.Entities.CustomerData;
using Domain.Entities.CustomerData.Requests;
using Domain.Entities.DriveData;
using Domain.Entities.General;
using Domain.Entities.RideData;
using TaxiService.Constants.Customer;

namespace Application.BL.Order;

public class OrdersLogic : IOrdersLogic
{
    private readonly IUserRepository _userRepository;
    private readonly IRideRepository _rideRepository;
    private readonly GeneralMethods _generalMethods;

    public OrdersLogic(IUserRepository userRepository, IRideRepository rideRepository, GeneralMethods generalMethods)
    {
        _userRepository = userRepository;
        _rideRepository = rideRepository;
        _generalMethods = generalMethods;
    }

    #region NewOrder

    public async Task<Response> BeginNewOrder(OrderModel order)
    {
        var checkCustomerResult = await CheckIsUserExist(order.PhoneNumber);
        if (checkCustomerResult != CustomerConstants.Ok)
            return await _generalMethods.CreateResponse(checkCustomerResult);

        var checkIfUserAlreadyHaveARide = await CheckIsAlreadyHaveAnOrder(order.PhoneNumber);
        if (checkIfUserAlreadyHaveARide == CustomerConstants.UserIsAlreadyHaveAOrder)
            return await _generalMethods.CreateResponse(CustomerConstants.UserIsAlreadyHaveAOrder);

        var userAccount = await GetUserByNumber(order.PhoneNumber);
        if (userAccount == null)
            return await _generalMethods.CreateResponse(CustomerConstants.UserNotFound);

        var checkMoneyResult = await CheckMoneyForRide(order.PhoneNumber, order.Price);
        if (checkMoneyResult != CustomerConstants.Ok)
            return await _generalMethods.CreateResponse(checkMoneyResult);

        var checkClassForMoney = await CheckMoneyForDriveClass(order.PhoneNumber, order.DriveClass);
        if (checkClassForMoney != CustomerConstants.Ok)
            return await _generalMethods.CreateResponse(checkClassForMoney);

        var newOrderResponse = await AddOrderToDatabase(order);
        if (newOrderResponse != CustomerConstants.Ok)
            return await _generalMethods.CreateResponse(newOrderResponse);

        return await _generalMethods.CreateResponse(CustomerConstants.Ok);
    }

    private async Task<string> AddOrderToDatabase(OrderModel order)
    {
        return await _rideRepository.AddOrderToDatabase(order);
    }

    private async Task<string> CheckMoneyForRide(string phoneNumber, decimal count)
    {
        var userEntity = await _userRepository.GetUserByPhoneNumber(phoneNumber);
        if (userEntity is null)
            return CustomerConstants.UserNotFound;

        return userEntity.AvailableMoney >= count ? CustomerConstants.Ok : CustomerConstants.NotEnoughMoney;
    }

    private async Task<string> CheckMoneyForDriveClass(string phoneNumber, DriveClass driveClass)
    {
        var userEntity = await _userRepository.GetUserByPhoneNumber(phoneNumber);
        if (userEntity is null)
            return CustomerConstants.UserNotFound;

        return userEntity.AvailableMoney >= (int)driveClass
            ? CustomerConstants.Ok
            : CustomerConstants.NotEnoughMoneyForRideClass;
    }

    private async Task<string> CheckIsAlreadyHaveAnOrder(string phoneNumber)
    {
        var allRides = await _rideRepository.GetAllRides();
        var rideEntity = allRides
            .FirstOrDefault(ride => ride.CustomerPhoneNumber == phoneNumber
                                    && ride.IsEnd != true);

        return rideEntity == null ? CustomerConstants.RideNotFound : CustomerConstants.UserIsAlreadyHaveAOrder;
    }

    private async Task<CustomerModel?> GetUserByNumber(string number)
    {
        return await _userRepository.GetUserByPhoneNumber(number);
    }

    private async Task<string> CheckIsUserExist(string phoneNumber)
    {
        var entityOfUser = await _userRepository.PermissionToRide(phoneNumber);
        return entityOfUser == null ? CustomerConstants.UserNotFound : CustomerConstants.Ok;
    }

    #endregion

    public async Task<Response> CancelOrder(string phoneNumber)
    {
        var checkResult = await _rideRepository.CheckRideForExistence(phoneNumber);
        if (checkResult != CustomerConstants.Ok)
            return await _generalMethods.CreateResponse(checkResult);

        var cancelOrderResult = await _rideRepository.CancelOrder(phoneNumber);
        return await _generalMethods.CreateResponse(cancelOrderResult);
    }

    public async Task<Ride?> GetRideInfo(string phoneNumber)
    {
        return await _rideRepository.GetRideInfo(phoneNumber);
    }

    public async Task<List<RideDb>> GetAllRides()
    {
        return await _rideRepository.GetAllRides();
    }
}