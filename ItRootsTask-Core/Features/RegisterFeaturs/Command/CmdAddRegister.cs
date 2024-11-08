using Dapper;
using ItRootsTask_Core.Enums;
using ItRootsTask_Core.Interfaces;
using ItRootsTask_Core.Interfaces.Repositories.RegisterRepo;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ItRootsTask_Core.Features.RegisterFeaturs.Command
{
    public class UserModel
    {
        public string? UserName { get; set; }
        public string? Code { get; set; }
    }
    public class CmdAddRegister:IRequest<Response<UserModel>>
    {
        [DefaultValue("")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        [DefaultValue("")]

        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [DefaultValue("")]
        public string Email { get; set; }
        [DefaultValue("")]

        public string Password { get; set; }
        [Display(Name = "Phone Number")]
        [DefaultValue("")]

        public string PhoneNumber { get; set; }
        public class CmdAddRegisterHandler : IRequestHandler<CmdAddRegister, Response<UserModel>>
        {
            private readonly ICmdAddRegisterRepoAsync _repo;
            private readonly IEncryption _encryption;
            public CmdAddRegisterHandler(ICmdAddRegisterRepoAsync repo,IEncryption encryption)
            {
                _repo = repo;
                _encryption = encryption;
                
            }
            public async Task<Response<UserModel>> Handle(CmdAddRegister request, CancellationToken cancellationToken)
            {
                var verificationCode = new Random().Next(100000, 999999).ToString();
                var passHash=_encryption.EncryptString(request.Password);
                var sql = "AddUserAccountNewst";
              DynamicParameters parameters= new DynamicParameters();
                parameters.Add("@FullName", request.FullName);
                parameters.Add("@PhoneNumber", request.PhoneNumber);
                parameters.Add("@Email", request.Email);
                parameters.Add("@UserName", request.UserName);
                parameters.Add("@Password", passHash);
                parameters.Add("@VerificationCode", verificationCode);
                parameters.Add("@is_success", null,System.Data.DbType.Int32,System.Data.ParameterDirection.Output);
                try
                {
                    var res = await _repo.ExecuteAsync(sql, parameters, System.Data.CommandType.StoredProcedure);
                    var isSuccess = parameters.Get<int>("is_success");
                    if (isSuccess == 1)
                    {
                        await SendVerificationEmail(request.Email, verificationCode);
                        var user = new UserModel()
                        {
                            UserName = request.UserName,
                            Code = verificationCode

                        };
                        return new Response<UserModel>(user, HttpStatuses.Status401Unauthorized, "added success");
                    }
                    else if (isSuccess == -5)
                    {
                        return new Response<UserModel>(HttpStatuses.Status401Unauthorized, "already exist");

                    }
                } catch {
                    return new Response<UserModel>(HttpStatuses.Status401Unauthorized, "unexpected eror happend");
                }
                return new Response<UserModel>(HttpStatuses.Status401Unauthorized, "error happend");
            }
            private async Task SendVerificationEmail(string email, string verificationCode)
            {
              
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("mostafa233323332@gmail.com", "wvnf tpvo rjzl iecz"), 
                    EnableSsl = true,
                };

                
                var message = new MailMessage
                {
                    From = new MailAddress("Itroots@gmail.com"),
                    Subject = "Your Verification Code",
                    Body = $"Your verification code is: {verificationCode}",
                    IsBodyHtml = true,
                };
                message.To.Add(email);

            
                await smtpClient.SendMailAsync(message);
            }
        }
    }
}
