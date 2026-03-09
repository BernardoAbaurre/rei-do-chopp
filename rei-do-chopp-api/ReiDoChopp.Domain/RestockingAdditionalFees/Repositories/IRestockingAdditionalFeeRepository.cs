using System.Linq;
using ReiDoChopp.Domain.RestockingAdditionalFees.Entities;
using ReiDoChopp.Domain.RestockingAdditionalFees.Repositories.Filters;
using ReiDoChopp.Domain.Utils;

namespace ReiDoChopp.Domain.RestockingAdditionalFees.Repositories
{
    public interface IRestockingAdditionalFeesRepository : IEntityFrameworkRepository<RestockingAdditionalFee>
    {
        IQueryable<RestockingAdditionalFee>Filter(RestockingAdditionalFeesListFilter filter);
    }
}
