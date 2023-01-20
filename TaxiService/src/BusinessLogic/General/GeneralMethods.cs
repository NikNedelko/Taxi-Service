using Entities.General;
using TaxiService.Constants.Customer.Account;
using TaxiService.Constants.Customer.General;
using TaxiService.Constants.Customer.OrdersLogic;
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
            UserConstants.UserWasCreated => UserConstants.SuccessfulCreate,
            UserConstants.UserWasDeleted => UserConstants.UserWasUpdatedAdditionalText,
            UserConstants.MoneyWasAdded =>"",
            UserConstants.UserIsInRide => UserConstants.UserIsInRideAdditionalText,
            
            ResponseConstants.ProblemWithUsersEntity => ResponseConstants.ProblemsWhenTryToTakeUser,
            OrdersConstants.UserNotFound => OrdersConstants.UserNotFoundAdditionalText,
            OrdersConstants.RideNotFound => OrdersConstants.RideNotFoundAdditionalText,
            ResponseConstants.RideAccepted => ResponseConstants.RideAcceptedAdditionalText,
            OrdersConstants.UserIsAlreadyHaveAOrder => OrdersConstants.UserIsAlreadyHaveAOrderAdditionalText,
            
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