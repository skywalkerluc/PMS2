using SchoolManagement.Domain.Cross;

namespace SchoolManagement.Domain.Interfaces.Repositorios
{
    public interface IRepositorioBase<TEntity> where TEntity : class
    {
        RetornoBase<bool> Incluir(TEntity param);
        RetornoBase Recuperar(int id);
        RetornoBase<TEntity> RecuperarTodos();
        RetornoBase<bool> Atualizar(TEntity param);
        RetornoBase<bool> Remover(TEntity param);
        void Dispose();
    }
}
