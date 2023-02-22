using Entities.CustomerApi.CustomerData;
using Entities.DriverApi;
using Entities.General;
using Entities.General.RideData;
using TaxiService.BusinessLogic.Customer;
using TaxiService.Repository.Customer.MockRepository;

namespace Tests.Unit.CustomerTests;

[TestClass]
public sealed class AccountLogicTests
{
    //Temporarily
    private readonly IAccountLogic _accountLogic =
        new AccountLogic(new MockUsersRepository(), new MockRideRepository(new MockUsersRepository()));

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
        var userEntity = await GetUserDbForDatabase();
        MockDatabases.CustomerList.Add(userEntity);
        var deleteResult = await _accountLogic.DeleteAccount(userEntity.PhoneNumber);
        
        Assert.IsNotNull(deleteResult);
        Assert.AreEqual(deleteResult.Message, CustomerConstants.UserWasDeleted);
        Assert.AreEqual(deleteResult.AdditionalInformation, CustomerConstants.Ok);
        
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
        var userEntity = await GetUserDbForDatabase();
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

    [TestMethod]
    public async Task DeleteExistedUser_WhileOnRide()
    {
        var userEntity = await GetUserDbForDatabase();
        var rideEntity = await GetRideDbEntity();
        MockDatabases.RideList.Add(rideEntity);
        var deleteResult = await _accountLogic.DeleteAccount(userEntity.PhoneNumber);
        
        Assert.IsNotNull(deleteResult);
        Assert.IsNotNull(MockDatabases.RideList
            .FirstOrDefault(x => x.CustomerPhoneNumber == rideEntity.CustomerPhoneNumber));
        Assert.AreEqual(deleteResult.Message, CustomerConstants.UserIsInRide);
        Assert.AreEqual(deleteResult.AdditionalInformation, CustomerConstants.UserIsInRideAdditionalText);

        MockDatabases.RideList.Remove(rideEntity);
        Assert.IsNull(MockDatabases.RideList
            .FirstOrDefault(x => x.CustomerPhoneNumber == rideEntity.CustomerPhoneNumber));
    }
    
    [TestMethod]
    public async Task UpdateExistedUser()
    {
        var userEntity = await GetUserDbForDatabase();
        MockDatabases.CustomerList.Add(userEntity);
        var userForUpdate = await GetUserForUpdate();
        
        var updateResult = await _accountLogic.UpdateAccount(userForUpdate);
        Assert.AreEqual(updateResult.Message, CustomerConstants.UserWasUpdated);
        Assert.AreEqual(updateResult.AdditionalInformation, CustomerConstants.UserWasUpdatedAdditionalText);
        
        Assert.IsNull(MockDatabases.CustomerList.FirstOrDefault(x=>x.Name == userEntity.Name 
                                                                   && x.LastName == userEntity.LastName
                                                                   && x.Email == userEntity.Email
                                                                   && x.PhoneNumber == userEntity.PhoneNumber));
        
        Assert.IsNotNull(MockDatabases.CustomerList.FirstOrDefault(x=>x.Name == userForUpdate.Name 
                                                                   && x.LastName == userForUpdate.LastName
                                                                   && x.Email == userForUpdate.Email
                                                                   && x.PhoneNumber == userForUpdate.PhoneNumber));
        
        _ = await _accountLogic.DeleteAccount(userForUpdate.PhoneNumber);
        Assert.IsNull(MockDatabases.CustomerList.FirstOrDefault(x=>x.Name == userForUpdate.Name 
                                                                      && x.LastName == userForUpdate.LastName
                                                                      && x.Email == userForUpdate.Email
                                                                      && x.PhoneNumber == userForUpdate.PhoneNumber));
    }
    
    [TestMethod]
    public async Task UpdateNotExistedUser()
    {
        var userForUpdate = await GetUserForUpdate();
        
        var updateResult = await _accountLogic.UpdateAccount(userForUpdate);
        Assert.AreEqual(updateResult.Message, CustomerConstants.UserNotFound);
        Assert.AreEqual(updateResult.AdditionalInformation, CustomerConstants.UserNotFoundAdditionalText);
        
        Assert.IsNull(MockDatabases.CustomerList.FirstOrDefault(x=>x.Name == userForUpdate.Name 
                                                                   && x.LastName == userForUpdate.LastName
                                                                   && x.Email == userForUpdate.Email
                                                                   && x.PhoneNumber == userForUpdate.PhoneNumber));
        
    }
    
    [TestMethod]
    public async Task AddMoneyToExistedUser()
    {
        var userEntity = await GetUserDbForDatabase();
        MockDatabases.CustomerList.Add(userEntity);

        var addMoneyResult = await _accountLogic.AddMoneyToAccount(userEntity.PhoneNumber, 1000);
        Assert.AreEqual(addMoneyResult.Message, CustomerConstants.MoneyWasAdded);
        Assert.AreEqual(addMoneyResult.AdditionalInformation, CustomerConstants.Default);
        
        Assert.IsNotNull(MockDatabases.CustomerList.FirstOrDefault(x=>x.Name == userEntity.Name 
                                                                   && x.LastName == userEntity.LastName
                                                                   && x.Email == userEntity.Email
                                                                   && x.PhoneNumber == userEntity.PhoneNumber
                                                                   && x.AvailableMoney == userEntity.AvailableMoney + 1000));
        _ = await _accountLogic.DeleteAccount(userEntity.PhoneNumber);
        //add check
    }
    

    private async Task<RegistrationForUser> GetRegistrationAccount() => new RegistrationForUser
    {
        Name = "TestNameForUnitTesting",
        LastName = "TestLastNameForUnitTesting",
        PhoneNumber = "TestPhoneNumberForUnitTesting",
        Email = "TestEmailForUnitTesting"
    };

    private async Task<CustomerDB> GetUserDbForDatabase() => new CustomerDB
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
    
    private async Task<Customer> GetUserForUpdate() => new Customer
    {
        Name = "Updated_TestNameForUnitTesting",
        LastName = "Updated_TestLastNameForUnitTesting",
        PhoneNumber = "TestPhoneNumberForUnitTesting",
        Email = "Updated_TestEmailForUnitTesting",
        FeedBack = FeedBack.Good,
        Status = AccountStatus.Active,
        RegistrationDate = DateTime.Today,
        AvailableMoney = -10
    };

    private async Task<RideDb> GetRideDbEntity() => new RideDb
    {
        Id = -1,
        CustomerPhoneNumber = "TestPhoneNumberForUnitTesting",
        DriveClass = DriveClass.NoData,
        IsTaken = true
    };
}