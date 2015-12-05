using SchoolManagement.Domain.Entidades;
using SchoolManagement.Domain.Interfaces.Repositorios;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Data.Repositorios
{
    public class ConteudosExtrasRepositorio : RepositorioBase<ConteudosExtras>, IConteudoExtraRepositorio
    {
        public ConteudosExtras IncluirConteudosExtras(ConteudosExtras conteudosExtras)
        {
            Db.Entry<Turma>(conteudosExtras.TurmaPublicoAlvo).State = EntityState.Unchanged;
            Db.Entry<Professor>(conteudosExtras.Professor).State = EntityState.Unchanged;
            Db.ConteudosExtras.Add(conteudosExtras);
            Db.SaveChanges();
            return conteudosExtras;
        }

        public bool EditarDadosConteudosExtras(ConteudosExtras conteudosExtras)
        {
            try
            {
                var conteudoIdParameter = new SqlParameter("@ConteudoId", conteudosExtras.ConteudoId);
                var anexoParameter = new SqlParameter("@Anexo", conteudosExtras.Anexo);
                var descricaoParameter = new SqlParameter("@Descricao", conteudosExtras.Descricao);
                var turmaIdParameter = new SqlParameter("@TurmaId", conteudosExtras.TurmaPublicoAlvo.TurmaId);

                var query = this.Db.Database.ExecuteSqlCommand("UPDATE ConteudosExtras SET Anexo = @Anexo, Descricao = @Descricao, TurmaPublicoAlvo_TurmaId = @TurmaId WHERE ConteudoId = @ConteudoId", conteudoIdParameter, anexoParameter, descricaoParameter, turmaIdParameter);
                return true;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message.ToString());
            }
        }

        public IEnumerable<ConteudosExtras> RecuperarConteudosExtrasProfessor(int ProfessorId)
        {
            try
            {
                var query = from c in Db.ConteudosExtras
                            where c.Professor.Id.Equals(ProfessorId)
                            select c;
                return query;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message.ToString());
            }
        }

        public IEnumerable<ConteudosExtras> RecuperarConteudosExtrasTurma(int TurmaId)
        {
            var cont = from c in Db.ConteudosExtras
                       where c.TurmaPublicoAlvo.TurmaId == TurmaId
                       select c;
            return cont;
        }

        public IEnumerable<ConteudosExtras> RecuperarProvasProfessor(int ProfessorId)
        {
            var provas = from c in Db.ConteudosExtras
                         where c.Professor.Id.Equals(ProfessorId)
                         select c;
            return provas;
        }
    }
}
