
using Application.BL.DriverLogic;
using Application.BL.General;
using DAL.Interfaces.Driver;
using DAL.Mock.MockRepository;

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