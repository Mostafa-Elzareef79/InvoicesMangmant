using Dapper;
using ItRootsTask_Core.Interfaces.Repositories.InvocesRepo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItRootsTask_Core.Features.InvoicesFeatures.Command
{
    public class CmdDeleteInvoice:IRequest<Response<int>>
    {
        public int Id { get; set; }
        public class CmdDeleteInvoiceHandler : IRequestHandler<CmdDeleteInvoice, Response<int>>
        {
            private readonly ICmdDeleteInvoiceRepoAsync _repo;
            public CmdDeleteInvoiceHandler(ICmdDeleteInvoiceRepoAsync repo)
            {
                _repo = repo;
                
            }
            public async Task<Response<int>> Handle(CmdDeleteInvoice request, CancellationToken cancellationToken)
            {
                var sql = "DeleteInvoices";
                DynamicParameters parameters= new DynamicParameters();
                parameters.Add("@InvoiceId", request.Id);
                parameters.Add("@is_success", null, System.Data.DbType.Int32, System.Data.ParameterDirection.Output);
                await _repo.ExecuteAsync(sql, parameters, System.Data.CommandType.StoredProcedure);
           var   is_success = parameters.Get<int>("is_success");
                if (is_success == 1)
                {
                    return new Response<int>(is_success, Enums.HttpStatuses.Status200OK, "تنم الحذف بنجاح");
                }
                return new Response<int>(is_success, Enums.HttpStatuses.Status200OK, " حدثت مشكلة اثناء الحذف");
            }
        }
    }
}
