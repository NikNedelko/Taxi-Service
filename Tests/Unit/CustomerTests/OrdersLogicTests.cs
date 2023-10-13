using Application.BL.Customer;
using Application.BL.General;
using Application.BL.Order;
using DAL.Interfaces.Customer;
using DAL.Interfaces.Order;
using DAL.Mock.MockDatabase;
using DAL.Mock.MockRepository;
using Domain.Entities.DriveData;
using Domain.Entities.DriverData;
using Tests.Unit.Data;

namespace Tests.Unit.CustomerTests;

[TestClass]
public class OrdersLogicTests
{
    private readonly IOrdersLogic _ordersLogic
        = new OrdersLogic(new MockUsersRepository(), new MockRideRepository(new MockUsersRepository()), new GeneralMethods());

    [TestMethod]
    public async Task CreateNewOrder()
    {
        var userEntity = await TestDataAndMethods.GetUserDbForDatabase();
        userEntity.AvailableMoney = 100;
        var rideRequest = await TestDataAndMethods.GetNewOrder();

        MockDatabases.CustomerList.Add(userEntity);
        var newOrderResult = await _ordersLogic.BeginNewOrder(rideRequest);

        Assert.AreEqual(newOrderResult.Message, CustomerConstants.Ok);
        Assert.AreEqual(newOrderResult.AdditionalInformation, CustomerConstants.Default);
        Assert.IsNotNull(
            await TestDataAndMethods.GetRideDbByUser(userEntity.PhoneNumber, rideRequest.Price));
        userEntity.AvailableMoney -= rideRequest.Price;
        Assert.IsNotNull(
            await TestDataAndMethods.GetRideDbByUser(userEntity.PhoneNumber, rideRequest.Price));
        MockDatabases.CustomerList.Remove(
            MockDatabases.CustomerList.FirstOrDefault(x => x.PhoneNumber == userEntity.PhoneNumber)!);
        MockDatabases.RideList.Remove(
            MockDatabases.RideList.FirstOrDefault(x => x.CustomerPhoneNumber == rideRequest.PhoneNumber)!);
    }


    [TestMethod]
    public async Task CreateNewOrderWithoutUser()
    {
        var userEntity = await TestDataAndMethods.GetUserDbForDatabase();
        var rideRequest = await TestDataAndMethods.GetNewOrder();
        var newOrderResult = await _ordersLogic.BeginNewOrder(rideRequest);

        Assert.AreEqual(newOrderResult.Message, CustomerConstants.UserNotFound);
        Assert.AreEqual(newOrderResult.AdditionalInformation, CustomerConstants.UserNotFoundAdditionalText);
        Assert.IsNull(
            await TestDataAndMethods.GetRideDbByUser(userEntity.PhoneNumber, rideRequest.Price));
    }

    [TestMethod]
    public async Task CreateNewOrderWithoutMoney()
    {
        var userEntity = await TestDataAndMethods.GetUserDbForDatabase();
        var rideRequest = await TestDataAndMethods.GetNewOrder();

        MockDatabases.CustomerList.Add(userEntity);
        var newOrderResult = await _ordersLogic.BeginNewOrder(rideRequest);

        Assert.AreEqual(newOrderResult.Message, CustomerConstants.NotEnoughMoney);
        Assert.AreEqual(newOrderResult.AdditionalInformation, CustomerConstants.Default);
        Assert.IsNull(
            await TestDataAndMethods.GetRideDbByUser(userEntity.PhoneNumber, rideRequest.Price));
        MockDatabases.CustomerList.Remove(userEntity);
    }

