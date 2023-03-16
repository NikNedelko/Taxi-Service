using Entities.DriverApi.DriverData;
using TaxiService.Repository.DriverRepository.Interfaces;

namespace AdminControlPanel.Repository.Interfaces;

public interface IDriverAdminRepository : IDriverAccountRepository
{
    public Task<List<DriverDb>> GetAllDriversWithId();
    public Task<DriverDb?> GetDriverById(int id);
    public Task<string> DeleteDriverById(int id);
}