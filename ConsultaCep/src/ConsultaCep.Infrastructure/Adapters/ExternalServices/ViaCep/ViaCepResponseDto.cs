using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*  Propriedades string do ViaCepResponseDto
    (l. 11-20) devem ser anuláveis (?), pois o construtor padrão
    não garante a atribuição de valor
    (falha na garantia de não-nulidade pelo compilador).
    Isso resolve o erro e lida com falhas de desserialização/dados 
    faltantes da API externa.*/

namespace ConsultaCep.Infrastructure.Adapters.ExternalServices.ViaCep
{
    internal class ViaCepResponseDto
    {
        public string? Cep { get; set; }
        public string? Logradouro { get; set; }
        public string? Complemento { get; set; }
        public string? Bairro { get; set; }
        public string? Localidade { get; set; } 
        public string? Uf { get; set; }
        public string? Ibge { get; set; }
        public string? Gia { get; set; }
        public string? Ddd { get; set; }
        public string? Siafi { get; set; }
        public bool Erro { get; set; } //cep não encontrado
    }
}
