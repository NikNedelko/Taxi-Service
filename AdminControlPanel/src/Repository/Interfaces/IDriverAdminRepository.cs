using DriverTaxiService.Repository.Interfaces;
using Entities.DriverApi.Driver;

namespace AdminControlPanel.Repository.Interfaces;

public interface IDriverAdminRepository : IDriverAccountRepository
{
    public Task<List<DriverDB>> GetAllDriversWithId();
    public Task<DriverDB?> GetDriverById(int id);
    public Task<string> DeleteDriverById(int id);
}