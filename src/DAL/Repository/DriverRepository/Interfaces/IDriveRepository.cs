using Domain.Entities.DriverApi.DriverData;
using Domain.Entities.General.RideData;

namespace DAL.Repository.DriverRepository.Interfaces;

public interface IDriveRepository
{
    public Task<string> StartWork(string phoneNumber);
    public Task<string> EndWork(string phoneNumber);
    public Task<List<RideDb>> GetAllAvailableOrders(DriveClass driveClass);
    public Task<string> TakeOrderById(int phoneNumber, string phoneNumber1);
    public Task<string> EndOrder(string phoneNumber);
}