using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PacienteService.Domain.Interfaces;
using PacienteService.Persistence.Context;
using PacienteService.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacienteService.Persistence.Services
{
    public static class ServiceExtensions
    {
        public static void ConfigurePersistenceApp(this IServiceCollection services,
                                                    IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SqlServer");
            services.AddDbContext<AppDbContext>(options =>
                     options.UseSqlServer(connectionString, b => b.MigrationsAssembly("PacienteService.WebApi")));
            
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPacienteRepository, PacienteRepository>();
        }
    }
}
