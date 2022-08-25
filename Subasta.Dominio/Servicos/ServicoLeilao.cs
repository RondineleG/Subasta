using FluentValidation;
using Subasta.Dominio.Dtos;
using Subasta.Dominio.Entidades;
using Subasta.Dominio.Interfaces.Repositorios;
using Subasta.Dominio.Interfaces.Servicos;
using Subasta.Dominio.Validacoes;
using Subasta.Recursos;
using Subasta.Recursos.Extensoes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Subasta.Dominio.Servicos
{
    public class ServicoLeilao : IServicoLeilao
    {
        private readonly IRepositorioLeilao _repositorio;

        public ServicoLeilao(IRepositorioLeilao repositorio)
        {
            _repositorio = repositorio;
        }

        public void Adicionar(LeilaoDto leilaoDto)
        {
            ValidarLeilao(leilaoDto);

            var leilao = new Leilao(leilaoDto);
            _repositorio.Adicionar(leilao);
        }

        private void ValidarLeilao(LeilaoDto leilaoDto)
        {
            var validador = new ValidadorLeilao();
            var resultadoValidacao = validador.Validate(leilaoDto);

            if (!resultadoValidacao.IsValid)
                throw new Exception(resultadoValidacao.GetErros());
        }

        public void Encerrar(Guid leilaoId)
        {
            var leilao = _repositorio.BuscarPorId(leilaoId);

            if (leilao == null)
                throw new Exception(MensagensErro.LeilaoNaoEncontrado);

            leilao.Encerrar();
        }
    }
}
