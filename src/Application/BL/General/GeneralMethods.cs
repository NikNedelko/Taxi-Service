using Domain.Entities.General;
using TaxiService.Constants.Customer;
using TaxiService.Constants.DriverConstants;

namespace Application.BL.General;

public abstract class GeneralMethods
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
            CustomerConstants.UserWasCreated => CustomerConstants.SuccessfulCreate,
            CustomerConstants.UserWasDeleted => CustomerConstants.Default,
            CustomerConstants.UserWasUpdated => CustomerConstants.UserWasUpdatedAdditionalText,
            CustomerConstants.MoneyWasAdded => CustomerConstants.Default,
            CustomerConstants.UserIsInRide => CustomerConstants.UserIsInRideAdditionalText,
            
            CustomerConstants.ProblemWithUsersEntity => CustomerConstants.ProblemsWhenTryToTakeUser,
            CustomerConstants.UserNotFound => CustomerConstants.UserNotFoundAdditionalText,
            CustomerConstants.RideNotFound => CustomerConstants.RideNotFoundAdditionalText,
            CustomerConstants.RideAccepted => CustomerConstants.RideAcceptedAdditionalText,
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