using Dapper;
using ItRootsTask_Core.Enums;
using ItRootsTask_Core.Interfaces.Repositories.RegisterRepo;
using MediatR;
using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace ItRootsTask_Core.Features.RegisterFeaturs.Command
{
    public class CmdverifyCode : IRequest<Response<int>>
    {
        [DefaultValue("")]
        public string UserName { get; set; }

        public int VerificationCode { get; set; }

        public class CmdverifyCodeHandler : IRequestHandler<CmdverifyCode, Response<int>>
        {
            private readonly ICmdverifyCodeRepoAsync _repo;

            public CmdverifyCodeHandler(ICmdverifyCodeRepoAsync repo)
            {
                _repo = repo;
            }

            public async Task<Response<int>> Handle(CmdverifyCode request, CancellationToken cancellationToken)
            {
                var sql = "UpdateUserVerificationStatus";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@username", request.UserName);
                parameters.Add("@code", request.VerificationCode);
                parameters.Add("@is_success", null, System.Data.DbType.Int32, System.Data.ParameterDirection.Output);

                var res = await _repo.ExecuteAsync(sql, parameters, System.Data.CommandType.StoredProcedure);
                var result = parameters.Get<int>("is_success");

                if (result == 1)
                {
                    return new Response<int>(result,HttpStatuses.Status200OK, "تم التحقق بنجاح.");
                }
                else if (result == -5)
                {
                    return new Response<int>(HttpStatuses.Status404NotFound, "هذا المستخدم غير مسجل.");
                }
                else
                {
                    return new Response<int>(HttpStatuses.Status400BadRequest, "الكود غير صحيح.");
                }
            }
        }
    }
}
