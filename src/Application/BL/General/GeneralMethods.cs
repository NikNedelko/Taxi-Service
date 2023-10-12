using Domain.Entities.General;
using TaxiService.Constants.Customer;
using TaxiService.Constants.DriverConstants;

namespace Application.BL.General;

public class GeneralMethods
{
    public async Task<Response> CreateResponse(string message)
        => new Response
        {
            Message = message,
            IsSuccess = message == CustomerConstants.Ok,
            AdditionalInformation = await TakeAdditionalInfoByMessage(message)
        };

    private Task<string> TakeAdditionalInfoByMessage(string message)
    {
        return Task.FromResult(message switch
        {
            CustomerConstants.UserIsInRide => CustomerConstants.UserIsInRideAdditionalText,
            
            CustomerConstants.UserNotFound => CustomerConstants.UserNotFoundAdditionalText,
            CustomerConstants.RideNotFound => CustomerConstants.RideNotFoundAdditionalText,
            CustomerConstants.UserIsAlreadyHaveAOrder => CustomerConstants.UserIsAlreadyHaveAOrderAdditionalText,
            
            DriverConstants.OrderByIdIsNotExist => DriverConstants.OrderByIdIsNotExistAdditionalText,
            DriverConstants.OrderByNumberIsNotExist => DriverConstants.OrderByNumberIsNotExistAdditionalText,
            
            DriverConstants.DriverIsExist => DriverConstants.DriverIsExistAdditionalInfo,
            DriverConstants.DriverWasAdded => DriverConstants.DriverWasAddedAdditionalInfo,
            DriverConstants.DriverIsNotExist => DriverConstants.DriverIsNotExistAdditionalInfo,
            DriverConstants.DriverWasDeleted => DriverConstants.DriverWasDeletedAdditionalInfo
            ,
            _ => CustomerConstants.Default
        });
    }
}