using DriverTaxiService.BusinessLogic.Interface;
using Entities.DriverApi.Driver;
using Entities.General;

namespace AdminControlPanel.BL.Interfaces;

public interface IDriversLogicForAdmin : IDriverAccountLogic
{
    public Task<List<DriverDB>> GetAllDrivers();
    public Task<Response> DeleteDriverById(int id);
}