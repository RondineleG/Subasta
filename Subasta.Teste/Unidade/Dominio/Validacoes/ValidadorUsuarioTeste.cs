using FluentAssertions;
using FluentValidation.TestHelper;
using Subasta.Dominio.Dtos;
using Subasta.Dominio.Validacoes;
using Subasta.Teste.Builders;
using System;
using Xunit;

namespace Subasta.Teste.Unidade.Dominio.Validacoes
{
    public class ValidadorUsuarioTeste
    {
        private readonly ValidadorUsuario _validador;

        public ValidadorUsuarioTeste()
        {
            _validador = new ValidadorUsuario();
        }

        [Fact]
        public void Validate_deve_retornar_invalido_quando_usuario_nao_tiver_nome()
        {
            //arrange
            var usuarioInvalido = new UsuarioBuilder()
                .ComNome(string.Empty)
                .ConstruirDto();

            //act
            var resultado = _validador.Validate(usuarioInvalido);

            //assert
            resultado.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Validate_deve_retornar_invalido_quando_usuario_nao_tiver_login()
        {
            //arrange
            var usuarioInvalido = new UsuarioBuilder()
                .ComLogin(string.Empty)
                .ConstruirDto();

            //act
            var resultado = _validador.Validate(usuarioInvalido);

            //assert
            resultado.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Validate_deve_retornar_invalido_quando_usuario_nao_tiver_senha()
        {
            //arrange
            var usuarioInvalido = new UsuarioBuilder()
                .ComSenha(string.Empty)
                .ConstruirDto();

            //act
            var resultado = _validador.Validate(usuarioInvalido);

            //assert
            resultado.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Validate_deve_retornar_invalido_quando_usuario_nao_tiver_email()
        {
            //arrange
            var usuarioInvalido = new UsuarioBuilder()
                .ComEmail(string.Empty)
                .ConstruirDto();

            //act
            var resultado = _validador.Validate(usuarioInvalido);

            //assert
            resultado.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Validate_deve_retornar_invalido_quando_data_for_anterior_a_130_anos()
        {
            //arrange
            var usuarioInvalido = new UsuarioBuilder()
                .ComDataNascimento(DateTime.MinValue)
                .ConstruirDto();

            //act
            var resultado = _validador.Validate(usuarioInvalido);

            //assert
            resultado.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Validate_deve_retornar_invalido_quando_data_for_anterior_a_18_anos()
        {
            //arrange
            var usuarioInvalido = new UsuarioBuilder()
                .ComDataNascimento(DateTime.Today.AddYears(-17))
                .ConstruirDto();

            //act
            var resultado = _validador.Validate(usuarioInvalido);

            //assert
            resultado.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Validate_deve_retornar_valido_quando_data_for_posterior_a_130_anos()
        {
            //arrange
            var usuarioInvalido = new UsuarioBuilder()
                .ComDataNascimento(DateTime.Today.AddYears(-129))
                .ConstruirDto();

            //act
            var resultado = _validador.Validate(usuarioInvalido);

            //assert
            resultado.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Validate_deve_retornar_invalido_quando_a_senha_nao_tiver_seis_caracteres()
        {
            //arrange
            var usuarioInvalido = new UsuarioBuilder()
                .ComSenha("Mm1")
                .ConstruirDto();

            //act
            var resultado = _validador.Validate(usuarioInvalido);

            //assert
            resultado.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Validate_deve_retornar_invalido_quando_a_senha_nao_tiver_letra_maiuscula()
        {
            //arrange
            var usuarioInvalido = new UsuarioBuilder()
                .ComSenha("zxcvbn1")
                .ConstruirDto();

            //act
            var resultado = _validador.Validate(usuarioInvalido);

            //assert
            resultado.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Validate_deve_retornar_invalido_quando_a_senha_nao_tiver_letra_minuscula()
        {
            //arrange
            var usuarioInvalido = new UsuarioBuilder()
                .ComSenha("ZXCVBN1")
                .ConstruirDto();

            //act
            var resultado = _validador.Validate(usuarioInvalido);

            //assert
            resultado.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Validate_deve_retornar_invalido_quando_a_senha_nao_tiver_um_numero()
        {
            //arrange
            var usuarioInvalido = new UsuarioBuilder()
                .ComSenha("Zxcvbnm")
                .ConstruirDto();

            //act
            var resultado = _validador.Validate(usuarioInvalido);

            //assert
            resultado.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Validate_deve_retornar_valido_quando_o_usuario_estiver_cumprindo_os_requisitos()
        {
            //arrange
            var usuario = new UsuarioBuilder().ConstruirDto();

            //act
            var resultado = _validador.Validate(usuario);

            //assert
            resultado.IsValid.Should().BeTrue();
        }
    }
}
