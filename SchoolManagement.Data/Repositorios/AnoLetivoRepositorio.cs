using SchoolManagement.Domain.Entidades;
using SchoolManagement.Domain.Interfaces.Repositorios;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Data.Repositorios
{
    public class AnoLetivoRepositorio : RepositorioBase<AnoLetivo>, IAnoLetivoRepositorio
    {
        public AnoLetivo IncluirAnoLetivo(AnoLetivo anoLetivo)
        {
            try
            {
                foreach (var turma in anoLetivo.Turmas)
                {
                    Db.Entry(turma).State = EntityState.Unchanged;
                }
                Db.AnosLetivos.Add(anoLetivo);
                Db.SaveChanges();
                return anoLetivo;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message.ToString());
            }
        }

        public bool RemoverAnoLetivo(int AnoLetivoId)
        {
            try
            {
                var anoLetivo = this.Recuperar(AnoLetivoId);
                Db.Entry(anoLetivo.Turmas).State = EntityState.Unchanged;
                Db.AnosLetivos.Remove(anoLetivo);
                Db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool AlterarDadosAnoLetivo(AnoLetivo anoLetivo)
        {
            try
            {
                Db.Entry(anoLetivo.Turmas).State = EntityState.Unchanged;
                Db.Entry(anoLetivo).State = EntityState.Modified;
                Db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
