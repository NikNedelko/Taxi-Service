namespace CustomerTaxiService.Repository.Interfaces;

public interface IUserRepository
{
    public string AddNewUser(string name);
    public string RemoveUser(string name);
    public string PermissionToRide(string name);
    public string CheckOfExist(string name);
    public string UpdateUser(string name);
}