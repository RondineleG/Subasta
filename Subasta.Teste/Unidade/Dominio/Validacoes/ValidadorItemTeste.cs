using FluentAssertions;
using Subasta.Dominio.Validacoes;
using Subasta.Teste.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Subasta.Teste.Unidade.Dominio.Validacoes
{
    public class ValidadorItemTeste
    {
        private readonly ValidadorItem _validador;

        public ValidadorItemTeste()
        {
            _validador = new ValidadorItem();
        }

        [Fact]
        public void Validate_deve_retornar_invalido_para_item_sem_nome()
        {
            //arrange
            var itemInvalido = new ItemBuilder()
                .ComNome(string.Empty)
                .ConstruirDto();

            //act
            var resultado = _validador.Validate(itemInvalido);

            //assert
            resultado.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Validate_deve_retornar_invalido_para_item_sem_descricao()
        {
            //arrange
            var itemInvalido = new ItemBuilder()
                .ComDescricao(string.Empty)
                .ConstruirDto();

            //act
            var resultado = _validador.Validate(itemInvalido);

            //assert
            resultado.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Validate_deve_retornar_invalido_para_item_com_valor_inicial_invalido()
        {
            //arrange
            var itemInvalido = new ItemBuilder()
                .ComValorInicial(0)
                .ConstruirDto();

            //act
            var resultado = _validador.Validate(itemInvalido);

            //assert
            resultado.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Validate_deve_retornar_valido_para_item_valido()
        {
            //arrange
            var itemInvalido = new ItemBuilder().ConstruirDto();

            //act
            var resultado = _validador.Validate(itemInvalido);

            //assert
            resultado.IsValid.Should().BeTrue();
        }
    }
}
