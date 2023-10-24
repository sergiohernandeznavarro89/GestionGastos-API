namespace Api.Configuration;

public static class InfrastructureServiceCollection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        //REPOSITORIES
        services.AddScoped<IUserQueryRepository, UserQueryRepository>();

        services.AddScoped<IAccountQueryRepository, AccountQueryRepository>();
        services.AddScoped<IAccountCommandRepository, AccountCommandRepository>();

        services.AddScoped<IItemCommandRepository, ItemCommandRepository>();
        services.AddScoped<IItemSummaryQueryRepository, ItemSummaryQueryRepository>();
        services.AddScoped<IItemQueryRepository, ItemQueryRepository>();        

        services.AddScoped<IItemPaymentQueryRepository, ItemPaymentQueryRepository>();
        services.AddScoped<IItemPaymentCommandRepository, ItemPaymentCommandRepository>();

        services.AddScoped<ICategoryQueryRepository, CategoryQueryRepository>();
        services.AddScoped<ICategoryCommandRepository, CategoryCommandRepository>();
        
        services.AddScoped<ISubCategoryQueryRepository, SubCategoryQueryRepository>();
        services.AddScoped<ISubCategoryCommandRepository, SubCategoryCommandRepository>();

        services.AddScoped<IDebtCommandRepository, DebtCommandRepository>();
        services.AddScoped<IDebtSummaryQueryRepository, DebtSummaryQueryRepository>();
        services.AddScoped<IDebtQueryRepository, DebtQueryRepository>();

        //HTTP CLIENT FACTORY
        services.AddHttpClient();
        services.AddScoped<IUnitOfWorkRepository, UnitOfWorkRepository>();
        services.AddScoped<IUnitOfWorkFactory, UnitOfWorkFactory>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        

        //DATABASE CONNECTION
        services.AddScoped<IDatabaseConnection, DatabaseConnection>();

        return services;
    }

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        ISqlConfig sqlconfig = new SqlConfig();
        configuration.GetSection("ConnectionStrings").Bind(sqlconfig);
        services.AddSingleton(sqlconfig);

        return services;
    }

}
