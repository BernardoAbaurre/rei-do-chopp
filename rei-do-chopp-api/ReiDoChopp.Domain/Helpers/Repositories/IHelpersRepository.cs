using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReiDoChopp.Infra.Helpers.Repositories
{
    public interface IHelpersRepository
    {
        Task<bool> PrintAsync(string text);
    }
}
