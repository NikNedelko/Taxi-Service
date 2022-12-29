using Entities.CustomerTaxiService.CustomerData;
using Entities.CustomerTaxiService.RideData;

namespace CustomerTaxiService.Repository.Interfaces;

public interface IRideRepository
{
    public Task<string> AddNewOrder(string phoneNumber, string endPoint);
    public Task<string> CheckRideForExistence(string phoneNumber);
    public Task<string> CancelOrder(string phoneNumber);
    public Task<Ride?> GetRideInfo(string phoneNumber);
    public Task<List<RideDb>> GetAllRides();
}