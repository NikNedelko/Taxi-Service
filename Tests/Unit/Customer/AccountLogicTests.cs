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

    [TestMethod]
    public async Task CreateUserByRegistration_ExistedNumber()
    {
        //First registration
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
        //Second registration
        var secondRegistrationResult = await _accountLogic.CreateAccount(entityForRegistration);
        Assert.IsNotNull(secondRegistrationResult);
        Assert.AreEqual(secondRegistrationResult.Message, CustomerConstants.UserIsAlreadyExist);
        Assert.AreEqual(secondRegistrationResult.AdditionalInformation, CustomerConstants.SomethingWentWrong);
        Assert.IsNotNull(userInDb);
        MockDatabases.CustomerList.Remove(userInDb);
        Assert.IsNull(MockDatabases.CustomerList
            .FirstOrDefault(x => x.Name == entityForRegistration.Name
                                 && x.LastName == entityForRegistration.LastName
                                 && x.PhoneNumber == entityForRegistration.PhoneNumber
                                 && x.Email == entityForRegistration.Email));
    }

    [TestMethod]
    public async Task DeleteExistedUser()
    {
        var userEntity = await GetUserForDatabase();
        MockDatabases.CustomerList.Add(userEntity);
        var deleteResult = await _accountLogic.DeleteAccount(userEntity.PhoneNumber);
        Assert.IsNotNull(deleteResult);
        Assert.AreEqual(deleteResult.Message, CustomerConstants.UserWasDeleted);
        Assert.AreEqual(deleteResult.AdditionalInformation, CustomerConstants.UserWasUpdatedAdditionalText);
        var userInDb = MockDatabases.CustomerList
            .FirstOrDefault(x => x.Name == userEntity.Name
                                 && x.LastName == userEntity.LastName
                                 && x.PhoneNumber == userEntity.PhoneNumber
                                 && x.Email == userEntity.Email);
        Assert.IsNull(userInDb);
    }
    
    [TestMethod]
    public async Task DeleteNotExistedUser()
    {
        var userEntity = await GetUserForDatabase();
        var deleteResult = await _accountLogic.DeleteAccount(userEntity.PhoneNumber);
        Assert.IsNotNull(deleteResult);
        Assert.AreEqual(deleteResult.Message, CustomerConstants.UserNotFound);
        Assert.AreEqual(deleteResult.AdditionalInformation, CustomerConstants.UserNotFoundAdditionalText);
        Assert.IsNull(MockDatabases.CustomerList
            .FirstOrDefault(x => x.Name == userEntity.Name
                                 && x.LastName == userEntity.LastName
                                 && x.PhoneNumber == userEntity.PhoneNumber
                                 && x.Email == userEntity.Email));
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