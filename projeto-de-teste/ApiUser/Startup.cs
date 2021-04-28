using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiUser.Controllers;
using ApiUser.Models;
using ApiUser.Repository;
using ApiUser.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;

namespace ApiUser
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApiUserContext>(opt => opt.UseInMemoryDatabase("banco"));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiUser", Version = "v1" });
            });

            services.AddControllers().AddNewtonsoftJson(x =>
            {
                x.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IUserController, UserController>();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiUser v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var context = app.ApplicationServices.GetService<ApiUserContext>();
            AdicionarDadosTeste(context);

            //CreateDB.PrepDB(app);
        }

        private void AdicionarDadosTeste(ApiUserContext context)
        {
            context.Users.Add(new User(1, "Leonardo", "Mendes", 23, DateTime.Now));
            context.Users.Add(new User(2, "Ivana", "Maria", 59, DateTime.Now));
            context.Users.Add(new User(3, "Jéssica", "Mendes", 30, DateTime.Now));

        }
    }
}
