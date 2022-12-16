namespace CustomerTaxiService.Repository.Interfaces;

public interface IUserRepository
{
    public Task<string> AddNewUser(string name);
    public Task<string> RemoveUser(string name);
    public Task<string> PermissionToRide(string name);
    public Task<string> CheckOfExist(string name);
    public Task<string> UpdateUser(string name);
}