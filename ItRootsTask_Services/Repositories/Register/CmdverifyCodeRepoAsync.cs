using ItRootsTask_Core.Interfaces.Repositories.RegisterRepo;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItRootsTask_Services.Repositories.Register
{
    public class CmdverifyCodeRepoAsync : DapperManagerAsync<object>, ICmdverifyCodeRepoAsync
    {
        public CmdverifyCodeRepoAsync(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
