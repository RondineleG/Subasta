using FluentAssertions;
using Subasta.Dominio.Dtos;
using Subasta.Dominio.Validacoes;
using Subasta.Teste.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Subasta.Teste.Unidade.Dominio.Validacoes
{
    public class ValidadorLeilaoTeste
    {
        private readonly ValidadorLeilao _validador;

        public ValidadorLeilaoTeste()
        {
            _validador = new ValidadorLeilao();
        }

        [Fact]
        public void Validate_deve_retornar_invalido_quando_leilao_nao_tiver_data_inicio_no_futuro()
        {
            //arrange
            var leilao = new LeilaoBuilder()
                .ComDataInicio(DateTime.Now.AddHours(-1))
                .ConstruirDto();

            //act
            var resultado = _validador.Validate(leilao);

            //assert
            resultado.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Validate_deve_retornar_invalido_quando_leilao_nao_tiver_pelo_menos_um_item()
        {
            //arrange
            var leilao = new LeilaoBuilder()
                .ComItens(new List<ItemDto>())
                .ConstruirDto();

            //act
            var resultado = _validador.Validate(leilao);

            //assert
            resultado.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Validate_deve_retornar_valido_quando_o_leilao_cumprir_com_as_regras()
        {
            //arrange
            var leilao = new LeilaoBuilder().ConstruirDto();

            //act
            var resultado = _validador.Validate(leilao);

            //assert
            resultado.IsValid.Should().BeTrue();
        }
    }
}
