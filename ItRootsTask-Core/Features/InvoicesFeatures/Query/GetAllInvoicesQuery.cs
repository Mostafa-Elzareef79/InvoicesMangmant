using Dapper;
using ItRootsTask_Core.Interfaces.Repositories.InvocesRepo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItRootsTask_Core.Features.InvoicesFeatures.Query
{
    public class GetAllInvoicesQuery:IRequest<Response<IEnumerable<AllInvoicesVM>>>
    {
    }
    public class GetAllInvoicesQueryHandler : IRequestHandler<GetAllInvoicesQuery, Response<IEnumerable<AllInvoicesVM>>>
{
        public IGetAllInvoicesQueryRepoAsync _repo;
        public GetAllInvoicesQueryHandler(IGetAllInvoicesQueryRepoAsync repo)
        {
            _repo = repo;

        }
        public async Task<Response<IEnumerable<AllInvoicesVM>>> Handle(GetAllInvoicesQuery request, CancellationToken cancellationToken)
        {
            var sql = "GetAllInvoices";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@InvoiceId", -1);
            var allData = new List<AllInvoicesVM>();
            using (var res = await _repo.MultipleQuery(sql, parameters, System.Data.CommandType.StoredProcedure))
            {

                 allData = (await res.ReadAsync<AllInvoicesVM>()).ToList();

            }
            return new Response<IEnumerable<AllInvoicesVM>>(allData, Enums.HttpStatuses.Status200OK);
        }


 
        }

    }

