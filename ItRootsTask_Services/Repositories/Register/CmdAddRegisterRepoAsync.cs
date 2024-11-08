using ItRootsTask_Core.Interfaces.Repositories.RegisterRepo;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItRootsTask_Services.Repositories.Register
{
    public class CmdAddRegisterRepoAsync : DapperManagerAsync<object>, ICmdAddRegisterRepoAsync
    {
        public CmdAddRegisterRepoAsync(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
