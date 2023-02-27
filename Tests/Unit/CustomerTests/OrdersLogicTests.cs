using Entities.DriverApi;
using TaxiService.BusinessLogic.Customer;
using TaxiService.BusinessLogic.General;
using TaxiService.Repository.Customer.MockRepository;
using Tests.Unit.Constants;
using Tests.Unit.CustomerTests.TestData;

namespace Tests.Unit.CustomerTests;

[TestClass]
public class OrdersLogicTests
{
    private readonly IOrdersLogic _ordersLogic 
        = new OrdersLogic(new MockUsersRepository(),new MockRideRepository(new MockUsersRepository()));

    [TestMethod]
    public async Task CreateNewOrder()
    {
        var userEntity = await GeneralCustomerTestDataAndMethods.GetUserDbForDatabase();
        userEntity.AvailableMoney += 100;
        var rideRequest = await GeneralCustomerTestDataAndMethods.GetNewOrder();
        
        MockDatabases.CustomerList.Add(userEntity);
        var newOrderResult = await _ordersLogic.BeginNewOrder(rideRequest);
        
        Assert.AreEqual(newOrderResult.Message,CustomerConstants.RideAccepted);
        Assert.AreEqual(newOrderResult.AdditionalInformation,CustomerConstants.RideAcceptedAdditionalText);
        Assert.IsNotNull(GeneralCustomerTestDataAndMethods.GetRideDbByUser(userEntity.PhoneNumber, rideRequest.Price));

        MockDatabases.RideList.Remove(
            MockDatabases.RideList.FirstOrDefault(x => x.CustomerPhoneNumber == userEntity.PhoneNumber 
                                                       && x.Price == rideRequest.Price)!);
        Assert.IsNull( await GeneralCustomerTestDataAndMethods.GetRideDbByUser(userEntity.PhoneNumber, rideRequest.Price));
    }
}