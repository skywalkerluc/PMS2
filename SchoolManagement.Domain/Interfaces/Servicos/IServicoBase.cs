using System.Collections.Generic;

namespace SchoolManagement.Domain.Interfaces.Servicos
{
    public interface IServicoBase <TEntity> where TEntity : class
    {
        TEntity Incluir(TEntity param);
        TEntity Recuperar(int id);
        IEnumerable<TEntity> RecuperarTodos();
        bool Atualizar(TEntity param);
        bool Remover(TEntity param);
        void Dispose();
    }
}
