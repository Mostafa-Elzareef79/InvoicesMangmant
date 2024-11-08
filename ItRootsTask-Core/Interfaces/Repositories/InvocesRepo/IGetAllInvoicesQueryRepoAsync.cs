using ItRootsTask_Core.Features.InvoicesFeatures.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItRootsTask_Core.Interfaces.Repositories.InvocesRepo
{
    public interface IGetAllInvoicesQueryRepoAsync:IDapperManager<AllInvoicesVM>
    {
    }
}
