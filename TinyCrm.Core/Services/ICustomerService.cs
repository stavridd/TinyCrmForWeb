using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;

namespace TinyCrm.Core.Services
{
    public interface ICustomerService
    {
        public Task<ApiResult<Customer>> CreateCustomer(
                    CreateCustomerOptions options);

        IQueryable<Model.Customer> SearchCustomers(
            Model.Options.SearchCustomerOptions options);

        bool UpdateCustomer(int id,
            Model.Options.UpdateCustomerOptions options);
    }
}
