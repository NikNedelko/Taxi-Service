using Entities.CustomerTaxiService.Requests;
using Entities.CustomerTaxiService.Response;

namespace CustomerTaxiService.BusinessLogic.Interfaces;

public interface IOrdersLogic
{
    public Task<Response> BeginNewOrder(Order order);
    public Task<Response> CancelOrder(int rideId);
}