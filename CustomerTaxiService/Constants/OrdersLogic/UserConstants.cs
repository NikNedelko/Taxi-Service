namespace CustomerTaxiService.Constants;

public static class UserConstants
{
    public const string Ok = "Ok";
    public const string DatabaseProblem = "DatabaseProblem";
    public const string UserIsAlreadyExist = "User with this phone number is already exist";
    public const string UserNotFound = "User with this phone number is not exist";
    public const string ErrorWhileTryToGetUser = "Error while try to get user from database";
    public const string UserDoesntHavePermissionToRide = "This user doesn't have permission to ride";
}