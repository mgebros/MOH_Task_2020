using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MOH.Common.Data;
using MOH.Common.IServices;
using MOH.Common.Services;
using Newtonsoft.Json.Linq;

namespace MOH.Jobaria
{
    public class Startup
    {
        public static string _cronExpression;
        public static Scheduler _scheduler;
        public static bool _isRunning;



        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MOHContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SQLServerConnectionString"), b => b.MigrationsAssembly("MOH_Task_2020")));
            services.AddScoped<IPeopleService, PeopleService>();
            


            ConnectiorDI._ps = services.BuildServiceProvider().GetService<IPeopleService>();
        }



        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.Run(async (context) =>
            {
                try
                {
                    if (File.Exists("./appsettings.json"))
                    {
                        JObject jo = JObject.Parse(File.ReadAllText("./appsettings.json"));
                        _cronExpression = jo["CronExpression"].ToString();
                    }
                    else
                    {
                        throw new Exception("File doesnt exisst");
                    }
                }catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    Environment.Exit(1);
                }


                if (!_isRunning)
                {
                    _isRunning = true;
                    _scheduler = new Scheduler();
                    await _scheduler.RunScheduler();
                }

            });
        }
    }
}
