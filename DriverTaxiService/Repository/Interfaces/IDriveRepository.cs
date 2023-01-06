namespace DriverTaxiService.Repository.Interfaces;

public interface IDriveRepository
{
    public Task<string> StartWork(string phoneNumber);
    public Task<string> EndWork(string phoneNumber);
    public Task<string> GetAllAvailableOrders(string phoneNumber);
    public Task<string> TakeOrderById(string phoneNumber);
}