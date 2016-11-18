using SchoolManagement.Domain.Cross;
using SchoolManagement.Domain.Entidades;
using SchoolManagement.Domain.Interfaces.Repositorios;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Data.Repositorios.NewReps
{
    public class AlunoRepositorio : RepositorioBase, IAlunoRepositorio
    {
        public RetornoBase<bool> Atualizar(Aluno param)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Aluno> FiltrarAluno(string nomeAluno, int? turmaId)
        {
            throw new NotImplementedException();
        }

        public RetornoBase<bool> Incluir(Aluno param)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Aluno> PesquisarAlunoPorNome(string nomeAluno)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Aluno> PesquisarAlunoPorNomeEmTurma(string nomeAluno, int codigoTurma)
        {
            throw new NotImplementedException();
        }

        public RetornoBase Recuperar(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Aluno> RecuperarAlunosTurma(int TurmaId)
        {
            throw new NotImplementedException();
        }

        public RetornoBase<Aluno> RecuperarTodos()
        {
            throw new NotImplementedException();
        }

        public RetornoBase<bool> Remover(Aluno param)
        {
            throw new NotImplementedException();
        }
    }
}
