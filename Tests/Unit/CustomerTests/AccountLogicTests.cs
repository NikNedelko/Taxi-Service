using Application.BL.Customer;
using Application.BL.General;
using DAL.Interfaces.Customer;
using DAL.Mock.MockDatabase;
using DAL.Mock.MockRepository;
using Domain.Entities.CustomerApi.CustomerData;
using Domain.Entities.CustomerApi.CustomerData.Interface;
using Tests.Unit.Data;

namespace Tests.Unit.CustomerTests;

[TestClass]
public class AccountLogicTests
{
    //Temporarily
    private readonly IAccountLogic _accountLogic =
        new AccountLogic(new MockUsersRepository(), new MockRideRepository(new MockUsersRepository()), new GeneralMethods());

    [TestMethod]
    public async Task CreateUserByRegistration()
    {
        var entityForRegistration = await TestDataAndMethods.GetRegistrationAccount();
        var registrationResult = await _accountLogic.CreateAccount(entityForRegistration);

        Assert.IsNotNull(registrationResult);
        Assert.AreEqual(registrationResult.Message, CustomerConstants.Ok);
        Assert.AreEqual(registrationResult.AdditionalInformation, CustomerConstants.Default);
        
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
        var entityForRegistration = await TestDataAndMethods.GetRegistrationAccount();
        var registrationResult = await _accountLogic.CreateAccount(entityForRegistration);
        
        Assert.IsNotNull(registrationResult);
        Assert.AreEqual(registrationResult.Message, CustomerConstants.Ok);
        Assert.AreEqual(registrationResult.AdditionalInformation, CustomerConstants.Default);

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
        var userEntity = await TestDataAndMethods.GetUserDbForDatabase();
        MockDatabases.CustomerList.Add(userEntity);
        var deleteResult = await _accountLogic.DeleteAccount(userEntity.PhoneNumber);
        Assert.IsNotNull(deleteResult);
        Assert.AreEqual(deleteResult.Message, CustomerConstants.Ok);
        Assert.AreEqual(deleteResult.AdditionalInformation, CustomerConstants.Default);
        Assert.IsNull(await FindUserInDatabase(userEntity));
    }

    [TestMethod]
    public async Task DeleteNotExistedUser()
    {
        var userEntity = await TestDataAndMethods.GetUserDbForDatabase();
        var deleteResult = await _accountLogic.DeleteAccount(userEntity.PhoneNumber);
        
        Assert.IsNotNull(deleteResult);
        Assert.AreEqual(deleteResult.Message, CustomerConstants.UserNotFound);
        Assert.AreEqual(deleteResult.AdditionalInformation, CustomerConstants.UserNotFoundAdditionalText);
        Assert.IsNull(await FindUserInDatabase(userEntity));
    }

    [TestMethod]
    public async Task DeleteExistedUser_WhileOnRide()
    {
        var userEntity = await TestDataAndMethods.GetUserDbForDatabase();
        var rideEntity = await TestDataAndMethods.GetRideDbEntity();
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
        MockDatabases.CustomerList.Remove(userEntity);
    }
    
    [TestMethod]
    public async Task UpdateExistedUser()
    {
        var userEntity = await TestDataAndMethods.GetUserDbForDatabase();
        MockDatabases.CustomerList.Add(userEntity);
        var userForUpdate = await TestDataAndMethods.GetUserForUpdate();
        
        var updateResult = await _accountLogic.UpdateAccount(userForUpdate);
        Assert.AreEqual(updateResult.Message, CustomerConstants.Ok);
        Assert.AreEqual(updateResult.AdditionalInformation, CustomerConstants.Default);
        
        Assert.IsNull(await FindUserInDatabase(userEntity));
        Assert.IsNotNull(await FindUserInDatabase(userForUpdate));
        _ = await _accountLogic.DeleteAccount(userForUpdate.PhoneNumber);
        Assert.IsNull(await FindUserInDatabase(userForUpdate));
    }
    
    [TestMethod]
    public async Task UpdateNotExistedUser()
    {
        var userForUpdate = await TestDataAndMethods.GetUserForUpdate();
        
        var updateResult = await _accountLogic.UpdateAccount(userForUpdate);
        Assert.AreEqual(updateResult.Message, CustomerConstants.UserNotFound);
        Assert.AreEqual(updateResult.AdditionalInformation, CustomerConstants.UserNotFoundAdditionalText);
        
        Assert.IsNull( await FindUserInDatabase(userForUpdate));
    }
    
    [TestMethod]
    public async Task AddMoneyToExistedUser()
    {
        var userEntity = await TestDataAndMethods.GetUserDbForDatabase();
        MockDatabases.CustomerList.Add(userEntity);

        var addMoneyResult = await _accountLogic.AddMoneyToAccount(userEntity.PhoneNumber, 1000);
        Assert.AreEqual(addMoneyResult.Message, CustomerConstants.Ok);
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
        var userEntity = await TestDataAndMethods.GetUserDbForDatabase();
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