using ItRootsTask_Core.Interfaces;
using ItRootsTask_Core.Interfaces.Repositories.InvocesRepo;
using ItRootsTask_Core.Interfaces.Repositories.LoginRepo;
using ItRootsTask_Core.Interfaces.Repositories.RegisterRepo;
using ItRootsTask_Services;
using ItRootsTask_Services.Repositories.Invocies;
using ItRootsTask_Services.Repositories.Login;
using ItRootsTask_Services.Repositories.Register;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RoomHour.Application;
using System.Reflection;

namespace ItRootsTask
{
    public class Program
    {
        public static void Main(string[] args)
        {
               
        var builder = WebApplication.CreateBuilder(args);

           
            builder.Services.AddControllersWithViews();
          builder.Services.AddApplicationLayer();
            builder.Services.Configure<EncryptSettingsModel>(builder.Configuration.GetSection("EncryptSettingsModel")); 
            builder.Services.AddTransient<IEncryption, Encryption>();
            builder.Services.AddTransient(typeof(IDapperManager<>), typeof(DapperManagerAsync<>));
            builder.Services.AddTransient<ICmdLoginCheckRepoAsync, CmdLoginCheckRepoAsync>();
            builder.Services.AddTransient<ICmdAddRegisterRepoAsync, CmdAddRegisterRepoAsync>();
            builder.Services.AddTransient<ICmdverifyCodeRepoAsync, CmdverifyCodeRepoAsync>();
            builder.Services.AddTransient<IGetAllInvoicesQueryRepoAsync, GetAllInvoicesQueryRepoAsync>();
            builder.Services.AddTransient<ICmdAddEditInvoicesRepoAsync, CmdAddEditInvoicesRepoAsync>();
            builder.Services.AddTransient<ICmdDeleteInvoiceRepoAsync, CmdDeleteInvoiceRepoAsync>();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
