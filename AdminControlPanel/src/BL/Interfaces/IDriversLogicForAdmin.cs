using Entities.DriverApi.Driver;
using Entities.General;
using TaxiService.BusinessLogic.Driver.Interface;

namespace AdminControlPanel.BL.Interfaces;

public interface IDriversLogicForAdmin : IDriverAccountLogic
{
    public Task<List<DriverDb>> GetAllDrivers();
    public Task<Response> DeleteDriverById(int id);
}