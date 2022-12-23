using Entities.CustomerTaxiService.CustomerData;
using Entities.CustomerTaxiService.RideData;

namespace CustomerTaxiService.Repository.Interfaces;

public interface IRideRepository
{
    public Task<string> AddNewOrder(int userId, string endPoint);
    public Task<string> CheckRideForExistence(string rideId);
    public Task<string> CancelOrder(string rideId);
}