namespace Tests.Unit.CustomerTests.TestData;

[TestClass]
public abstract class CleanupMockDatabase
{
    [TestCleanup]
    public async Task Cleanup()
    {
        MockDatabases.CustomerList = GeneralCustomerTestDataAndMethods.DefaultCustomerList;
        MockDatabases.RideList = GeneralCustomerTestDataAndMethods.DefaultRideDbList;
        MockDatabases.DriverList = GeneralCustomerTestDataAndMethods.DefaultDriverList;
    }
}