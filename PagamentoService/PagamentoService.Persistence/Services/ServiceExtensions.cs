using PagamentoService.Domain.Interfaces;
using PagamentoService.Persistence.Context;
using PagamentoService.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagamentoService.Persistence.Services
{
    public static class ServiceExtensions
    {
        public static void ConfigurePersistenceApp(this IServiceCollection services,
                                                    IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options =>
                     options.UseSqlServer(connectionString, b => b.MigrationsAssembly("PagamentoService.WebApi")));
            
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPagamentoRepository, PagamentoRepository>();
        }
    }
}
