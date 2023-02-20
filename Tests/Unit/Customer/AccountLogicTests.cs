using System.Runtime.CompilerServices;
using Entities.CustomerApi.Requests;
using TaxiService.BusinessLogic.Customer.Interfaces;

namespace Tests.Unit.Customer;

[TestClass]
public sealed class AccountLogicTests
{
    private readonly IAccountLogic _accountLogic;

    public AccountLogicTests(IAccountLogic accountLogic)
    {
        _accountLogic = accountLogic;
    }

    public async void CreateUserWithRegistration()
    {
        var entityForRegistration = await GetRegistrationAccount();
        Assert.are
        
    }

    private async Task<RegistrationForUser> GetRegistrationAccount() => new RegistrationForUser
    {
        Name = "TestNameForUnitTesting",
        LastName = "TestLastNameForUnitTesting",
        PhoneNumber = "TestPhoneNumberForUnitTesting",
        Email = "TestEmailForUnitTesting"
    };
}