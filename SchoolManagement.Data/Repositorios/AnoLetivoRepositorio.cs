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
    }
}
