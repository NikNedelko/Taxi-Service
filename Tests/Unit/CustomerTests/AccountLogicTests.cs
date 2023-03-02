using Entities.CustomerApi.CustomerData;
using Entities.CustomerApi.CustomerData.Interface;
using TaxiService.BusinessLogic.Customer;
using TaxiService.Repository.Customer.MockRepository;
using Tests.Unit.CustomerTests.TestData;

namespace Tests.Unit.CustomerTests;

[TestClass]
public class AccountLogicTests : CleanupMockDatabase
{
    //Temporarily
    private readonly IAccountLogic _accountLogic =
        new AccountLogic(new MockUsersRepository(), new MockRideRepository(new MockUsersRepository()));

    [TestMethod]
    public async Task CreateUserByRegistration()
    {
        var entityForRegistration = await GeneralCustomerTestDataAndMethods.GetRegistrationAccount();
        var registrationResult = await _accountLogic.CreateAccount(entityForRegistration);

        Assert.IsNotNull(registrationResult);
        Assert.AreEqual(registrationResult.Message, CustomerConstants.UserWasCreated);
        Assert.AreEqual(registrationResult.AdditionalInformation, CustomerConstants.SuccessfulCreate);
        
        Assert.IsNotNull(await FindUserInDatabase(entityForRegistration));
        MockDatabases.CustomerList.Remove( MockDatabases.CustomerList
            .FirstOrDefault(x => x.Name == entityForRegistration.Name
                                 && x.LastName == entityForRegistration.LastName
                                 && x.PhoneNumber == entityForRegistration.PhoneNumber
                                 && x.Email == entityForRegistration.Email)!);
        Assert.IsNull(await FindUserInDatabase(entityForRegistration));
    }

    [TestMethod]
    public async Task CreateUserByRegistration_ExistedNumber()
    {
        //First registration
        var entityForRegistration = await GeneralCustomerTestDataAndMethods.GetRegistrationAccount();
        var registrationResult = await _accountLogic.CreateAccount(entityForRegistration);
        
        Assert.IsNotNull(registrationResult);
        Assert.AreEqual(registrationResult.Message, CustomerConstants.UserWasCreated);
        Assert.AreEqual(registrationResult.AdditionalInformation, CustomerConstants.SuccessfulCreate);

        var userInDb = MockDatabases.CustomerList
            .FirstOrDefault(x => x.Name == entityForRegistration.Name
                                 && x.LastName == entityForRegistration.LastName
                                 && x.PhoneNumber == entityForRegistration.PhoneNumber
                                 && x.Email == entityForRegistration.Email);
        Assert.IsNotNull(await FindUserInDatabase(entityForRegistration));
        
        //Second registration
        var secondRegistrationResult = await _accountLogic.CreateAccount(entityForRegistration);
        Assert.IsNotNull(secondRegistrationResult);
        Assert.AreEqual(secondRegistrationResult.Message, CustomerConstants.UserIsAlreadyExist);
        Assert.AreEqual(secondRegistrationResult.AdditionalInformation, CustomerConstants.Default);
        Assert.IsNotNull(userInDb);
        MockDatabases.CustomerList.Remove(userInDb);
        Assert.IsNull(await FindUserInDatabase(entityForRegistration));
    }

    [TestMethod]
    public async Task DeleteExistedUser()
    {
        var userEntity = await GeneralCustomerTestDataAndMethods.GetUserDbForDatabase();
        MockDatabases.CustomerList.Add(userEntity);
        var deleteResult = await _accountLogic.DeleteAccount(userEntity.PhoneNumber);
        Assert.IsNotNull(deleteResult);
        Assert.AreEqual(deleteResult.Message, CustomerConstants.UserWasDeleted);
        Assert.AreEqual(deleteResult.AdditionalInformation, CustomerConstants.Default);
        Assert.IsNull(await FindUserInDatabase(userEntity));
    }

    [TestMethod]
    public async Task DeleteNotExistedUser()
    {
        var userEntity = await GeneralCustomerTestDataAndMethods.GetUserDbForDatabase();
        var deleteResult = await _accountLogic.DeleteAccount(userEntity.PhoneNumber);
        
        Assert.IsNotNull(deleteResult);
        Assert.AreEqual(deleteResult.Message, CustomerConstants.UserNotFound);
        Assert.AreEqual(deleteResult.AdditionalInformation, CustomerConstants.UserNotFoundAdditionalText);
        Assert.IsNull(await FindUserInDatabase(userEntity));
    }

    [TestMethod]
    public async Task DeleteExistedUser_WhileOnRide()
    {
        var userEntity = await GeneralCustomerTestDataAndMethods.GetUserDbForDatabase();
        var rideEntity = await GeneralCustomerTestDataAndMethods.GetRideDbEntity();
        rideEntity.IsTaken = true;
        MockDatabases.CustomerList.Add(userEntity);
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
        var userEntity = await GeneralCustomerTestDataAndMethods.GetUserDbForDatabase();
        MockDatabases.CustomerList.Add(userEntity);
        var userForUpdate = await GeneralCustomerTestDataAndMethods.GetUserForUpdate();
        
        var updateResult = await _accountLogic.UpdateAccount(userForUpdate);
        Assert.AreEqual(updateResult.Message, CustomerConstants.UserWasUpdated);
        Assert.AreEqual(updateResult.AdditionalInformation, CustomerConstants.UserWasUpdatedAdditionalText);
        
        Assert.IsNull(await FindUserInDatabase(userEntity));
        Assert.IsNotNull(await FindUserInDatabase(userForUpdate));
        _ = await _accountLogic.DeleteAccount(userForUpdate.PhoneNumber);
        Assert.IsNull(await FindUserInDatabase(userForUpdate));
    }
    
    [TestMethod]
    public async Task UpdateNotExistedUser()
    {
        var userForUpdate = await GeneralCustomerTestDataAndMethods.GetUserForUpdate();
        
        var updateResult = await _accountLogic.UpdateAccount(userForUpdate);
        Assert.AreEqual(updateResult.Message, CustomerConstants.UserNotFound);
        Assert.AreEqual(updateResult.AdditionalInformation, CustomerConstants.UserNotFoundAdditionalText);
        
        Assert.IsNull( await FindUserInDatabase(userForUpdate));
    }
    
    [TestMethod]
    public async Task AddMoneyToExistedUser()
    {
        var userEntity = await GeneralCustomerTestDataAndMethods.GetUserDbForDatabase();
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
        Assert.IsNull( await FindUserInDatabase(userEntity));
    }
    
    [TestMethod]
    public async Task AddMoneyToNotExistedUser()
    {
        var userEntity = await GeneralCustomerTestDataAndMethods.GetUserDbForDatabase();
        var addMoneyResult = await _accountLogic.AddMoneyToAccount(userEntity.PhoneNumber, 1000);
        Assert.AreEqual(addMoneyResult.Message, CustomerConstants.UserNotFound);
        Assert.AreEqual(addMoneyResult.AdditionalInformation, CustomerConstants.UserNotFoundAdditionalText);
    }

    private async Task<CustomerDB?> FindUserInDatabase(ICustomerBase customer) => MockDatabases.CustomerList
        .FirstOrDefault(x => x.Name == customer.Name
                             && x.LastName == customer.LastName
                             && x.PhoneNumber == customer.PhoneNumber
                             && x.Email == customer.Email);

    
}