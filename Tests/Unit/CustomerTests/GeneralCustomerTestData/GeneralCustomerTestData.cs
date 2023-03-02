using System.Runtime.CompilerServices;
using Entities.CustomerApi.CustomerData;
using Entities.DriverApi;
using Entities.General;
using Entities.General.RideData;
using Tests.Unit.Constants;

namespace Tests.Unit.CustomerTests.TestData;

public static class GeneralCustomerTestDataAndMethods
{
    public static async Task<RegistrationForUser> GetRegistrationAccount() => new RegistrationForUser
    {
        Name = CustomerTestsConstants.Registration_Name,
        LastName = CustomerTestsConstants.Registration_LastName,
        PhoneNumber = CustomerTestsConstants.Registration_Phonenumber,
        Email = CustomerTestsConstants.Registration_Email
    };

    public async static Task<List<RideDb>> GetRandomCountOfRides()
    {
        var list = new List<RideDb>();
        for (int i = 0; i < new Random().Next(1,10); i++)
            list.Add(new RideDb());
        return list;
    }

    public static async Task<CustomerDB> GetUserDbForDatabase() => new CustomerDB
    {
        Id = -1,
        Name = CustomerTestsConstants.UserDb_Name,
        LastName = CustomerTestsConstants.UserDb_LastName,
        PhoneNumber = CustomerTestsConstants.UserDb_Phonenumber,
        Email = CustomerTestsConstants.UserDb_Email,
        FeedBack = 0,
        Status = 0,
        RegistrationDate = DateTime.Now,
        AvailableMoney = -1
    };
    
    public static async Task<Customer> GetUserForUpdate() => new Customer
    {
        Name = CustomerTestsConstants.Update_Name,
        LastName = CustomerTestsConstants.Update_LastName,
        PhoneNumber = CustomerTestsConstants.Update_Phonenumber,
        Email = CustomerTestsConstants.Update_Email,
        FeedBack = FeedBack.Good,
        Status = AccountStatus.Active,
        RegistrationDate = DateTime.Today,
        AvailableMoney = -10
    };

    public static async Task<RideDb> GetRideDbEntity() => new RideDb
    {
        Id = -1,
        CustomerPhoneNumber = CustomerTestsConstants.UserDb_Email,
        DriveClass = DriveClass.NoData,
        IsTaken = true
    };
    
    public static async Task<Order> GetNewOrder() => new Order
    {
        PhoneNumber = CustomerTestsConstants.UserDb_Phonenumber,
        RideEndPoint = "EndPlace",
        Price = 30,
        DriveClass = DriveClass.Economic
    };

    public static async Task<RideDb?> GetRideDbByUser(string phoneNumber, decimal price)
        => MockDatabases.RideList.FirstOrDefault(x => x.CustomerPhoneNumber == phoneNumber 
                                                      && x.Price == price);
}