using System.Linq;
using Microsoft.EntityFrameworkCore;
using ReiDoChopp.Domain.PrintControls.Entities;
using ReiDoChopp.Domain.PrintControls.Repositories;
using ReiDoChopp.Domain.PrintControls.Repositories.Filters;
using ReiDoChopp.Infra.Utils;
using ReiDoChopp.Infra.Data;

namespace ReiDoChopp.Infra.PrintControls.Repositories
{
    public class PrintControlsRepository : EntityFrameworkRepository<PrintControl>, IPrintControlsRepository
    {
        public PrintControlsRepository(ReiDoChoppDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<PrintControl> Filter(PrintControlsListFilter filter)
        {
            DateTime timeLimit = DateTime.Now.AddMinutes(-1);

            var query = Query().Where(x => x.RequestDate >= timeLimit);
            
            if (filter.Id.HasValue)
            {
                query = query.Where(x => x.Id == filter.Id.Value);
            }
            if (filter.Status.HasValue)
            {
                query = query.Where(x => x.Status == filter.Status.Value);
            }
            if (filter.UserId.HasValue)
            {
                query = query.Where(x => x.User.Id == filter.UserId.Value);
            }
            if (!string.IsNullOrEmpty(filter.Content))
            {
                query = query.Where(x => x.Content.ToUpper().Contains(filter.Content.ToUpper()));
            }

            return query;
        }
    }
}
