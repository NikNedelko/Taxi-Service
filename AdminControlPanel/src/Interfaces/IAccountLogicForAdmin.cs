using CustomerTaxiService.BusinessLogic.Interfaces;
using Entities.General;

namespace AdminControlPanel.Interfaces;

public interface IAccountLogicForAdmin : IAccountLogic
{
    public Task<string> ChangeAccountStatus (string accountId, AccountStatus newAccountStatus);
}