    [TestMethod]
    public async Task CreateNewOrderWithoutMoneyForClass()
    {
        var userEntity = await TestDataAndMethods.GetUserDbForDatabase();
        var rideRequest = await TestDataAndMethods.GetNewOrder();
        rideRequest.DriveClass = DriveClass.Premium;
        rideRequest.Price = 50;
        userEntity.AvailableMoney = 50;

        MockDatabases.CustomerList.Add(userEntity);
        var newOrderResult = await _ordersLogic.BeginNewOrder(rideRequest);

        Assert.AreEqual(newOrderResult.Message, CustomerConstants.NotEnoughMoneyForRideClass);
        Assert.AreEqual(newOrderResult.AdditionalInformation, CustomerConstants.Default);
        Assert.IsNull(
            await TestDataAndMethods.GetRideDbByUser(userEntity.PhoneNumber, rideRequest.Price));
        MockDatabases.CustomerList.Remove(userEntity);
    }

    [TestMethod]
    public async Task CancelExistedOrder()
    {
        var userEntity = await TestDataAndMethods.GetUserDbForDatabase();
        var rideEntity = await TestDataAndMethods.GetRideDbEntity();
        MockDatabases.CustomerList.Add(userEntity);
        MockDatabases.RideList.Add(rideEntity);
        var cancelResult = await _ordersLogic.CancelOrder(userEntity.PhoneNumber);
        Assert.IsNotNull(cancelResult);
        Assert.AreEqual(CustomerConstants.Ok, cancelResult.Message);
        Assert.AreEqual( CustomerConstants.Default, cancelResult.AdditionalInformation);
        Assert.IsNull(MockDatabases.RideList.FirstOrDefault(x=>x.Id == rideEntity.Id));
        MockDatabases.CustomerList.Remove(userEntity);
    }
    
    [TestMethod]
    public async Task CancelNotExistedOrder()
    {
        var userEntity = await TestDataAndMethods.GetUserDbForDatabase();
        var rideEntity = await TestDataAndMethods.GetRideDbEntity();
        var cancelResult = await _ordersLogic.CancelOrder(userEntity.PhoneNumber);
        Assert.IsNotNull(cancelResult);
        Assert.AreEqual(CustomerConstants.RideNotFound, cancelResult.Message);
        Assert.AreEqual( CustomerConstants.RideNotFoundAdditionalText, cancelResult.AdditionalInformation);
        Assert.IsNull(MockDatabases.RideList.FirstOrDefault(x=>x.Id == rideEntity.Id));
    }

    [TestMethod]
    public async Task GetCountOfOrders()
    {
        var previousList = MockDatabases.RideList;
        var newList = await TestDataAndMethods.GetRandomCountOfRides();
        MockDatabases.RideList = newList;
        var getResult = await _ordersLogic.GetAllRides();
        Assert.AreEqual(newList.Count, getResult.Count);
        MockDatabases.RideList = previousList;
        var newGetResult = await _ordersLogic.GetAllRides();
        Assert.AreEqual(previousList.Count, newGetResult.Count);
    }

    [TestMethod]
    public async Task GetInfoAboutExistedRide()
    {
        var rideEntity = await TestDataAndMethods.GetRideDbEntity();
        MockDatabases.RideList.Add(rideEntity);
        var getInfoResult = await _ordersLogic.GetRideInfo(rideEntity.CustomerPhoneNumber);
        Assert.IsNotNull(getInfoResult);
        Assert.AreEqual(rideEntity.Id, getInfoResult.Id);
        Assert.AreEqual(rideEntity.CustomerPhoneNumber, getInfoResult.CustomerPhoneNumber);
        MockDatabases.RideList.Remove(rideEntity);
    }

    [TestMethod]
    public async Task GetInfoAboutNotExistedRide()
    {
        var rideEntity = await TestDataAndMethods.GetRideDbEntity();
        var getInfoResult = await _ordersLogic.GetRideInfo(rideEntity.CustomerPhoneNumber);
        Assert.IsNull(getInfoResult);
        Assert.IsNull(MockDatabases.RideList.FirstOrDefault(x =>
            x.Id == rideEntity.Id || x.CustomerPhoneNumber == rideEntity.CustomerPhoneNumber));
    }
}