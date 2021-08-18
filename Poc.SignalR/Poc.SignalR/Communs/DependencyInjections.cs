using Microsoft.Extensions.DependencyInjection;
using Poc.SignalR.Context;
using Poc.SignalR.Interfaces;
using Poc.SignalR.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poc.SignalR.Communs
{
    public static class DependencyInjections
    {
        /// <summary>
        /// Método responsável por realizar o bind de dependências
        /// </summary>
        /// <param name="services">Coleção de serviços do startup</param>
        public static void AddDependencyInjections(this IServiceCollection services)
        {
            /* Repositories */
            services.AddScoped<IDispositivoRepository, DispositivoRepository>();
            services.AddScoped<IMensagemRepository, MensagemRepository>();

            /* Contexts */
            services.AddDbContext<DataContext>();
        }
    }
}
