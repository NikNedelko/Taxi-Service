using Entities.DriverApi;
using TaxiService.BusinessLogic.Customer;
using TaxiService.Repository.Customer.MockRepository;
using Tests.Unit.Constants;

namespace Tests.Unit.CustomerTests;

[TestClass]
public class OrdersLogicTests
{
    private readonly IOrdersLogic _ordersLogic 
        = new OrdersLogic(new MockUsersRepository(),new MockRideRepository(new MockUsersRepository()));

    public async Task CreateNewOrder()
    {
        
    }

    private async Task<Order> GetOrder() => new Order
    {
        PhoneNumber = CustomerTestsConstants.UserDb_Phonenumber,
        RideEndPoint = "EndPlace",
        Price = 20,
        DriveClass = DriveClass.Economic
    };
}