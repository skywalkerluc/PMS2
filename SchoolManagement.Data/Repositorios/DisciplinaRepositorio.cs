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
    public class DisciplinaRepositorio : RepositorioBase<Disciplina>, IDisciplinaRepositorio
    {
        private readonly TurmaRepositorio _turmaRep;

        public Disciplina IncluirDisciplina(Disciplina disciplina)
        {
            try
            {
                if (disciplina.Livros.Count > 0)
                {
                    foreach (var item in disciplina.Livros)
                    {
                        Db.Entry(item).State = EntityState.Unchanged;
                    }
                }

                Db.Disciplinas.Add(disciplina);
                Db.SaveChanges();

                return disciplina;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message.ToString());
            }
        }

        public IEnumerable<Disciplina> FiltroDisciplina(string nomeDisciplina, int LivroId)
        {

            var disciplinaFiltro = from d in Db.Disciplinas
                                   where d.Livros.All(l => l.LivroId == LivroId || LivroId == null)
                                   where d.NomeDisciplina == nomeDisciplina || nomeDisciplina == string.Empty || nomeDisciplina == null
                                   select d;

            IEnumerable<Disciplina> RetornoDisciplina = disciplinaFiltro;
            return RetornoDisciplina;
        }

        public IEnumerable<Disciplina> BuscarPorNome(string nome)
        {
            return Db.Disciplinas.Where(p => p.NomeDisciplina.Contains(nome));
        }

        public IEnumerable<Disciplina> RecuperarDisciplinasTurma(int TurmaId)
        {
            List<Disciplina> ListaRetorno = new List<Disciplina>();
            var TurmaIdParam = new SqlParameter("@TurmaId", TurmaId);
            var valor = this.Db.Set<Disciplina>().SqlQuery("SELECT * FROM Disciplina JOIN DisciplinaTurma ON Disciplina.DisciplinaId = DisciplinaTurma.Disciplina_DisciplinaId WHERE DisciplinaTurma.Turma_TurmaId = @TurmaId", TurmaIdParam);

            if (valor != null)
            {
                foreach (var value in valor)
                {
                    ListaRetorno.Add(value);
                }
            }
            return ListaRetorno;
        }

        public bool IncluirDisciplinasEmTurma(int TurmaId, List<Disciplina> ListaDisciplinas)
        {
            try
            {
                foreach (var disc in ListaDisciplinas)
                {
                    var turmaParameter = new SqlParameter("@TurmaId", TurmaId);
                    var disciplinaParameter = new SqlParameter("@DisciplinaId", disc.DisciplinaId);

                    var query = Db.Database.ExecuteSqlCommand("INSERT INTO DisciplinaTurma (Turma_TurmaId, Disciplina_DisciplinaId) VALUES (@TurmaId, @DisciplinaId)", turmaParameter, disciplinaParameter);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoverDisciplinasTurma(int TurmaId, List<Disciplina> ListaDisciplinas)
        {
            try
            {
                foreach (var disc in ListaDisciplinas)
                {
                    var turmaParameter = new SqlParameter("@TurmaId", TurmaId);
                    var disciplinaParameter = new SqlParameter("@DisciplinaId", disc.DisciplinaId);

                    var query = Db.Database.ExecuteSqlCommand("DELETE FROM DisciplinaTurma WHERE Turma_TurmaId = @TurmaId AND Disciplina_DisciplinaId = @DisciplinaId", turmaParameter, disciplinaParameter);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
