using Domain.Entities.CustomerApi.CustomerData;
using Domain.Entities.DriverApi.DriverData;
using Domain.Entities.General;
using Domain.Entities.General.RideData;

namespace DAL.Mock.MockDatabase;

public static class MockDatabases
{
    public static List<RideDb> RideList = new()
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
    
    public static List<CustomerDB> CustomerList = new()
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
    
    public static List<DriverDb> DriverList = new()
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
}