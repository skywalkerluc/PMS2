using SchoolManagement.Domain.Cross;
using SchoolManagement.Domain.Entidades;
using SchoolManagement.Domain.Interfaces.Repositorios;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Data.Repositorios.NewReps
{
    public class ConteudosExtrasRepositorio : RepositorioBase, IConteudosExtrasRepositorio
    {
        public RetornoBase<bool> Atualizar(ConteudosExtras param)
        {
            throw new NotImplementedException();
        }

        public RetornoBase<bool> Incluir(ConteudosExtras param)
        {
            throw new NotImplementedException();
        }

        public RetornoBase Recuperar(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ConteudosExtras> RecuperarConteudosExtrasProfessor(int ProfessorId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ConteudosExtras> RecuperarConteudosExtrasTurma(int TurmaId)
        {
            throw new NotImplementedException();
        }

        public RetornoBase<ConteudosExtras> RecuperarTodos()
        {
            throw new NotImplementedException();
        }

        public RetornoBase<bool> Remover(ConteudosExtras param)
        {
            throw new NotImplementedException();
        }
    }
}
