using Entities.DriverApi.Driver;
using TaxiService.Repository.Driver.Interfaces;

namespace AdminControlPanel.Repository.Interfaces;

public interface IDriverAdminRepository : IDriverAccountRepository
{
    public Task<List<DriverDb>> GetAllDriversWithId();
    public Task<DriverDb?> GetDriverById(int id);
    public Task<string> DeleteDriverById(int id);
}