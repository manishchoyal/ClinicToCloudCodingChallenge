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
            var options = new DbContextOptionsBuilder<ApiContext>().UseInMemoryDatabase(databaseName: "database_name").Options;
            services.AddSingleton(x => new ApiContext(options));
            //services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase(databaseName: "database_name"));
            services.AddMvc();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            services.AddTransient<IPatientService, PatientService>();
            services.AddTransient<IDatabase, Database.Database>();
            //services.AddSingleton(x => new ApiContext(opt => opt.UseInMemoryDatabase(databaseName: "database_name"))
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
            for(int i = 1; i <= 15; i++)
            {
                Database.Models.Patient p = new Database.Models.Patient
                {
                    DateOfBirth = "12/12/2012",
                    FirstName = $"Sam {i}",
                    LastName = $"Smith {i}",
                    Email = $"Test{i}@test.com.au",
                    Gender = "Male",
                    Id = new Guid(),
                    IsActive = true,
                    Phone = $"123123{i}",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                context.Add(p);
            }
            
            
            context.SaveChanges();
        }
    }
}
