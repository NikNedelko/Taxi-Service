using DAL.Mock.MockDatabase;
using DAL.Repository.Customer.Interfaces;
using Domain.Entities.CustomerData.Requests;
using Domain.Entities.General;
using Domain.Entities.RideData;
using TaxiService.Constants.Customer;

namespace DAL.Mock.MockRepository;

public class MockRideRepository : IRideRepository
{
    private readonly IUserRepository _userRepository;

    public MockRideRepository(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<string> AddOrderToDatabase(OrderModel newOrder)
    {
        MockDatabases.RideList.Add(await CreateRideEntityForDb(newOrder));

        var customerEntity = await _userRepository.GetUserByPhoneNumber(newOrder.PhoneNumber);
        customerEntity.AvailableMoney -= newOrder.Price;
        _ = _userRepository.UpdateUser(customerEntity, newOrder.PhoneNumber);

        return CustomerConstants.Ok;
    }

    public async Task<string> CheckRideForExistence(string phoneNumber)
    {
        var rideEntity = MockDatabases.RideList.FirstOrDefault(x => x.CustomerPhoneNumber == phoneNumber);

        return rideEntity == null ? CustomerConstants.RideNotFound : CustomerConstants.Ok;
    }

    private async Task<RideDb?> TakeRideDbEntity(string phoneNumber)
    {
        var rideEntity = MockDatabases.RideList.FirstOrDefault(x => x.CustomerPhoneNumber == phoneNumber)!;
        return rideEntity;
    }

    public async Task<string> CancelOrder(string phoneNumber)
    {
        var rideEntity = await TakeRideDbEntity(phoneNumber);
        if (rideEntity == null)
            return CustomerConstants.RideNotFound;
        MockDatabases.RideList.Remove(rideEntity);

        var customerEntity = await _userRepository.GetUserByPhoneNumber(phoneNumber);

        customerEntity.AvailableMoney += rideEntity.Price;
        _ = _userRepository.UpdateUser(customerEntity, phoneNumber);

        return CustomerConstants.Ok;
    }

    public async Task<Ride?> GetRideInfo(string phoneNumber)
    {
        var rideEntity = await TakeRideDbEntity(phoneNumber);
        if (rideEntity == null)
            return null;
        return await ConvertRideDbToRide(rideEntity);
    }

    public async Task<List<RideDb>> GetAllRides()
    {
        return MockDatabases.RideList;
    }

    private async Task<RideDb> CreateRideEntityForDb(OrderModel order)
    {
        return new RideDb
        {
            CustomerPhoneNumber = order.PhoneNumber,
            EndPointOfRide = order.RideEndPoint,
            RideDate = DateTime.Now,
            Price = order.Price,
            DriveClass = order.DriveClass
        };
    }

    private async Task<Ride?> ConvertRideDbToRide(RideDb rideDb)
    {
        return new Ride
        {
            Id = rideDb.Id,
            CustomerPhoneNumber = rideDb.CustomerPhoneNumber,
            EndPointOfRide = rideDb.EndPointOfRide,
            RideDate = rideDb.RideDate,
            DriverFeedBack = (FeedBack)rideDb.DriverFeedBack,
            CustomerFeedBack = (FeedBack)rideDb.CustomerFeedBack
        };
    }
}