namespace CustomerTaxiService.Constants.Account;

public static class UserConstants
{
    public const string Ok = "Ok";

    public const string SuccessfulCreate =
        "Thank you for registration. Now you need to add money to account. After it you can request a taxi ride in our service";
    
    public const string UserIsAlreadyExist = "User with this phone number is already exist";
    public const string UserNotFound = "User with this phone number is not exist";
    public const string UserDoesntHavePermissionToRide = "This user doesn't have permission to ride";

    public const string UserWasDeleted = "User was successfuly deleated";
    public const string MoneyWasAdded = "Money was successfuly added to your account";
    public const string UserWasCreated = "User was successfuly created";
    public const string UserWasUpdated = "User was successfuly updated";
    
    public const string UserIsInRide = "This account in ride now";
    public const string UserIsInRideAdditionalText = "You can't delete your account while you in ride";

    public const string UserWasUpdatedAdditionalText = "We hope to see you again!";
}