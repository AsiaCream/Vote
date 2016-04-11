using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using vote.Models;
using Microsoft.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Extensions.Logging;

namespace vote
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var appEnv = services.BuildServiceProvider().GetRequiredService<IApplicationEnvironment>();
            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<VoteContext>(x => x.UseSqlServer("server=localhost;uid=sa;password=123456;database=vote"));

            //services.addentityframework()
            //    .addsqlite()
            //    .adddbcontext<votecontext>(x=>x.usesqlite("data source=" + appenv.applicationbasepath + "/datebase/vote.db"));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<VoteContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();
            services.AddSmartUser<User, string>();
        }

        public async void Configure(IApplicationBuilder app,ILoggerFactory logger)
        {
            app.UseIISPlatformHandler();
            app.UseStaticFiles();
            logger.MinimumLevel = LogLevel.Information;
            logger.AddConsole();
            logger.AddDebug();
            app.UseIdentity();
            app.UseMvc(x => x.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}"));
            await SampleData.InitDB(app.ApplicationServices);

        }
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
