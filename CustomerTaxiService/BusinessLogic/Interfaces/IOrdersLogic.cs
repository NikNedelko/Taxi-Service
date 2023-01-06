using Entities.CustomerTaxiService.Requests;
using Entities.General;
using Entities.General.RideData;

namespace CustomerTaxiService.BusinessLogic.Interfaces;

public interface IOrdersLogic
{
    public Task<Response> BeginNewOrder(Order order);
    public Task<Response> CancelOrder(string phoneNumber);
    public Task<Ride?> GetRideInfo(string phoneNumber);
    public Task<List<RideDb>> GetAllRides();
    
}