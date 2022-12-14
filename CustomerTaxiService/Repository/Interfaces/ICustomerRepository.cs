namespace CustomerTaxiService.Repository.Interfaces;

public interface ICustomerRepository
{
    public bool AddNewOrder(string str);
    public bool DeclineOrder(string str);
}