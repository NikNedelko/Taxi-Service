using Entities.CustomerApi.Requests;
using Entities.General.RideData;

namespace TaxiService.Repository.Customer.Interfaces;

public interface IRideRepository
{
    public Task<string> AddNewOrder(Order newOrder);
    public Task<string> CheckRideForExistence(string phoneNumber);
    public Task<string> CancelOrder(string phoneNumber);
    public Task<Ride?> GetRideInfo(string phoneNumber);
    public Task<List<RideDb>> GetAllRides();
}