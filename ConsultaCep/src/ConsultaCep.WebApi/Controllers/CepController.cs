using ConsultaCep.Application.DTOs;
using ConsultaCep.Application.Interfaces;
using ConsultaCep.Application.UseCases;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ConsultaCep.WebApi.Controllers
{

    /// <summary>
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CepController : ControllerBase
    {
        private readonly IConsultarCepUseCase _consultarCepUseCases;
        /// <param name="consultarCepUseCases">A interface do Caso de Uso de Consulta.</param>
        public CepController(IConsultarCepUseCase consultarCepUseCases)
        {
            _consultarCepUseCases = consultarCepUseCases;
        }

        /// <param name="cep">O CEP a ser consultado (apenas números).</param>
        [HttpGet("{cep}")]
        [ProducesResponseType(typeof(EnderecoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(string cep)
        {
            try
            {
                var enderecoResponse = await _consultarCepUseCases.ExecuteAsync(cep);

                if (enderecoResponse == null)
                {
                    return NotFound($"CEP {cep} não encontrado ou inválido."); 
                }

                return Ok(enderecoResponse); 
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message); 
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno ao consultar o serviço de CEP."); 
            }
        }
    }
}