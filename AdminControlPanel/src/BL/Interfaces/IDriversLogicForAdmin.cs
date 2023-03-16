using Entities.DriverApi.DriverData;
using Entities.General;
using TaxiService.BusinessLogic.DriverLogic.Interface;

namespace AdminControlPanel.BL.Interfaces;

public interface IDriversLogicForAdmin : IDriverAccountLogic
{
    public Task<List<DriverDb>> GetAllDrivers();
    public Task<Response> DeleteDriverById(int id);
}