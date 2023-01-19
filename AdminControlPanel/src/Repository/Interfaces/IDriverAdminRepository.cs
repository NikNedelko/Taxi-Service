using Entities.DriverApi.Driver;
using TaxiService.Repository.Driver.Interfaces;

namespace AdminControlPanel.Repository.Interfaces;

public interface IDriverAdminRepository : IDriverAccountRepository
{
    public Task<List<DriverDB>> GetAllDriversWithId();
    public Task<DriverDB?> GetDriverById(int id);
    public Task<string> DeleteDriverById(int id);
}