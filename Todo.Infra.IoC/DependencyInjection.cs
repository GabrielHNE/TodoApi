using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Todo.Application.Interfaces;
using Todo.Application.Services;
using Todo.Domain.Interfaces.Repositories;
using Todo.Domain.Interfaces.Services;
using Todo.Infra.Auth;
using Todo.Infra.Data.PostgreSQL.Extensions;
using Todo.Infra.Data.PostgreSQL.Repositories;
using Todo.Infra.Extensions;
using Todo.Infra.Middlewares;
using Todo.Infra.Security;

namespace Todo.Infra.IoC;
public static class DependencyInjection 
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        => services.AddInfrastructure(configuration);

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplicationDbContext(configuration);

        services.AddTransient<ExceptionHandlingMiddleware>();

        services.AddControllers();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITodoItemRepository, TodoItemRepository>();

        services.AddScoped<IPasswordEncoder, PasswordEncoder>();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITodoItemService, TodoItemService>();
        services.AddScoped<IUserService, UserService>();

        services.AddSwaggerJwtAuthentication();

        services.AddJwtAuthentication(configuration);        

        return services;
    }

    public static IApplicationBuilder ConfigureMiddlewares(this IApplicationBuilder app){
        app.UseMiddleware<ExceptionHandlingMiddleware>();

        return app;
    }
}