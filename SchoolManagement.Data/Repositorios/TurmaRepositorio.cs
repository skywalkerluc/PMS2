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
    public class TurmaRepositorio : RepositorioBase<Turma>, ITurmaRepositorio
    {
        private readonly AnoLetivoRepositorio anoLetivoRep;
        private readonly ProfessorRepositorio professorRep;

        public Turma IncluirTurma(Turma turma)
        {
            try
            {
                foreach (var item in turma.Disciplinas)
                {
                    Db.Entry(item).State = EntityState.Unchanged;
                }
                Db.Entry(turma.AnoLetivo).State = EntityState.Unchanged;
                Db.Turmas.Add(turma);
                Db.SaveChanges();

                return turma;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message.ToString());
            }
        }

        public IEnumerable<Aluno> RecuperarTodosAlunosTurma(int TurmaId)
        {
            List<Aluno> ListaAlunos = new List<Aluno>();
            var turmaDomain = Recuperar(TurmaId);

            foreach (var aluno in turmaDomain.Alunos)
            {
                ListaAlunos.Add(aluno);
            }

            IEnumerable<Aluno> AlunosNaTurma = ListaAlunos;
            return AlunosNaTurma;
        }

        public IEnumerable<ResultadosProvas> RecuperarResultadosProvasTurma(int TurmaId)
        {
            var results = from n in Db.ResultadosProvas
                          where n.Aluno.Turma.Equals(TurmaId)
                          select n;
            return results;
        }

        public IEnumerable<Frequencia> RecuperarFrequenciasAlunosTurma(Turma turma)
        {
            var turmaDomain = Recuperar(turma.TurmaId);

            var frequencias = from a in Db.Frequencia
                              where a.Aluno.Turma.TurmaId == turmaDomain.TurmaId
                              select a;
            return frequencias;
        }

        public IEnumerable<Turma> FiltrarTurma(string descTurma, int ProfessorId, int AnoLetivo, int horarioId)
        {
            List<Turma> ListaTurmas = new List<Turma>();


            SqlConnection conn = (SqlConnection)Db.Database.Connection;
            SqlCommand command = new SqlCommand("SELECT * FROM Turma AS T INNER JOIN ProfessorTurma AS PT ON T.TurmaId = PT.Turma_TurmaId WHERE T.Descricao = " + descTurma + " OR " + descTurma + " IS NULL AND PT.Professor_Id = " + ProfessorId + " OR " + ProfessorId + " IS NULL AND T.AnoLetivo_AnoLetivoId = " + AnoLetivo + " OR " + AnoLetivo + " IS NULL AND T.HorariosTurmaId = " + horarioId + " OR " + horarioId + " IS NULL", conn);
            conn.Open();

            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    List<Professor> ListaProfessores = new List<Professor>();
                    Professor professor = new Professor();
                    professor = (new ProfessorRepositorio().Recuperar(reader.GetInt32(5)));
                    ListaProfessores.Add(professor);

                    Turma turma = new Turma()
                    {
                        TurmaId = reader.GetInt32(0),
                        Descricao = reader.GetString(1),
                        HorariosTurmaId = reader.GetInt32(2),
                        Vagas = reader.GetInt32(3),
                        AnoLetivo = (new AnoLetivoRepositorio().Recuperar(reader.GetInt32(4))),
                        Professores = ListaProfessores
                    };
                    ListaTurmas.Add(turma);
                }
                conn.Close();
                return ListaTurmas;
            }
            else
            {
                throw new NotImplementedException();
            }

        }

        public bool RemoverAlunosTurma(int TurmaId, List<Aluno> ListaAlunos)
        {
            try
            {
                foreach (var aluno in ListaAlunos)
                {
                    var AlunoParameter = new SqlParameter("@AlunoId", aluno.Id);
                    var query = Db.Set<Aluno>().SqlQuery("UPDATE Aluno SET Turma_TurmaId = NULL WHERE Aluno.Id = @AlunoId", AlunoParameter);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool AdicionarAlunosTurma(int TurmaId, List<Aluno> ListaAlunos)
        {
            try
            {
                foreach (var aluno in ListaAlunos)
                {
                    var AlunoParameter = new SqlParameter("@AlunoId", aluno.Id);
                    var TurmaParameter = new SqlParameter("@TurmaId", TurmaId);
                    var query = Db.Set<Aluno>().SqlQuery("UPDATE Aluno SET Turma_TurmaId = @TurmaId WHERE Aluno.Id = @AlunoId", TurmaParameter, AlunoParameter);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Turma> RecuperarTurmasQueProfessorLeciona(int professorId)
        {
            List<Turma> ListaTurmas = new List<Turma>();
            SqlConnection conn = (SqlConnection)Db.Database.Connection;
            SqlCommand command = new SqlCommand("SELECT * FROM ProfessorTurma AS PT WHERE PT.Professor_Id = " + professorId, conn);
            conn.Open();

            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Turma turma = this.RecuperarDadosTurma(reader.GetInt32(1));
                    ListaTurmas.Add(turma);
                }
                conn.Close();
                return ListaTurmas;
            }
            else
            {
                return ListaTurmas;
            }

        }

        public IEnumerable<Turma> RecuperarTurmasProfessorNaoLeciona(int ProfessorId)
        {
            List<Turma> ListaRetorno = new List<Turma>();
            List<Turma> TurmaRecuperada = new List<Turma>();
            try
            {
                
                var turmas = this.RecuperarTodos();
                List<Dictionary<int, int>> ListaRecuperada = new List<Dictionary<int, int>>();

                foreach (var turma in turmas)
                {
                    var turmaProfDic = this.RecuperarProfessoresTurma(turma.TurmaId);
                    foreach (var item in turmaProfDic)
                    {
                        Turma turmaY = new Turma();
                        turmaY = this.Recuperar(item.Values.ToList()[0]);
                        turmaY.Professores.Add((new ProfessorRepositorio().Recuperar(item.Keys.ToList()[0])));
                        TurmaRecuperada.Add(turmaY);
                    }
                }

                foreach (var turmaX in TurmaRecuperada)
                {
                    if (turmaX.Professores == null)
                    {
                        ListaRetorno.Add(turmaX);
                    }
                    else
                    {
                        foreach (var prof in turmaX.Professores)
                        {
                            if (prof.Id != ProfessorId)
                            {
                                ListaRetorno.Add(turmaX);
                            }
                        }
                    }
                }
                return ListaRetorno;
            }
            catch (Exception ex)
            {
                return ListaRetorno;
            }
        }

        public Turma RecuperarDadosTurma(int TurmaId)
        {
            try
            {
                SqlConnection conn = (SqlConnection)Db.Database.Connection;
                SqlCommand command = new SqlCommand("SELECT * FROM Turma AS T WHERE T.TurmaId = " + TurmaId, conn);
                conn.Open();

                Turma turma = new Turma();


                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        turma = new Turma()
                        {
                            TurmaId = reader.GetInt32(0),
                            Descricao = reader.GetString(1),
                            HorariosTurmaId = reader.GetInt32(2),
                            Vagas = reader.GetInt32(3),
                            AnoLetivo = (new AnoLetivoRepositorio().Recuperar(reader.GetInt32(4)))
                        };
                    }
                    conn.Close();
                    return turma;
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

        public bool AtualizarDadosTurma(Turma turma)
        {
            try
            {
                SqlParameter TurmaIdParameter = new SqlParameter("@TurmaId", turma.TurmaId);
                SqlParameter DescricaoParameter = new SqlParameter("@Descrição", turma.Descricao);
                SqlParameter HorarioIdParameter = new SqlParameter("@HorarioTurmaId", turma.HorariosTurmaId);
                SqlParameter VagasParameter = new SqlParameter("@Vagas", turma.Vagas);

                var query = this.Db.Database.ExecuteSqlCommand("UPDATE Turma SET Descricao = @Descrição, HorariosTurmaId = @HorarioTurmaId, Vagas = @Vagas WHERE TurmaId = @TurmaId", TurmaIdParameter, DescricaoParameter, HorarioIdParameter, VagasParameter);
                return true;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message.ToString());
            }

        }

        public List<Dictionary<int, int>> RecuperarProfessoresTurma(int TurmaId)
        {
            List<Dictionary<int, int>> ListDict = new List<Dictionary<int, int>>();

            try
            {
                SqlConnection conn = (SqlConnection)Db.Database.Connection;
                SqlCommand command = new SqlCommand("SELECT * FROM ProfessorTurma AS PT WHERE PT.Turma_TurmaId = " + TurmaId, conn);
                conn.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Dictionary<int, int> dic = new Dictionary<int, int>();
                        dic.Add(reader.GetInt32(0), reader.GetInt32(1));
                        ListDict.Add(dic);
                    }
                    conn.Close();
                    return ListDict;
                }
                else
                {
                    conn.Close();
                    return ListDict;
                }
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message.ToString());
            }

        }

    }
}
