using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
