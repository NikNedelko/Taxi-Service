using AdminControlPanel.BL.Interfaces;
using AdminControlPanel.Repository.Interfaces;
using Entities.DriverApi.Driver;
using Entities.General;
using TaxiService.BusinessLogic.Driver.Interface;

namespace AdminControlPanel.BL;

public class DriversControlLogic : IDriversLogicForAdmin
{
    private readonly IDriverAdminRepository _driverAdminRepository;

    public DriversControlLogic(IDriverAdminRepository driverAdminRepository)
    {
        _driverAdminRepository = driverAdminRepository;
    }

    public async Task<List<DriverDb>> GetAllDrivers()
    {
        return await _driverAdminRepository.GetAllDriversWithId();
    }

    public async Task<Response> DeleteDriverById(int id)
    {
        var checkIsExist = await CheckIsDriverExist(id);
        if (checkIsExist != "User is exist")
            return await CreateResponse(checkIsExist);

        return await CreateResponse(await _driverAdminRepository.DeleteDriverById(id));
    }

    private async Task<string> CheckIsDriverExist(int id)
    {
        var entity = await _driverAdminRepository.GetDriverById(id);
        return entity == null ? "User not found" : "User is exist";
    }

    private async Task<Response> CreateResponse(string message)
        => new Response
        {
            Message = message,
            AdditionalInformation = await TakeAdditionalInfoByMessage(message) ?? ""
        };

    private async Task<string?> TakeAdditionalInfoByMessage(string message)
    {
        return message switch
        {
            _ => ""
        };
    }


    Task<Response> IDriverAccountLogic.AddNewDriver(RegistrationForDriver registrationDriver)
    {
        throw new NotImplementedException();
    }

    Task<Response> IDriverAccountLogic.DeleteDriver(string phoneNumber)
    {
        throw new NotImplementedException();
    }
}