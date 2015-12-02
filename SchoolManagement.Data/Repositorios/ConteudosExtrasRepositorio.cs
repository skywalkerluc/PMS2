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
