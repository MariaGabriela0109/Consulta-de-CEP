using ConsultaCep.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultaCep.Application.Interfaces
{
    public interface IConsultarCepUseCase
    {
        //Dado um CEP (string), retorna o EnderecoResponse
        Task<EnderecoResponse?> ExecuteAsync(string cep);
    }
}
