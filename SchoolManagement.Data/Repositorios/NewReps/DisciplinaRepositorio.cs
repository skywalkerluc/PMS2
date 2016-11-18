using SchoolManagement.Domain.Cross;
using SchoolManagement.Domain.Entidades;
using SchoolManagement.Domain.Interfaces.Repositorios;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Data.Repositorios.NewReps
{
    public class DisciplinaRepositorio : RepositorioBase, IDisciplinaRepositorio
    {
        public RetornoBase<bool> Atualizar(Disciplina param)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Disciplina> BuscarPorNome(string nome)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Disciplina> FiltroDisciplina(string nomeDisciplina, int LivroId)
        {
            throw new NotImplementedException();
        }

        public RetornoBase<bool> Incluir(Disciplina param)
        {
            throw new NotImplementedException();
        }

        public bool IncluirDisciplinasEmTurma(int TurmaId, List<Disciplina> ListaDisciplinas)
        {
            throw new NotImplementedException();
        }

        public RetornoBase Recuperar(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Disciplina> RecuperarDisciplinasProfessorLeciona(int ProfessorId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Disciplina> RecuperarDisciplinasTurma(int TurmaId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Disciplina> RecuperarDisciplinasTurmaProfessor(int TurmaId, int ProfessorId)
        {
            throw new NotImplementedException();
        }

        public RetornoBase<Disciplina> RecuperarTodos()
        {
            throw new NotImplementedException();
        }

        public RetornoBase<bool> Remover(Disciplina param)
        {
            throw new NotImplementedException();
        }

        public bool RemoverDisciplinasTurma(int TurmaId, List<Disciplina> ListaDisciplinas)
        {
            throw new NotImplementedException();
        }
    }
}
