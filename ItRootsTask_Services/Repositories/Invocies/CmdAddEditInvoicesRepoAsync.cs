using ItRootsTask_Core.Features.InvoicesFeatures.Command;
using ItRootsTask_Core.Interfaces.Repositories.InvocesRepo;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItRootsTask_Services.Repositories.Invocies
{
    public class CmdAddEditInvoicesRepoAsync : DapperManagerAsync<object>, ICmdAddEditInvoicesRepoAsync
    {
        public CmdAddEditInvoicesRepoAsync(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
