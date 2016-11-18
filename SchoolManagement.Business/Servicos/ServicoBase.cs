using SchoolManagement.Domain.Interfaces.Repositorios;
using SchoolManagement.Domain.Interfaces.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Servicos
{
    public class ServicoBase<TEntity> : IDisposable, IServicoBase<TEntity> where TEntity : class
    {
        private readonly IRepositorioBase<TEntity> _repositorio;
        public ServicoBase(IRepositorioBase<TEntity> repositorio)
        {
            _repositorio = repositorio;
        }

        public TEntity Incluir(TEntity param)
        {
            return _repositorio.Incluir(param);
        }

        public TEntity Recuperar(int id)
        {
            return _repositorio.Recuperar(id);
        }

        public IEnumerable<TEntity> RecuperarTodos()
        {
            return _repositorio.RecuperarTodos();
        }

        public bool Atualizar(TEntity param)
        {
            return _repositorio.Atualizar(param);
        }

        public bool Remover(TEntity param)
        {
            return _repositorio.Remover(param);
        }

        public void Dispose()
        {
            _repositorio.Dispose();
        }
    }
}
