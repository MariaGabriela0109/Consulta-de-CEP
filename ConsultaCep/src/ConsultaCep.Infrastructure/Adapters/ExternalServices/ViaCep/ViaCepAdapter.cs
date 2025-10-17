using ConsultaCep.Domain.Entities; // Para retornar a Entidade Endereco
using ConsultaCep.Domain.Interfaces; // Para implementar a porta ICepService
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ConsultaCep.Infrastructure.Adapters.ExternalServices.ViaCep
{
    public class ViaCepAdapter : ICepService
    {
        private readonly HttpClient _httpClient;

        public ViaCepAdapter(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://viacep.com.br/ws/");
        }

        public async Task<Endereco?> ConsultarCepAsync(string cep)
        {
            var url = $"{cep.Replace("-", "").Trim()}/json/";

            try
            {
                // 1. Execução e Tratamento de Erros de Comunicação
                var response = await _httpClient.GetAsync(url);

                // Trata 404/500
                response.EnsureSuccessStatusCode();

                // 2. Desserializa o Retorno da API para o DTO
                var dto = await response.Content.ReadFromJsonAsync<ViaCepResponseDto>();

                // 3. Trata CEP Inválido ou Não Encontrado
                if (dto == null || dto.Erro)
                {
                    return null; 
                }

                // 4. Converte o DTO Externo para o Modelo Interno (Mapeamento)
                // Garante que dto.Cep não é nulo antes de Replace, usando o operador '?? ""'
                // Operador de coalescência nula (?? "") em todos os campos string
                var endereco = new Endereco(
                cep: (dto.Cep ?? "").Replace("-", ""),
                logradouro: dto.Logradouro ?? "",
                complemento: dto.Complemento ?? "",
                bairro: dto.Bairro ?? "",
                localidade: dto.Localidade ?? "",
                uf: dto.Uf ?? ""
);

                return endereco;
            }
            catch (HttpRequestException ex)
            {
                // trata falhas de rede
                throw new Exception($"Falha de comunicação ou serviço ViaCEP fora do ar: {ex.Message}", ex);
            }
        }
    }
}
