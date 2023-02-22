using Entities.General;
using TaxiService.Constants.Customer;
using TaxiService.Constants.Driver;

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
            CustomerConstants.UserWasDeleted => CustomerConstants.Ok,
            CustomerConstants.UserWasUpdated => CustomerConstants.UserWasUpdatedAdditionalText,
            CustomerConstants.MoneyWasAdded => "",
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
            _ => CustomerConstants.SomethingWentWrong
        };
    }
}