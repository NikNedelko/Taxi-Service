using Entities.CustomerApi.CustomerData;
using TaxiService.BusinessLogic.Customer;
using TaxiService.Repository.Customer.MockRepository;

namespace Tests.Unit.Customer;

[TestClass]
public sealed class AccountLogicTests
{
    //Temporarily
    private readonly IAccountLogic _accountLogic = 
        new AccountLogic(new MockUsersRepository(),new MockRideRepository(new MockUsersRepository()));

    [TestMethod]
    public async Task CreateUserByRegistration()
    {
        var entityForRegistration = await GetRegistrationAccount();
        var registrationResult = await _accountLogic.CreateAccount(entityForRegistration);
        Assert.IsNotNull(registrationResult);
        Assert.AreEqual(registrationResult.Message, CustomerConstants.UserWasCreated);
        Assert.AreEqual(registrationResult.AdditionalInformation, CustomerConstants.SuccessfulCreate);
        var userInDb = MockDatabases.CustomerList
            .FirstOrDefault(x => x.Name == entityForRegistration.Name
                                 && x.LastName == entityForRegistration.LastName
                                 && x.PhoneNumber == entityForRegistration.PhoneNumber
                                 && x.Email == entityForRegistration.Email);
        Assert.IsNotNull(userInDb);
        MockDatabases.CustomerList.Remove(userInDb);
        Assert.IsNull(MockDatabases.CustomerList
            .FirstOrDefault(x => x.Name == entityForRegistration.Name
                                 && x.LastName == entityForRegistration.LastName
                                 && x.PhoneNumber == entityForRegistration.PhoneNumber
                                 && x.Email == entityForRegistration.Email));
    }

    

    private async Task<RegistrationForUser> GetRegistrationAccount() => new RegistrationForUser
    {
        Name = "TestNameForUnitTesting",
        LastName = "TestLastNameForUnitTesting",
        PhoneNumber = "TestPhoneNumberForUnitTesting",
        Email = "TestEmailForUnitTesting"
    };

    private async Task<CustomerDB> GetUserForDatabase() => new CustomerDB
    {
        Id = -1,
        Name = "TestNameForUnitTesting",
        LastName = "TestLastNameForUnitTesting",
        PhoneNumber = "TestPhoneNumberForUnitTesting",
        Email = "TestEmailForUnitTesting",
        FeedBack = 0,
        Status = 0,
        RegistrationDate = DateTime.Now,
        AvailableMoney = -1
    };
}