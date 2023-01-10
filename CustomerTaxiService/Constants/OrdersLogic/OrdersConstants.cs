namespace CustomerTaxiService.Constants;

public class OrdersConstants
{
    public const string Ok = "Ok";
    public const string UserNotFound = "User not found";

    public const string UserNotFoundAdditionalText =
        "Sorry, but customer with this phone number not exist. Please do registration before use uor service ";

    public const string RideNotFound = "Ride not found";
    public const string RideNotFoundAdditionalText = "Sorry, but we don't have orders with this number";
    
    public const string UserIsAlreadyHaveAOrder = "Sorry, but user already have a order";
    public const string UserIsAlreadyHaveAOrderAdditionalText = "You can't request several ride while you in ride or waiting for a driver";
}