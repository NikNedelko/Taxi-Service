namespace TaxiService.Constants.Customer;

public static class CustomerConstants
{
    public const string Ok = "Ok";
    public const string Default = "";

    public const string SomethingWentWrong = "Something went wrong";
    
    public const string UserIsAlreadyExist = "User with this phone number is already exist";
    public const string UserDoesntHavePermissionToRide = "User doesn't have permission for ride";
    public const string UserIsInRide = "Account in ride now";
    public const string UserIsInRideAdditionalText = "Can't delete account while in ride";
    public const string RideAcceptedAdditionalText =
        "Thank you for choseing us. Your ride is accepted. You can find all the information in a letter on your mail";
    public const string RideBlocked = "Sorry + ..."; //inDevelop
    public const string ProblemsWhenTryToTakeUser = "User not found";
    public const string UserNotFound = "User not found";
    public const string UserNotFoundAdditionalText =
        "Phone number in not exist";
    public const string RideNotFound = "Ride not found";
    public const string RideNotFoundAdditionalText = "Sorry, but we don't have orders with this number";
    public const string UserIsAlreadyHaveAOrder = "Sorry, but user already have a order";
    public const string UserIsAlreadyHaveAOrderAdditionalText = "You can't request several ride while you in ride or waiting for a driver";
    public const string NotEnoughMoney = "Not enough money for request ride with this price";
    public const string NotEnoughMoneyForRideClass = "Not enough money for this class of ride";
}