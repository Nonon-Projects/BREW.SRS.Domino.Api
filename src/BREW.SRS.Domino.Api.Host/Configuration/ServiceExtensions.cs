using Abp.Domain.Repositories;
using BREW.SRS.Domino.Application;
using BREW.SRS.Domino.Application.Entities;
using BREW.SRS.Domino.Application.Shared.Persons;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BREW.SRS.Domino.Host.Configuration
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServiceBindings(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

           // if (configuration == null)
             //   throw new ArgumentNullException(nameof(configuration));

            services.AddScoped<IPersonService,PersonService>();
            // IRepository<Person>

            return services;
        }
    }
}
