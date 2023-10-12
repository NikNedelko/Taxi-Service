using Domain.Entities.General;
using Domain.Entities.General.RideData;

namespace DAL.Interfaces.Order;

public interface IOrdersLogic
{
    public Task<Response> BeginNewOrder(Domain.Entities.CustomerApi.Requests.OrderEntity order);
    public Task<Response> CancelOrder(string phoneNumber);
    public Task<Ride?> GetRideInfo(string phoneNumber);
    public Task<List<RideDb>> GetAllRides();
    
}