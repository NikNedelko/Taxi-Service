using Entities.DriverApi;
using TaxiService.BusinessLogic.Customer;
using TaxiService.Repository.Customer.MockRepository;
using Tests.Unit.CustomerTests.TestData;

namespace Tests.Unit.CustomerTests;

[TestClass]
public class OrdersLogicTests : CleanupMockDatabase
{
    private readonly IOrdersLogic _ordersLogic 
        = new OrdersLogic(new MockUsersRepository(),new MockRideRepository(new MockUsersRepository()));

    [TestMethod]
    public async Task CreateNewOrder()
    {
        var userEntity = await GeneralCustomerTestDataAndMethods.GetUserDbForDatabase();
        userEntity.AvailableMoney = 100;
        var rideRequest = await GeneralCustomerTestDataAndMethods.GetNewOrder();
        
        MockDatabases.CustomerList.Add(userEntity);
        var newOrderResult = await _ordersLogic.BeginNewOrder(rideRequest);
        
        Assert.AreEqual(newOrderResult.Message,CustomerConstants.RideAccepted);
        Assert.AreEqual(newOrderResult.AdditionalInformation,CustomerConstants.RideAcceptedAdditionalText);
        Assert.IsNotNull( await GeneralCustomerTestDataAndMethods.GetRideDbByUser(userEntity.PhoneNumber, rideRequest.Price));
        userEntity.AvailableMoney -= rideRequest.Price;
        Assert.IsNotNull( await GeneralCustomerTestDataAndMethods.GetRideDbByUser(userEntity.PhoneNumber, rideRequest.Price));
        MockDatabases.CustomerList.Remove(MockDatabases.CustomerList.FirstOrDefault(x=>x.PhoneNumber == userEntity.PhoneNumber)!);
        MockDatabases.RideList.Remove(MockDatabases.RideList.FirstOrDefault(x=>x.CustomerPhoneNumber == rideRequest.PhoneNumber)!);
    }


    [TestMethod]
    public async Task CreateNewOrderWithoutUser()
    {
        var userEntity = await GeneralCustomerTestDataAndMethods.GetUserDbForDatabase();
        var rideRequest = await GeneralCustomerTestDataAndMethods.GetNewOrder();
        var newOrderResult = await _ordersLogic.BeginNewOrder(rideRequest);
        
        Assert.AreEqual(newOrderResult.Message,CustomerConstants.UserNotFound);
        Assert.AreEqual(newOrderResult.AdditionalInformation,CustomerConstants.UserNotFoundAdditionalText);
        Assert.IsNull( await GeneralCustomerTestDataAndMethods.GetRideDbByUser(userEntity.PhoneNumber, rideRequest.Price));
    }
    
    [TestMethod]
    public async Task CreateNewOrderWithoutMoney()
    {
        var userEntity = await GeneralCustomerTestDataAndMethods.GetUserDbForDatabase();
        var rideRequest = await GeneralCustomerTestDataAndMethods.GetNewOrder();
        
        MockDatabases.CustomerList.Add(userEntity);
        var newOrderResult = await _ordersLogic.BeginNewOrder(rideRequest);
        
        Assert.AreEqual(newOrderResult.Message,CustomerConstants.NotEnoughMoney);
        Assert.AreEqual(newOrderResult.AdditionalInformation,CustomerConstants.Default);
        Assert.IsNull( await GeneralCustomerTestDataAndMethods.GetRideDbByUser(userEntity.PhoneNumber, rideRequest.Price));
        MockDatabases.CustomerList.Remove(userEntity);
    }
    
    [TestMethod]
    public async Task CreateNewOrderWithoutMoneyForClass()
    {
        var userEntity = await GeneralCustomerTestDataAndMethods.GetUserDbForDatabase();
        var rideRequest = await GeneralCustomerTestDataAndMethods.GetNewOrder();
        rideRequest.DriveClass = DriveClass.Premium;
        rideRequest.Price = 50;
        userEntity.AvailableMoney = 50;
        
        MockDatabases.CustomerList.Add(userEntity);
        var newOrderResult = await _ordersLogic.BeginNewOrder(rideRequest);
        
        Assert.AreEqual(newOrderResult.Message,CustomerConstants.NotEnoughMoneyForRideClass);
        Assert.AreEqual(newOrderResult.AdditionalInformation,CustomerConstants.Default);
        Assert.IsNull(await GeneralCustomerTestDataAndMethods.GetRideDbByUser(userEntity.PhoneNumber, rideRequest.Price));
        MockDatabases.CustomerList.Remove(userEntity);
    }

    [TestMethod]
    public async Task GetCountOfOrders()
    {
        var previousList = MockDatabases.RideList;
        var newList = await GeneralCustomerTestDataAndMethods.GetRandomCountOfRides();
        MockDatabases.RideList = newList;
        var getResult = await _ordersLogic.GetAllRides();
        Assert.AreEqual(newList.Count, getResult.Count);
        MockDatabases.RideList = previousList;
        var newGetResult = await _ordersLogic.GetAllRides();
        Assert.AreEqual(previousList.Count, newGetResult.Count);
    }
}