using CustomerTaxiService.Repository.Interfaces;

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
    
    public async Task<string> BeginNewOrder(string str)
    {
        CheckInformationAboutCustomer("number");
        CreateNewOrder("data");
        return "";
    }
    
    private async Task<string> CreateNewOrder(string str)
    {
        _customerRepository.AddNewOrder(str);
        
        return "sdads";
    }

    private async Task<string> CheckInformationAboutCustomer(string phoneNumber)
    {
        _userRepository.PermissionToRide(phoneNumber);
        return "";
    }

}