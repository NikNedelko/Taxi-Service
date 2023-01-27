namespace TaxiService.Constants.Driver;

public static class DriverConstants
{
    public const string Ok = "Ok";
    public const string DriverIsNotWorking = "Driver is not working";
    public const string OrderByIdIsNotExist = "Order with this id is not exist";
    public const string OrderIsAlreadyTaken = "Order with this id is already taken";
    public const string OrderByNumberIsNotExist = "Order with this phone number is not exist";
    public const string CanNotDeleteWhileInRide = "You can not delete account while you in a ride";
    public const string CanNotEndWorkWhileInRide = "You can not end work while you in a ride";
    
    public const string OrderByIdIsNotExistAdditionalText = "Please check id of order";
    public const string OrderByNumberIsNotExistAdditionalText = "Please check your phone number of order";
    
    
    public const string DriverIsNotExist = "Driver is not exist";
    public const string DriverIsExist = "Driver is already exist";
    public const string DriverWasAdded = "Your driver account was created";
    public const string DriverWasDeleted = "Your driver account was deleted";
    
    public const string DriverIsAlreadyWorking = "Driver account is already working";
    
    public const string DriverWasDeletedAdditionalInfo = "Sorry to hear that. We hope to see you again";
    public const string DriverIsNotExistAdditionalInfo = "Sorry, but driver with this number is not exist";

    public const string DriverIsExistAdditionalInfo = "Driver with this phone number or license number is already exist";
    public const string DriverWasAddedAdditionalInfo = "Thank you for registration in our service. Now you can start work and take your first order";
}