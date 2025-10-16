// File: src/ConsultaCep.Application/UseCases/ConsultarCepUseCase.cs

// Importa as interfaces e entidades das camadas que o Application precisa
using ConsultaCep.Application.DTOs;
using ConsultaCep.Application.Interfaces;
using ConsultaCep.Domain.Interfaces; // A Porta de Saída da camada Domain
using System.Threading.Tasks;

namespace ConsultaCep.Application.UseCases
{
    // 1. Mudamos para 'public' para ser acessível pela WebAPI
    // 2. Implementamos a interface IConsultarCepUseCase (Porta de Entrada)
    public class ConsultarCepUseCase : IConsultarCepUseCase
    {
        // Variável privada para armazenar a dependência (Porta de Saída)
        private readonly ICepService _cepService;

        // Construtor para Injeção de Dependência: o .NET fornecerá a implementação (Adapter)
        public ConsultarCepUseCase(ICepService cepService)
        {
            _cepService = cepService;
        }

        public async Task<EnderecoResponse?> ExecuteAsync(string cep)
        {
            // REQUISITO: Validar o formato do CEP
            var cepLimpo = cep?.Replace("-", "").Trim();
            if (string.IsNullOrWhiteSpace(cepLimpo) || cepLimpo.Length != 8)
            {
                // Lança uma exceção se a validação falhar
                throw new System.ArgumentException("CEP inválido. O CEP deve ter 8 dígitos.", nameof(cep));
            }

            // REQUISITO: Acionar a interface do serviço (Porta de Saída)
            var endereco = await _cepService.ConsultarCepAsync(cepLimpo);

            if (endereco == null)
            {
                // Se o serviço não encontrar o CEP, retornamos null
                return null;
            }

            // REQUISITO: Preparar o retorno com os dados do endereço
            // Mapeia a Entidade de Domínio (Endereco) para o DTO de Resposta (EnderecoResponse)
            var response = new EnderecoResponse(
                endereco.Cep,
                endereco.Logradouro,
                endereco.Complemento,
                endereco.Bairro,
                endereco.Localidade,
                endereco.Uf
            );

            return response;
        }
    }
}