using ItRootsTask_Core.Interfaces.Repositories.LoginRepo;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItRootsTask_Services.Repositories.Login
{
    public class CmdLoginCheckRepoAsync : DapperManagerAsync<object>, ICmdLoginCheckRepoAsync
    {
        public CmdLoginCheckRepoAsync(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
