using GraphQLDemo.API.Configurations;
using GraphQLDemo.API.GraphQLSetup.GraphQLConfiguration;
using GraphQLDemo.Infrastructure.Configurations;
using GraphQLDemo.Service.Configurations;
using GraphQLDemo.Service.Utils;

namespace GraphQLDemo.API;

public class Startup(IConfiguration configuration)
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.ConfigureInfrastructure(configuration);
        services.ConfigureService();
        services.ConfigurePresentation(configuration);
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseRouting();
        app.UseAuthorization();
        
        app.Use(async (context, next) =>
        {
            context.Request.EnableBuffering();
            await next();
        });
        
        app.UseMiddleware<CaptureRequestBodyMiddleware>();
        
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapDefaultControllerRoute();
        });
        
        //GraphiQL
        app.UseGraphQL<CustomSchema>();
        app.UseGraphQLGraphiQL("/graphiql");
        app.UseGraphQLPlayground("/graphql");
        
        
        //Altair
        //app.UseGraphQLAltair();
    }
}