using SchoolManagement.Data.Contexto;
using SchoolManagement.Domain.Cross;
using SchoolManagement.Domain.Interfaces.Repositorios;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace SchoolManagement.Data.Repositorios
{
    public class RepositorioBase<TEntity> : IDisposable, IRepositorioBase<TEntity> where TEntity : class
    {
        protected SchoolManagementContext Db = new SchoolManagementContext();
        public TEntity Incluir(TEntity param)
        {
            try
            {
                Db.Set<TEntity>().Add(param);
                Db.SaveChanges();
                return param;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message.ToString());
            }
        }

        public TEntity Recuperar(int id)
        {
            return Db.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> RecuperarTodos()
        {
            //you can remove AsNoTracking Later
            return Db.Set<TEntity>().AsNoTracking().ToList();
        }

        public bool Atualizar(TEntity param)
        {
            try
            {
                Db.Entry(param).State = EntityState.Modified;
                Db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }   
        }

        public bool Remover(TEntity param)
        {
            try
            {
                Db.Set<TEntity>().Remove(param);
                Db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        public void Dispose()
        {
            this.Dispose();
        }

        #region Métodos específicos de todos os usuários

        public bool AtualizarNivelDePermissao(int idUsuario, int novoNivelPermissao)
        {
            try
            {
                var sqlUpdateQuery = Db.Set<TEntity>().SqlQuery("", idUsuario, novoNivelPermissao);
                Db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;

            }
        }

        RetornoBase<bool> IRepositorioBase<TEntity>.Incluir(TEntity param)
        {
            throw new NotImplementedException();
        }

        RetornoBase IRepositorioBase<TEntity>.Recuperar(int id)
        {
            throw new NotImplementedException();
        }

        RetornoBase<TEntity> IRepositorioBase<TEntity>.RecuperarTodos()
        {
            throw new NotImplementedException();
        }

        RetornoBase<bool> IRepositorioBase<TEntity>.Atualizar(TEntity param)
        {
            throw new NotImplementedException();
        }

        RetornoBase<bool> IRepositorioBase<TEntity>.Remover(TEntity param)
        {
            throw new NotImplementedException();
        }



        #endregion

    }
}
