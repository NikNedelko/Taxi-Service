using CustomerTaxiService.Constants;
using CustomerTaxiService.Repository.Interfaces;
using Entities.CustomerTaxiService.Requests;
using Entities.CustomerTaxiService.Response;

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
    
    public async Task<Response> BeginNewOrder(Order order)
    {
        var checkCustomerResult =  await CheckInformationAboutCustomer(order.PhoneNumber);
        if (checkCustomerResult != CheckInformationConstants.Accepted)
            return CreateResponse(checkCustomerResult, null);
        var newOrderResponse = await CreateNewOrder("data");
        return  CreateResponse(newOrderResponse,null);
    }
    
    private async Task<string> CreateNewOrder(string str)
    {
        //maybe check drivers
        var dbResponse = await _customerRepository.AddNewOrder(str);
        if (dbResponse != CreateNewOrderConstants.Ok)
        {
            //return one of bad results
        }
        return CreateNewOrderConstants.Ok;
    }
    
    public async Task<string> CancelOrder(string str)
    {
        await _customerRepository.CancelOrder(str);
        
        return "sdads";
    }

    private async Task<string> CheckInformationAboutCustomer(string phoneNumber)
    {
        _userRepository.PermissionToRide(phoneNumber);
        return "";
    }

    private Response CreateResponse(string message, string? additionalInformation)
        => new Response
        {
            Message = message,
            AdditionalInformation = additionalInformation ?? ""
        };
    

}