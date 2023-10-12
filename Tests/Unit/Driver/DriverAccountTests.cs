
using Application.BL.DriverLogic;
using Application.BL.DriverLogic.Interface;
using Application.BL.General;
using DAL.Repository.DriverRepository.MockRepository;

namespace Tests.Unit.Driver;

[TestClass]
public class DriverAccountTests
{
    private readonly IDriverAccountLogic _driverAccountLogic = new DriverAccountLogic(new MockDriverAccountRepository(), new GeneralMethods());

    [TestMethod]
    public async Task CreateNotExistedAccount()
    {
        
    }
}