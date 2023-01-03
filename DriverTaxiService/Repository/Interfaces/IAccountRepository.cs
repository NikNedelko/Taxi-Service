namespace DriverTaxiService.Repository.Interfaces;

public interface IAccountRepository
{
    public Task<string> AddNewDriver();
    public Task<string> UpdateDriver();
    public Task<string> DeleteDriver();
    public Task<string> GetAllDrivers();
}