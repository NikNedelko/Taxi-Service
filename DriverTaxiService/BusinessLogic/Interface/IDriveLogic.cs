using Entities.General;

namespace DriverTaxiService.BusinessLogic.Interface;

public interface IDriveLogic
{
    public Task<Response> StartWork(string phoneNumber);
    public Task<Response> EndWork(string phoneNumber);
    public Task<Response> GetAllAvailableOrders(string phoneNumber);
    public Task<Response> TakeOrderById(string phoneNumber);
}