using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
namespace ApiUser.Repository
{
    public static class CreateDB
    {
        public static void PrepDB(IApplicationBuilder app)
        {
            using (var serviceScrope = app.ApplicationServices.CreateScope())
            {
                SeedDB(serviceScrope.ServiceProvider.GetService<ApiUserContext>());
            }
        }
        public static void SeedDB(ApiUserContext context)
        {
            System.Console.WriteLine("Criando Database");
            
            context.Database.Migrate();
        }
    }
}