namespace TaxiService.Constants.Customer;

public static class CustomerConstants
{
    public const string Ok = "Ok";
    public const string SomethingWentWrong = "Something went wrong";
    public const string SuccessfulCreate =
        "Thank you for registration. Now you need to add money to account. After it you can request a taxi ride in our service";
    public const string UserIsAlreadyExist = "User with this phone number is already exist";
    public const string UserDoesntHavePermissionToRide = "This user doesn't have permission to ride";
    public const string UserWasDeleted = "User was successfuly deleated";
    public const string MoneyWasAdded = "Money was successfuly added to your account";
    public const string UserWasCreated = "User was successfuly created";
    public const string UserWasUpdated = "User was successfuly updated";
    public const string UserIsInRide = "This account in ride now";
    public const string UserIsInRideAdditionalText = "You can't delete your account while you in ride";
    public const string UserWasUpdatedAdditionalText = "We hope to see you again!";
    public const string RideAccepted = "Succesful";
    public const string RideAcceptedAdditionalText =
        "Thank you for choseing us. Your ride is accepted. You can find all the information in a letter on your mail";
    public const string RideBlocked = "Sorry + ..."; //inDevelop
    public const string ProblemWithUsersEntity = "Problems with getting a customer profile";
    public const string ProblemsWhenTryToTakeUser = "Sorry, but now we can't find your profile. Try it later";
    public const string UserNotFound = "User not found";
    public const string UserNotFoundAdditionalText =
        "Sorry, but customer with this phone number not exist. Please do registration before use uor service ";
    public const string RideNotFound = "Ride not found";
    public const string RideNotFoundAdditionalText = "Sorry, but we don't have orders with this number";
    public const string UserIsAlreadyHaveAOrder = "Sorry, but user already have a order";
    public const string UserIsAlreadyHaveAOrderAdditionalText = "You can't request several ride while you in ride or waiting for a driver";
    public const string NotEnoughMoney = "Not enough money for request ride with this price";
    public const string NotEnoughMoneyForRideClass = "Not enough money for this class of ride";
}