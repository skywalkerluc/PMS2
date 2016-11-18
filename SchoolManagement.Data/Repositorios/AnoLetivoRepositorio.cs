using SchoolManagement.Domain.Entidades;
using System;
using System.Data.Entity;

namespace SchoolManagement.Data.Repositorios
{
    public class AnoLetivoRepositorio : RepositorioBase<AnoLetivo>/*, IAnoLetivoRepositorio*/
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
                if (anoLetivo.Turmas != null && anoLetivo.Turmas.Count != 0)
                {
                    Db.Entry(anoLetivo.Turmas).State = EntityState.Unchanged;
                }
                Db.AnosLetivos.Remove(anoLetivo);
                Db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool AlterarDadosAnoLetivo(AnoLetivo anoLetivo)
        {
            try
            {
                if (anoLetivo.Turmas != null && anoLetivo.Turmas.Count != 0)
                {
                    Db.Entry(anoLetivo.Turmas).State = EntityState.Unchanged;
                }
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
