using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsultaCep.Domain.Entities; // Importa a Entidade Endereco
using System.Threading.Tasks;

namespace ConsultaCep.Domain.Interfaces
{
    public interface ICepService
    {
        //Dado um CEP (string), retorne um Endereco (Entidade)
        Task<Endereco?>ConsultarCepAsync(string cep);
    }
}
/*
O retorno 'null' (CEP não encontrado) viola a garantia de não-nulidade de Task<Endereco>. 
Solução: Corrigir ICepService.cs para retornar Task<Endereco?> (anulável).
*/