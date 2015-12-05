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
    public class AlunoServico : ServicoBase<Aluno>, IAlunoServico
    {
        private readonly IAlunoRepositorio _alunoRepositorio;

        public AlunoServico(IAlunoRepositorio alunoRepositorio)
            :base(alunoRepositorio)
        {
            _alunoRepositorio = alunoRepositorio;
        }

        public Aluno IncluirAluno(Aluno param)
        {
            return this._alunoRepositorio.IncluirAluno(param);
        }

        public IEnumerable<Aluno> PesquisarAlunoPorNome(string nomeAluno)
        {
            return this._alunoRepositorio.PesquisarAlunoPorNome(nomeAluno);
        }

        public IEnumerable<Aluno> FiltrarAluno(string nomeAluno, int? turmaId)
        {
            return _alunoRepositorio.FiltrarAluno(nomeAluno, turmaId);
        }

        public IEnumerable<Aluno> PesquisarAlunoPorNomeEmTurma(string nomeAluno, int codigoTurma)
        {
            return this._alunoRepositorio.PesquisarAlunoPorNomeEmTurma(nomeAluno, codigoTurma);
        }

        public IEnumerable<Aluno> RecuperarAlunosTurma(int TurmaId)
        {
            return this._alunoRepositorio.RecuperarAlunosTurma(TurmaId);
        }

        public IEnumerable<ResultadosProvas> RecuperarResultadosAluno(Aluno aluno)
        {
            return this._alunoRepositorio.RecuperarResultadosAluno(aluno);
        }


        
    }
}
