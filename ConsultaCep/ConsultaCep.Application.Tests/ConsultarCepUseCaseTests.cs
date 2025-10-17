using Xunit;
using Moq;
using System.Threading.Tasks;
using System;
using ConsultaCep.Domain.Interfaces;
using ConsultaCep.Domain.Entities;
using ConsultaCep.Application.UseCases;
using ConsultaCep.Application.DTOs; 

namespace ConsultaCep.Application.Tests
{
    //classe que valida o Caso de Uso
    public class ConsultarCepUseCaseTests
    {
        [Fact]
        public async Task ExecuteAsync_CepValido_DeveRetornarEnderecoMapeadoCorretamente()
        {
            var cepDeTeste = "30130010";

            //Mock (objeto falso) da Porta de Saída (ICepService)
            var mockCepService = new Mock<ICepService>();

            //Entidade de Domínio que o Adapter Mockado deve retornar
            var enderecoEntity = new Endereco(
                cep: cepDeTeste,
                logradouro: "Av. Afonso Pena",
                complemento: "Lado Par",
                bairro: "Centro",
                localidade: "Belo Horizonte",
                uf: "MG"
            );

            //Quando o método ConsultarCepAsync for chamado, 
            // retorne a Entidade que acabamos de criar.
            mockCepService.Setup(s => s.ConsultarCepAsync(cepDeTeste))
                          .ReturnsAsync(enderecoEntity);

            var useCase = new ConsultarCepUseCase(mockCepService.Object);

            var resultado = await useCase.ExecuteAsync(cepDeTeste);

            Assert.NotNull(resultado);

            Assert.Equal("Belo Horizonte", resultado.Localidade);
            Assert.Equal("MG", resultado.Uf);
            Assert.Equal(cepDeTeste, resultado.Cep);

            //Verifica que o Adapter Mockado foi chamado exatamente uma vez
            mockCepService.Verify(s => s.ConsultarCepAsync(cepDeTeste), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_CepNaoEncontrado_DeveRetornarNull()
        {          
            var mockCepService = new Mock<ICepService>();
            mockCepService.Setup(s => s.ConsultarCepAsync(It.IsAny<string>()))
                          .ReturnsAsync((Endereco?)null); 

            var useCase = new ConsultarCepUseCase(mockCepService.Object);

            var resultado = await useCase.ExecuteAsync("99999999");

            Assert.Null(resultado);
            mockCepService.Verify(s => s.ConsultarCepAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_CepComMenosDe8Digitos_DeveLancarArgumentException()
        {
            var cepInvalido = "123";
            var mockCepService = new Mock<ICepService>();
            var useCase = new ConsultarCepUseCase(mockCepService.Object);

            await Assert.ThrowsAsync<ArgumentException>(() => useCase.ExecuteAsync(cepInvalido));

            mockCepService.Verify(s => s.ConsultarCepAsync(It.IsAny<string>()), Times.Never);
        }
    }
}