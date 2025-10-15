using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsultaCep.Domain.Entities; // Importa a Entidade Endereco
using System.Threading.Tasks;

namespace ConsultaCep.Domain.Interfaces
{
    internal interface ICepService
    {
        public interface ICepService
        {
            //Dado um CEP (string), retorne um Endereco (Entidade)
            Task<Endereco> ConsultarCepAsync(string cep);
        }
    }
}
