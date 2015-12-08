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

        public IEnumerable<Disciplina> RecuperarDisciplinasProfessorLeciona(int ProfessorId)
        {
            try
            {
                var ProfessorIdParameter = new SqlParameter("@ProfessorId", ProfessorId);
                var query = this.Db.Disciplinas.SqlQuery("SELECT * FROM Disciplina AS D INNER JOIN ProfessorDisciplina AS PD ON D.DisciplinaId = PD.Disciplina_DisciplinaId WHERE PD.Professor_Id = @ProfessorId", ProfessorIdParameter).ToList();
                return query;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message.ToString());
            }
        }

        public bool AtualizarDadosDisciplina(Disciplina disciplina)
        {
            try
            {
                var DisciplinaIdParameter = new SqlParameter("@DisciplinaId", disciplina.DisciplinaId);
                var NomeDisciplinaParameter = new SqlParameter("@NomeDisciplina", disciplina.NomeDisciplina);

                var query = this.Db.Database.ExecuteSqlCommand("UPDATE Disciplina SET NomeDisciplina = @NomeDisciplina WHERE DisciplinaId = @DisciplinaId", NomeDisciplinaParameter, DisciplinaIdParameter);
                return true;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message.ToString());
            }
        }

        public IEnumerable<Disciplina> RecuperarDisciplinasTurmaProfessor(int TurmaId, int ProfessorId)
        {
            try
            {
                List<Disciplina> ListaRetorno = new List<Disciplina>();
                SqlConnection conn = (SqlConnection)Db.Database.Connection;
                SqlCommand command = new SqlCommand("SELECT * FROM Disciplina AS D JOIN DisciplinaTurma  AS DT ON D.DisciplinaId = DT.Disciplina_DisciplinaId JOIN ProfessorDisciplina AS PD ON PD.Disciplina_DisciplinaId = DT.Disciplina_DisciplinaId WHERE PD.Professor_Id = " + ProfessorId + " AND DT.Turma_TurmaId = " + TurmaId, conn);
                conn.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Disciplina result = new Disciplina()
                        {
                            DisciplinaId = reader.GetInt32(0),
                            NomeDisciplina = reader.GetString(1)
                        };
                        ListaRetorno.Add(result);
                    }
                    conn.Close();
                    return ListaRetorno;
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message.ToString());
            }
        }
    }
}
