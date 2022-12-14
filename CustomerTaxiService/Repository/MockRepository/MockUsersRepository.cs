using CustomerTaxiService.Repository.Interfaces;

namespace CustomerTaxiService.Repository.MockRepository;

public class MockUsersRepository : IUserRepository
{
    public string AddNewUser(string name)
    {
        throw new NotImplementedException();
    }

    public string RemoveUser(string name)
    {
        throw new NotImplementedException();
    }

    public string PermissionToRide(string name)
    {
        throw new NotImplementedException();
    }

    public string CheckOfExist(string name)
    {
        throw new NotImplementedException();
    }

    public string UpdateUser(string name)
    {
        throw new NotImplementedException();
    }
}