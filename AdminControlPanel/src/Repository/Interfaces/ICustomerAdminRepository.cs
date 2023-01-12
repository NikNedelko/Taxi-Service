using CustomerTaxiService.Repository.Interfaces;
using Entities.CustomerTaxiService.CustomerData;

namespace AdminControlPanel.Repository.Interfaces;

public interface ICustomerAdminRepository : IUserRepository
{
    public Task<List<CustomerDB>> GetAllUserWIthId();
}