using CustomerTaxiService.Repository.Interfaces;
using Entities.CustomerTaxiService.Requests;

namespace CustomerTaxiService.BusinessLogic;

public class CreateOrderLogic
{
    private readonly IUserRepository _userRepository;
    private readonly ICustomerRepository _customerRepository;
    
    public CreateOrderLogic(IUserRepository userRepository, ICustomerRepository customerRepository)
    {
        _userRepository = userRepository;
        _customerRepository = customerRepository;
    }
    
    public async Task<string> BeginNewOrder(Order order)
    {
        var someResult =  await CheckInformationAboutCustomer(order.PhoneNumber);
        _ = CreateNewOrder("data");
        return "";
    }
    
    private async Task<string> CreateNewOrder(string str)
    {
        await _customerRepository.AddNewOrder(str);
        
        return "sdads";
    }
    
    public async Task<string> CancelOrder(string str)
    {
        await _customerRepository.(str);
        
        return "sdads";
    }

    private async Task<string> CheckInformationAboutCustomer(string phoneNumber)
    {
        _userRepository.PermissionToRide(phoneNumber);
        return "";
    }

}