using ConsultaCep.Application.DTOs;
using ConsultaCep.Application.Interfaces;
using ConsultaCep.Domain.Interfaces; 
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;

namespace ConsultaCep.Application.UseCases
{
    // interface IConsultarCepUseCase (Porta de Entrada)
    public class ConsultarCepUseCase : IConsultarCepUseCase
    {
        private readonly ICepService _cepService;

        public ConsultarCepUseCase(ICepService cepService)
        {
            _cepService = cepService;
        }

        public async Task<EnderecoResponse?> ExecuteAsync(string cep)
        {
            var cepLimpo = cep?.Replace("-", "").Trim();
            if (string.IsNullOrWhiteSpace(cepLimpo) || cepLimpo.Length != 8)
            {
                throw new System.ArgumentException("CEP inválido. O CEP deve ter 8 dígitos.", nameof(cep));
            }

            var endereco = await _cepService.ConsultarCepAsync(cepLimpo);

            if (endereco == null)
            {
                return null;
            }

            
            var response = new EnderecoResponse 
            {
                Cep = endereco.Cep,
                Logradouro = endereco.Logradouro,
                Complemento = endereco.Complemento,
                Bairro = endereco.Bairro,
                Localidade = endereco.Localidade, 
                Uf = endereco.Uf
            };

            return response;
        }
    }
}