using Dapper;
using ItRootsTask_Core.Enums;
using ItRootsTask_Core.Interfaces.Repositories.InvocesRepo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItRootsTask_Core.Features.InvoicesFeatures.Command
{
    public class Invoice
    {
        public int? Id { get; set; }
        public string? ProductName { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public int? InvoiceId { get; set; }
    }
    public class CmdAddEditInvoices : IRequest<Response<int>>
    {
        public List<Invoice> Invoices { get; set; }=new List<Invoice>();
    
            
        
        public class CmdAddEditInvoicesHandler : IRequestHandler<CmdAddEditInvoices, Response<int>>
{
            public ICmdAddEditInvoicesRepoAsync _repo;
            public CmdAddEditInvoicesHandler(ICmdAddEditInvoicesRepoAsync repo)
            {
                
                _repo = repo;
            }
            public async Task<Response<int>> Handle(CmdAddEditInvoices request, CancellationToken cancellationToken)
            {
                var sql = "AddOrEditInvoice";
                DynamicParameters parameters= new DynamicParameters();
                parameters.Add("@InvoiceId", request.Invoices.ToList()[0].InvoiceId==null?0: request.Invoices.ToList()[0].InvoiceId);
                parameters.Add("@TotalAmount", request.Invoices.ToList().Sum(i=>i.Price==null?0: i.Price));
                parameters.Add("@is_success", null,System.Data.DbType.Int32,System.Data.ParameterDirection.Output) ;
                await _repo.ExecuteAsync(sql, parameters, System.Data.CommandType.StoredProcedure);
                var is_success = parameters.Get<int>("is_success");
                if(is_success >= 0)
                {
                   
             if(request.Invoices.Count > 0)
                    {
                        foreach (var item in request.Invoices)
                        {


                            var sql2 = "AddOrEditInvoiceItems";
                            DynamicParameters parameters2 = new DynamicParameters();
                            parameters2.Add("@InvoiceItemId", item.Id==null?0:item.Id );
                            parameters2.Add("@InvoiceId", is_success);
                            parameters2.Add("@Price", item.Price == null ? 0 : item.Price);
                            parameters2.Add("@ProductName", item.ProductName == null ? "": item.ProductName);
                            parameters2.Add("@Quantity", item.Quantity == null ? 0 : item.Quantity);

                            parameters2.Add("@is_success", null, System.Data.DbType.Int32, System.Data.ParameterDirection.Output);
                            await _repo.ExecuteAsync(sql2, parameters2, System.Data.CommandType.StoredProcedure);


                            is_success = parameters.Get<int>("is_success");

                        }
                    }
                    if(is_success >= 0)
                    {
                        return new Response<int>(is_success, HttpStatuses.Status200OK, "تم الاضافة بنجاح");
                    }
                    else
                    {
                        return new Response<int>( HttpStatuses.Status404NotFound, "حدثت مشكلة برجاء المحاولة مرة اخري");

                    }

                }

                throw new NotImplementedException();
            }
        }
    }
}
