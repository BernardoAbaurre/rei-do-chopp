using System.Linq;
using ReiDoChopp.Domain.PrintControls.Entities;
using ReiDoChopp.Domain.PrintControls.Repositories.Filters;
using ReiDoChopp.Domain.Utils;

namespace ReiDoChopp.Domain.PrintControls.Repositories
{
    public interface IPrintControlsRepository : IEntityFrameworkRepository<PrintControl>
    {
        IQueryable<PrintControl>Filter(PrintControlsListFilter filter);
    }
}
