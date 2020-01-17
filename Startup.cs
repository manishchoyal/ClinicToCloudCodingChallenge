using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinicToCloudCodingChallenge.Database;
using ClinicToCloudCodingChallenge.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace ClinicToCloudCodingChallenge
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //services.AddControllers();
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = new List<JsonConverter> { new StringEnumConverter() }
            };
            services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase(databaseName: "database_name"));
            services.AddMvc();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            services.AddTransient<IPatientService, PatientService>();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //var context = app.ApplicationServices.GetService<ApiContext>();
            var context = serviceProvider.GetService<ApiContext>();
            AddTestData(context);

            app.UseMvc();
        }

        private void AddTestData(ApiContext context)
        {
            Database.Models.Patient p = new Database.Models.Patient
            {
                Date_Of_Birth = "22/11/1979",
                First_Name = "M",
                Last_Name = "A"
            };
            context.Add(p);
            context.SaveChanges();
        }
    }
}
