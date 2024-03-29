using DAL.Mock.MockDatabase;
using Domain.Entities.CustomerData;
using Domain.Entities.CustomerData.Requests;
using Domain.Entities.DriveData;
using Domain.Entities.DriverData;
using Domain.Entities.General;
using Domain.Entities.RideData;

namespace Tests.Unit.Data;

public static class TestDataAndMethods
{
    public static readonly RegistrationForDriver GetRegistrationForDriver = new RegistrationForDriver
    {
        Name = DriverTestConstants.DriverTestName,
        LastName = DriverTestConstants.DriverTestLastName,
        PhoneNumber = DriverTestConstants.DriverTestPhoneNumber,
        DriverLicenseNumber = DriverTestConstants.DriverTestDriverLicenseNumber,
        Car = nameof(DriverTestConstants.DriverTestCar)
    };
    
    public static readonly List<RideDb> DefaultRideDbList = new List<RideDb>()
    {
        new RideDb
        {
            Id = 1,
            DriverPhoneNumber = "1234",
            CustomerPhoneNumber = "1234",
            IsTaken = false,
            IsEnd = false,
            EndPointOfRide = "Heaven",
            RideDate = DateTime.Today,
            DriverFeedBack = (int)FeedBack.Normal,
            CustomerFeedBack = (int)FeedBack.Normal
        }
    };
    
    public static readonly List<CustomerDB> DefaultCustomerList = new()
    {
        new CustomerDB
        {
            Id = 1,
            Name = "Name",
            LastName = "LastName",
            PhoneNumber = "12345",
            FeedBack = (int)FeedBack.Good,
            Status = 1,
            RegistrationDate = DateTime.Now,
            AvailableMoney = 100
        }
    };
    
    public static readonly List<DriverDb> DefaultDriverList = new()
    {
        new DriverDb
        {
            Id = 0,
            Name = "Jacob",
            LastName = "Sally",
            PhoneNumber = "12345",
            DriverLicenseNumber = "EU12345",
            Car = "Ford",
            IsWorking = false,
            DriveClass = 1,
            Status = 2,
            FeedBack = 0,
            RegistrationDate = DateTime.Now,
            Balance = 1000
        }
    };

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
    
    public static async Task<CustomerModel> GetUserForUpdate() => new CustomerModel
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
        CustomerPhoneNumber = CustomerTestsConstants.UserDb_Phonenumber,
        DriveClass = DriveClass.NoData,
        IsTaken = true
    };
    
    public static async Task<OrderModel> GetNewOrder() => new OrderModel
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