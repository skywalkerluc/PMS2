using SchoolManagement.Domain.Entidades;
using SchoolManagement.Domain.Interfaces.Repositorios;
using SchoolManagement.Domain.Interfaces.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Servicos
{
    public class ResponsavelServico : ServicoBase<Responsavel>, IResponsavelServico
    {
        private readonly IResponsavelRepositorio _responsavelRepositorio;

        public ResponsavelServico(IResponsavelRepositorio responsavelRepositorio)
            :base(responsavelRepositorio)
        {
            _responsavelRepositorio = responsavelRepositorio;
        }

        public Responsavel IncluirResponsavel(Responsavel responsavel)
        {
            return _responsavelRepositorio.IncluirResponsavel(responsavel);
        }

        public bool CriarRelacaoResponsavelAluno(int ResponsavelId, int AlunoId)
        {
            return _responsavelRepositorio.CriarRelacaoResponsavelAluno(ResponsavelId, AlunoId);
        }

        public IEnumerable<Responsavel> PesquisarResponsavelPorNome(string nomeResponsavel)
        {
            return _responsavelRepositorio.PesquisarResponsavelPorNome(nomeResponsavel);
        }

        public IEnumerable<Aluno> ExibirDadosAlunoRelacionado(int responsavelId)
        {
            return _responsavelRepositorio.ExibirDadosAlunoRelacionado(responsavelId);
        }

        public IEnumerable<Responsavel> FiltrarResponsavel(string nomeResponsavel, int idAluno)
        {
            return _responsavelRepositorio.FiltrarResponsavel(nomeResponsavel, idAluno);
        }
    }
}
