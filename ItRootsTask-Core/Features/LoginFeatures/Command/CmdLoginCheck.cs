namespace ItRootsTask_Core.Features.LoginFeatures.Command;
using Dapper;
using ItRootsTask_Business;
using ItRootsTask_Core.Enums;
using ItRootsTask_Core.Interfaces;
using ItRootsTask_Core.Interfaces.Repositories.LoginRepo;
using MediatR;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

public class CmdLoginCheck : IRequest<Response<int>>
{
    [DefaultValue("")]
    public string UserName {  get; set; }
    [DefaultValue("")]

    public string Password { get; set; }
    public class CmdLoginCheckHandler : IRequestHandler<CmdLoginCheck, Response<int>>
    {
        private readonly ICmdLoginCheckRepoAsync _repo;
        private readonly IEncryption _encryption;

        public CmdLoginCheckHandler(ICmdLoginCheckRepoAsync repo, IEncryption encryption)
        {
            _repo = repo;
            _encryption = encryption;
        }
        public async Task<Response<int>> Handle(CmdLoginCheck request, CancellationToken cancellationToken)
        {
            var sql = "check_user_status";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("UserName", request.UserName);
            parameters.Add("is_success",null, System.Data.DbType.Int32,System.Data.ParameterDirection.Output);
            parameters.Add("is_verified",null, System.Data.DbType.Int32,System.Data.ParameterDirection.Output);
            try {
                var result = await _repo.ExecuteAsync(sql, parameters, System.Data.CommandType.StoredProcedure);
                var isUserExist = parameters.Get<int>("is_success");
                var isUserVarified = parameters.Get<int>("is_verified");

                if (isUserExist == -5)
                {
                    return new Response<int>(HttpStatuses.Status401Unauthorized, "هذا المستخدم غير موجود");
                }
                else if (isUserExist == 1 && isUserVarified == 0)
                {
                    return new Response<int>(HttpStatuses.Status401Unauthorized, "هذا المستخدم غير مفعل");
                }

                if (request.Password != null)
                {
                    var HashPassword = _encryption.EncryptString(request.Password);
                    var sql2 = "check_user_password";
                    DynamicParameters parameters2 = new DynamicParameters();
                    parameters2.Add("@UserName", request.UserName);
                    parameters2.Add("@Password", HashPassword);
                    parameters2.Add("@is_success", null, System.Data.DbType.Int32, System.Data.ParameterDirection.Output);
                    var Res = await _repo.ExecuteAsync(sql2, parameters2, System.Data.CommandType.StoredProcedure);
                    var IsValid = parameters2.Get<int>("is_success");
                    if (IsValid == -5)
                    {
                        return new Response<int>(HttpStatuses.Status401Unauthorized, "كلمة المرور خاطئة");
                    }
                    else
                    {
                        return new Response<int>(IsValid, HttpStatuses.Status200OK, "تم تسجيل الدخول بنجاح");
                    }


                }
                else if (request.Password == null)
                {
                    return new Response<int>(HttpStatuses.Status401Unauthorized, "برجاء ادخال كلمة مرور صالحة ");
                }
            } catch
            {
                return new Response<int>(HttpStatuses.Status500InternalServerError, "حدث خطا ما رجاءا المحاولة مرة اخري");
            }


            return new Response<int>(HttpStatuses.Status500InternalServerError, "حدث خطا ما رجاءا المحاولة مرة اخري");

        }
    }


}
