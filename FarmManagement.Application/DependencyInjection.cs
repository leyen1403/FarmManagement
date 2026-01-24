// ***********************************************************************
// File: DependencyInjection.cs
// Description: Configures dependency injection for the Application layer.
// ***********************************************************************

using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FarmManagement.Application;

/// <summary>
/// Provides extension methods for configuring dependency injection in the Application layer.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds application-specific services to the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    /// <returns>The configured service collection.</returns>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Register AutoMapper
        services.AddAutoMapper(cfg =>
        {
            // register all mapping profiles from this assembly
            cfg.AddMaps(Assembly.GetExecutingAssembly());
        });

        // Register MediatR and all handlers from this assembly
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        return services;
    }
}