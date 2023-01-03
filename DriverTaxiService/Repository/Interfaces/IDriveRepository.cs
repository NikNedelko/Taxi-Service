namespace DriverTaxiService.Repository.Interfaces;

public interface IDriveRepository
{
    public Task<string> StartWork();
    public Task<string> EndWork();
}