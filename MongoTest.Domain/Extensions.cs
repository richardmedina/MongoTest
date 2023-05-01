using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using MongoTest.Common.Domain.Repository;
using MongoTest.Domain.Repository;

namespace MongoTest.Domain
{
    public static class Extensions
    {
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IMongoDatabase, IMongoDatabase>(impl => {
                var client = new MongoClient("mongodb://192.168.0.21");
                var db = client.GetDatabase("richarddb");
                return db;
            });

            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}