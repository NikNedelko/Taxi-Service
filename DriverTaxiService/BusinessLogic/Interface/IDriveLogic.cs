namespace DriverTaxiService.BusinessLogic.Interface;

public interface IDriveLogic
{
    public Task<string> StartWork();
    public Task<string> EndWork();
}