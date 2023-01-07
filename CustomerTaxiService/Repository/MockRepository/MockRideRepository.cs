using CustomerTaxiService.Constants;
using CustomerTaxiService.Repository.Interfaces;
using Entities.General;
using Entities.General.RideData;

namespace CustomerTaxiService.Repository.MockRepository;

public class MockRideRepository : IRideRepository
{
    private static List<RideDb> _mockRepository = new()
    {
        new RideDb
        {
            Id = 1,
            DriverPhoneNumber = "1234",
            CustomerPhoneNumber = "1234",
            IsTaken = false,
            IsEnd = false,
            EndPointOfRide = "Heaven",
            RideDate = DateTime.Today,
            DriverFeedBack = (int)FeedBack.Normal,
            CustomerFeedBack = (int)FeedBack.Normal
        }
    };

    public async Task<string> AddNewOrder(string phoneNumber, string endPoint)
    {
        _mockRepository.Add(await CreateRideEntityForDb(phoneNumber, endPoint));
        return OrdersConstants.Ok;
    }

    public async Task<string> CheckRideForExistence(string phoneNumber)
    {
        var rideEntity = _mockRepository.FirstOrDefault(x => x.CustomerPhoneNumber == phoneNumber);

        return rideEntity == null ? OrdersConstants.RideNotFound : OrdersConstants.Ok;
    }

    private async Task<RideDb?> TakeRideDbEntity(string phoneNumber)
    {
        var rideEntity = _mockRepository.FirstOrDefault(x => x.CustomerPhoneNumber == phoneNumber)!;
        return rideEntity;
    }

    public async Task<string> CancelOrder(string phoneNumber)
    {
        var rideEntity = await TakeRideDbEntity(phoneNumber);
        if (rideEntity == null)
            return OrdersConstants.RideNotFound;
        _mockRepository.Remove(rideEntity);
        return OrdersConstants.Ok;
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
        return _mockRepository;
    }

    public async Task<string> TakeRideById(int rideId, string phoneNumber)
    {
        var rideEntity =  _mockRepository.FirstOrDefault(x=>x.Id == rideId);
        rideEntity.IsTaken = true;
        rideEntity.IsEnd = true;
        rideEntity.DriverPhoneNumber = phoneNumber;
        rideEntity.StartTime = DateTime.Now;
        _mockRepository.Remove(
            _mockRepository.FirstOrDefault(x => x.CustomerPhoneNumber == rideEntity.CustomerPhoneNumber)!);
        _mockRepository.Add(rideEntity);
        return "Ok";
    }

    public async Task<string> EndRide(string phoneNumber)
    {
        var rideEntity = _mockRepository.FirstOrDefault(x=>x.DriverPhoneNumber == phoneNumber);
        rideEntity.EndTime = DateTime.Now;
        rideEntity.IsEnd = true;
        _mockRepository.Remove(_mockRepository.FirstOrDefault(x => x.DriverPhoneNumber == phoneNumber)!);
        _mockRepository.Add(rideEntity);
        return "Ok";
    }

    private async Task<RideDb> CreateRideEntityForDb(string phoneNumber, string endPoint)
    {
        return new RideDb
        {
            CustomerPhoneNumber = phoneNumber,
            EndPointOfRide = endPoint,
            RideDate = DateTime.Now
        };
    }

    private async Task<Ride?> ConvertRideDbToRide(RideDb rideDb)
    {
        return new Ride
        {
            CustomerPhoneNumber = rideDb.CustomerPhoneNumber,
            EndPointOfRide = rideDb.EndPointOfRide,
            RideDate = rideDb.RideDate,
            DriverFeedBack = (FeedBack)rideDb.DriverFeedBack,
            CustomerFeedBack = (FeedBack)rideDb.CustomerFeedBack
        };
    }
}