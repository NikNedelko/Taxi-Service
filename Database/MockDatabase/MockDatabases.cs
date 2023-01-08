using Entities.General;
using Entities.General.RideData;

namespace Database.MockDatabase;

public static class MockRideDatabase
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
}