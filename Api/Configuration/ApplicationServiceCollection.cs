namespace Api.Configuration;

public static class ApplicationServiceCollection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(Application.Commands.SendUpcomingNotificationsCommand).Assembly);
        });

        return services;
    }
}
