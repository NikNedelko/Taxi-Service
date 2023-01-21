using Entities.General;
using TaxiService.Constants.Customer;
using TaxiService.Constants.Driver.AccountConstants;
using TaxiService.Constants.Driver.DriverConstants;

namespace TaxiService.BusinessLogic.General;

public static class GeneralMethods
{
    public static async Task<Response> CreateResponse(string message)
        => new Response
        {
            Message = message,
            AdditionalInformation = await TakeAdditionalInfoByMessage(message) ?? ""
        };

    private static async Task<string?> TakeAdditionalInfoByMessage(string message)
    {
        return message switch
        {
            CustomerConstants.UserWasCreated => CustomerConstants.SuccessfulCreate,
            CustomerConstants.UserWasDeleted => CustomerConstants.UserWasUpdatedAdditionalText,
            CustomerConstants.MoneyWasAdded =>"",
            CustomerConstants.UserIsInRide => CustomerConstants.UserIsInRideAdditionalText,
            
            CustomerConstants.ProblemWithUsersEntity => CustomerConstants.ProblemsWhenTryToTakeUser,
            CustomerConstants.UserNotFound => CustomerConstants.UserNotFoundAdditionalText,
            CustomerConstants.RideNotFound => CustomerConstants.RideNotFoundAdditionalText,
            CustomerConstants.RideAccepted => CustomerConstants.RideAcceptedAdditionalText,
            CustomerConstants.UserIsAlreadyHaveAOrder => CustomerConstants.UserIsAlreadyHaveAOrderAdditionalText,
            
            DriverConstants.OrderByIdIsNotExist => DriverConstants.OrderByIdIsNotExistAdditionalText,
            DriverConstants.OrderByNumberIsNotExist => DriverConstants.OrderByNumberIsNotExistAdditionalText,
            
            AccountConstants.DriverIsExist => AccountConstants.DriverIsExistAdditionalInfo,
            AccountConstants.DriverWasAdded => AccountConstants.DriverWasAddedAdditionalInfo,
            AccountConstants.DriverIsNotExist => AccountConstants.DriverIsNotExistAdditionalInfo,
            AccountConstants.DriverWasDeleted => AccountConstants.DriverWasDeletedAdditionalInfo
            ,
            _ => "Something went wrong"
        };
    }
}