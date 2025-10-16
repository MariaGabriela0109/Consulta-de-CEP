using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultaCep.Application.DTOs
{
    public record EnderecoResponse(
        string Cep,
        string Logradouro,
        string Complemento,
        string Bairro,
        string Localidade,
        string Uf
    );
}
