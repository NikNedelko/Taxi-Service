using System.Reflection.Metadata;
using AdminControlPanel.BL.Interfaces;
using Entities.DriverApi.Driver;
using Entities.General;
using Microsoft.AspNetCore.Mvc;

namespace AdminControlPanel.Controllers;

[ApiController]
public class DriverControlPanel : ControllerBase
{
   private readonly IDriversLogicForAdmin _driversLogicForAdmin;

   public DriverControlPanel(IDriversLogicForAdmin driversLogicForAdmin)
   {
      _driversLogicForAdmin = driversLogicForAdmin;
   }

   [HttpPost("/GetAllDrivers")]
   public async Task<List<DriverDb>> GetAllDrivers()
   {
      return await _driversLogicForAdmin.GetAllDrivers();
   }

   [HttpPost("/DeleteDriverById")]
   public async Task<Response> DeleteDriverById(int id)
   {
      return await _driversLogicForAdmin.DeleteDriverById(id);
   }
   
   [HttpPost("/StopWorkForDriver")]
   public async Task<Response> StopWorkForDriver(int id)
   {
      throw new NotImplementedException();
   }
   
   [HttpPost("/StartWorkForDriver")]
   public async Task<Response> StartWorkForDriver(int id)
   {
      throw new NotImplementedException();
   }
}