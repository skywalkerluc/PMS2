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
    public class ProvaRepositorio : RepositorioBase<Prova>, IProvaRepositorio
    {
        public Prova IncluirProva(Prova prova)
        {
            Db.Entry(prova.Disciplina).State = EntityState.Unchanged;
            Db.Entry(prova.Professores).State = EntityState.Unchanged;
            Db.Provas.Add(prova);
            Db.SaveChanges();

            return prova;
        }

        public IEnumerable<Prova> BuscarPorDisciplina(int codDisciplina)
        {
            return Db.Provas.Where(p => p.Disciplina.DisciplinaId == codDisciplina);
        }

        public IEnumerable<Prova> RecuperarProvasProfessor(int ProfessorId)
        {
            var provas = from p in Db.Provas
                         where p.Professores.Id.Equals(ProfessorId)
                         select p;
            return provas;
        }

        public IEnumerable<Prova> RecuperarProvasTurma(int TurmaId)
        {
            var provas = from p in Db.Provas
                         where p.Turma.TurmaId == TurmaId
                         select p;
            return provas;
        }

    }
}
