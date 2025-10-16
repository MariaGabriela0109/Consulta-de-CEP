using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultaCep.Domain.Entities
{

    public
        class Endereco
    {
        public string Cep { get; private set; }
        public string Logradouro { get; private set; }
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string Localidade { get; private set; }
        public string Uf { get; private set; }

        public Endereco(string cep, string logradouro, string complemento, string bairro, string localidade, string uf)
        {
            if (string.IsNullOrWhiteSpace(cep))//validação de dominio 
                throw new ArgumentException("O CEP não pode ser nulo ou vazio.", nameof(cep));

            Cep = cep;
            Logradouro = logradouro;
            Complemento = complemento;
            Bairro = bairro;
            Localidade = localidade;
            Uf = uf;
        }


    }

}
