using CustomerTaxiService.Repository.Interfaces;

namespace CustomerTaxiService.Repository.MockRepository;

public class MockUsersRepository : IUserRepository
{
    private List<string> MockRepository = new();

    public async Task<string> AddNewUser(string name)
    {
        try
        {
            MockRepository.Add(name);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return "";
    }

    public async Task<string> RemoveUser(string name)
    {
        try
        {
            MockRepository.Add(name);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return "";
    }

    public async Task<string> PermissionToRide(string name)
    {
        try
        {
            MockRepository.Add(name);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return "";
    }

    public async Task<string> CheckOfExist(string name)
    {
        try
        {
            MockRepository.Add(name);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return "";
    }

    public async Task<string> UpdateUser(string name)
    {
        try
        {
            MockRepository.Add(name);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return "";
    }
}