using SchoolManagement.Domain.Cross;
using SchoolManagement.Domain.Entidades;
using SchoolManagement.Domain.Interfaces.Repositorios;
using System;

namespace SchoolManagement.Data.Repositorios.NewReps
{
    public class AnoLetivoRepositorio : RepositorioBase, IAnoLetivoRepositorio
    {
        public RetornoBase<bool> Atualizar(AnoLetivo param)
        {
            throw new NotImplementedException();
        }

        public RetornoBase<bool> Incluir(AnoLetivo param)
        {
            throw new NotImplementedException();
        }

        public RetornoBase Recuperar(int id)
        {
            throw new NotImplementedException();
        }

        public RetornoBase<AnoLetivo> RecuperarTodos()
        {
            throw new NotImplementedException();
        }

        public RetornoBase<bool> Remover(AnoLetivo param)
        {
            throw new NotImplementedException();
        }
    }
}
