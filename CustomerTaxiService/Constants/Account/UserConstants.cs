namespace CustomerTaxiService.Constants;

public static class UserConstants
{
    public const string Ok = "Ok";

    public const string SuccessfulCreate =
        "Thank you for registration. Now you need to add money to account. After it you can request a taxi ride in our service";

    public const string DatabaseProblem = "DatabaseProblem";
    public const string DatabaseProblemResponse = "Sorry, but now you can't create account. Please try later";
    public const string UserIsAlreadyExist = "User with this phone number is already exist";
    public const string UserNotFound = "User with this phone number is not exist";
    public const string ErrorWhileTryToGetUser = "Error while try to get user from database";
    public const string UserDoesntHavePermissionToRide = "This user doesn't have permission to ride";

    public const string UserWasDeleted = "User was successfuly deleated";
    public const string MoneyWasAdded = "Money was successfuly added to your account";
    public const string UserWasCreated = "User was successfuly created";
    public const string UserWasUpdated = "User was successfuly updated";
    
    public const string UserWasUpdatedAdditionalText = "We hope to see you again!";
}