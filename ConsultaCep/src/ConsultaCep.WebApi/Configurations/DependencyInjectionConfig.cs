using ConsultaCep.Application.Interfaces;
using ConsultaCep.Application.UseCases;
using ConsultaCep.Domain.Interfaces;
using ConsultaCep.Infrastructure.Adapters.ExternalServices.ViaCep;
using Microsoft.Extensions.DependencyInjection;

namespace ConsultaCep.WebApi.Configurations
{
    /// <summary>
    /// </summary>
    public static class DependencyInjectionConfig
    {
        /// <summary>
        /// </summary>
        // Método que registra todas as dependências da arquitetura
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // Ligando a Interface da Aplicação à Implementação do Caso de Uso
            services.AddScoped<IConsultarCepUseCase, ConsultarCepUseCase>();

            // Ligando a Interface do Domínio (ICepService) ao Adapter (ViaCepAdapter)
            services.AddHttpClient<ICepService, ViaCepAdapter>();

            return services;
        }
    }
}