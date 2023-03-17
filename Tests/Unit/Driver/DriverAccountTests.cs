using TaxiService.BusinessLogic.DriverLogic;
using TaxiService.BusinessLogic.DriverLogic.Interface;
using TaxiService.Repository.DriverRepository.MockRepository;

namespace Tests.Unit.Driver;

[TestClass]
public class DriverAccountTests
{
    private readonly IDriverAccountLogic _driverAccountLogic = new DriverAccountLogic(new MockDriverAccountRepository());

    [TestMethod]
    public async Task CreateNotExistedAccount()
    {
        
    }
}