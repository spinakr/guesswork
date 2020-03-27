using Domain;
using Domain.GroupAggregate.DomainEvents;
using Marten;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Web.Queries;

namespace Web
{
    public static class MartenConfiguration
    {
        public static void ConfigureMarten(this IServiceCollection services, IConfiguration configuration, IHostEnvironment hostEnvironment)
        {
            var store = DocumentStore.For(_ =>
            {
                _.DatabaseSchemaName = $"guesswork_{hostEnvironment.EnvironmentName}";
                _.Connection(configuration.GetValue<string>("POSTGRES_CON_STRING"));

                _.CreateDatabasesForTenants(c =>
                {
                    c.ForTenant()
                        .CheckAgainstPgDatabase()
                        .WithOwner("postgres")
                        .WithEncoding("UTF-8")
                        .ConnectionLimit(-1);
                });

                _.AutoCreateSchemaObjects = AutoCreate.All;


                _.Events.AddEventType(typeof(NewGroupWasCreated));
                _.Events.AddEventType(typeof(NewTeamWasRegistered));
                _.Events.AddEventType(typeof(NewTournamentWasCreated));
                
                _.Events.InlineProjections.Add(new TournamentProjection());
            });

            services.AddSingleton<IDocumentStore>(store);
            services.AddScoped(sp => sp.GetService<IDocumentStore>().OpenSession());
        }
    }
}