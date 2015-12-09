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
    public class ProvaRepositorio : RepositorioBase<Prova>, IProvaRepositorio
    {
        private readonly TurmaRepositorio _turmaRep;
        private readonly ProfessorRepositorio _profRep;
        private readonly DisciplinaRepositorio _discRep;

        public Prova RecuperarProva(int ProvaId)
        {
            try
            {
                //var ProvaIdParameter = new SqlParameter("@ProvaId", ProvaId);
                //var query = this.Db.Set<Prova>().SqlQuery("SELECT * FROM Prova AS P INNER JOIN Disciplina AS D ON P.Disciplina_DisciplinaId = D.DisciplinaId INNER JOIN Professor AS PR ON P.Professores_Id = PR.Id INNER JOIN Funcionario AS F ON PR.Id = F.Id INNER JOIN Usuario AS U ON F.Id = U.Id INNER JOIN Turma AS T ON P.Turma_TurmaId = T.TurmaId WHERE P.ProvaId = @ProvaId", ProvaIdParameter).First();
                //return query;

                var result = from d in Db.Provas
                             where d.ProvaId.Equals(ProvaId)
                             select d;
                return result.First();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message.ToString());
            }
        }

        public Prova IncluirProva(Prova prova)
        {
            try
            {
                var DataProvaParameter = new SqlParameter("@DataProva", prova.DataProva);
                var UnidadeParameter = new SqlParameter("@Unidade", prova.Unidade);
                var StatusProvaParameter = new SqlParameter("@StatusProva", prova.StatusProva);
                var TipoProvaParameter = new SqlParameter("@TipoProva", prova.TipoProva);
                var DisciplicaIdParameter = new SqlParameter("@DisciplinaId", prova.Disciplina.DisciplinaId);
                var ProfessoreIdParameter = new SqlParameter("@ProfessorId", prova.Professores.Id);
                var TurmaIdParameter = new SqlParameter("@TurmaId", prova.Turma.TurmaId);

                var query = Db.Database.ExecuteSqlCommand("INSERT INTO Prova (DataProva, Unidade, StatusProva, TipoProva, Disciplina_DisciplinaId, Professores_Id, Turma_TurmaId) VALUES (@DataProva, @Unidade, @StatusProva, @TipoProva, @DisciplinaId, @ProfessorId, @TurmaId)", DataProvaParameter, UnidadeParameter, StatusProvaParameter, TipoProvaParameter, DisciplicaIdParameter, ProfessoreIdParameter, TurmaIdParameter);

                return prova;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message.ToString());
            }
            
        }

        public bool AtualizarDadosProva(Prova prova)
        {
            try
            {
                var provaIdParameter = new SqlParameter("@ProvaId", prova.ProvaId);
                var dataProvaParameter = new SqlParameter("@DataProva", prova.DataProva);
                var unidadeParameter = new SqlParameter("@Unidade", prova.Unidade);
                var statusProvaParameter = new SqlParameter("@StatusProva", prova.StatusProva);
                var tipoProvaParameter = new SqlParameter("@TipoProva", prova.TipoProva);
                var disciplinaIdParameter = new SqlParameter("@DisciplinaId", prova.Disciplina.DisciplinaId);
                var turmaIdParameter = new SqlParameter("@TurmaId", prova.Turma.TurmaId);

                var query = this.Db.Database.ExecuteSqlCommand("UPDATE Prova SET DataProva = @DataProva, Unidade = @Unidade, StatusProva = @StatusProva, TipoProva = @TipoProva, Disciplina_DisciplinaId = @DisciplinaId, Turma_TurmaId = @TurmaId WHERE ProvaId = @ProvaId", provaIdParameter, dataProvaParameter, unidadeParameter, statusProvaParameter, tipoProvaParameter, disciplinaIdParameter, turmaIdParameter);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Prova> BuscarPorDisciplina(int codDisciplina)
        {
            List<Prova> ListaRetorno = new List<Prova>();
            SqlConnection conn = (SqlConnection)Db.Database.Connection;
            SqlCommand command = new SqlCommand("SELECT * FROM Prova AS P WHERE P.Disciplina_DisciplinaId = " + codDisciplina, conn);
            conn.Open();

            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Prova pr = new Prova()
                    {
                        ProvaId = reader.GetInt32(0),
                        DataProva = reader.GetDateTime(1),
                        Unidade = reader.GetInt32(2),
                        StatusProva = reader.GetInt32(3),
                        TipoProva = reader.GetInt32(4),
                        Disciplina = (new DisciplinaRepositorio().Recuperar(reader.GetInt32(5))),
                        Professores = (new ProfessorRepositorio().Recuperar(reader.GetInt32(6))),
                        Turma = (new TurmaRepositorio().Recuperar(reader.GetInt32(7)))
                    };
                    ListaRetorno.Add(pr);
                }
                conn.Close();
                return ListaRetorno;
            }
            else
            {
                return ListaRetorno;
            }
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

        public bool ExcluirProva(int ProvaId)
        {
            try
            {
                var ProvaIdParameter = new SqlParameter("@ProvaId", ProvaId);
                var query = this.Db.Database.ExecuteSqlCommand("DELETE FROM Prova WHERE ProvaId = @ProvaId", ProvaIdParameter);
                var query2 = this.Db.Database.ExecuteSqlCommand("DELETE FROM ResultadosProvas WHERE Prova_ProvaId = @ProvaId", ProvaIdParameter);
                return true;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message.ToString());
            }

        }

        public IEnumerable<Prova> RecuperarTodasAsProvas()
        {
            List<Prova> ListaRetorno = new List<Prova>();
            SqlConnection conn = (SqlConnection)Db.Database.Connection;
            SqlCommand command = new SqlCommand("SELECT * FROM Prova", conn);
            conn.Open();

            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Prova pr = new Prova()
                    {
                        ProvaId = reader.GetInt32(0),
                        DataProva = reader.GetDateTime(1),
                        Unidade = reader.GetInt32(2),
                        StatusProva = reader.GetInt32(3),
                        TipoProva = reader.GetInt32(4),
                        Disciplina = (new DisciplinaRepositorio().Recuperar(reader.GetInt32(5))),
                        Professores = (new ProfessorRepositorio().Recuperar(reader.GetInt32(6))),
                        Turma = (new TurmaRepositorio().Recuperar(reader.GetInt32(7)))
                    };
                    ListaRetorno.Add(pr);
                }
                return ListaRetorno;
            }
            else
            {
                return ListaRetorno;
            }
        }

        public IEnumerable<Prova> RecuperarProvasPendentesTurmaProfessor(int ProfessorId, int TurmaId)
        {
            try
            {
                List<Prova> ListaRetorno = new List<Prova>();
                SqlConnection conn = (SqlConnection)Db.Database.Connection;
                SqlCommand command = new SqlCommand("SELECT * FROM Prova AS P WHERE P.Professores_Id = " + ProfessorId + " OR " + ProfessorId + " IS NULL AND P.Turma_TurmaId = " + TurmaId + " OR " + TurmaId + " IS NULL", conn);
                conn.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Prova pr = new Prova()
                        {
                            ProvaId = reader.GetInt32(0),
                            DataProva = reader.GetDateTime(1),
                            Unidade = reader.GetInt32(2),
                            StatusProva = reader.GetInt32(3),
                            TipoProva = reader.GetInt32(4),
                            Disciplina = (new DisciplinaRepositorio().Recuperar(reader.GetInt32(5))),
                            Professores = (new ProfessorRepositorio().Recuperar(reader.GetInt32(6))),
                            Turma = (new TurmaRepositorio().Recuperar(reader.GetInt32(7)))
                        };
                        ListaRetorno.Add(pr);
                    }
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

        public IEnumerable<Prova> RecuperarProvasConcluidas(int TurmaId)
        {
            try
            {
                List<Prova> ListaRetorno = new List<Prova>();
                SqlConnection conn = (SqlConnection)Db.Database.Connection;
                SqlCommand command = new SqlCommand("SELECT * FROM Prova AS P WHERE P.StatusProva = 2 AND P.Turma_TurmaId = " + TurmaId, conn);
                conn.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Prova pr = new Prova()
                        {
                            ProvaId = reader.GetInt32(0),
                            DataProva = reader.GetDateTime(1),
                            Unidade = reader.GetInt32(2),
                            StatusProva = reader.GetInt32(3),
                            TipoProva = reader.GetInt32(4),
                            Disciplina = (new DisciplinaRepositorio().Recuperar(reader.GetInt32(5))),
                            Professores = (new ProfessorRepositorio().Recuperar(reader.GetInt32(6))),
                            Turma = (new TurmaRepositorio().Recuperar(reader.GetInt32(7)))
                        };
                        ListaRetorno.Add(pr);
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


        public IEnumerable<Prova> RecuperarProvasConcluidasTurmaProfessor(int ProfessorId, int TurmaId)
        {
            try
            {
                List<Prova> ListaRetorno = new List<Prova>();
                SqlConnection conn = (SqlConnection)Db.Database.Connection;
                SqlCommand command = new SqlCommand("SELECT * FROM Prova AS P WHERE P.StatusProva = 2 AND P.Turma_TurmaId = " + TurmaId + "AND P.Professores_Id = " + ProfessorId, conn);
                //SqlCommand command = new SqlCommand("SELECT * FROM Prova AS P WHERE P.Professores_Id = " + ProfessorId + " OR " + ProfessorId + " IS NULL AND P.Turma_TurmaId = " + TurmaId + " OR " + TurmaId + " IS NULL", conn);
                conn.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Prova pr = new Prova()
                        {
                            ProvaId = reader.GetInt32(0),
                            DataProva = reader.GetDateTime(1),
                            Unidade = reader.GetInt32(2),
                            StatusProva = reader.GetInt32(3),
                            TipoProva = reader.GetInt32(4),
                            Disciplina = (new DisciplinaRepositorio().Recuperar(reader.GetInt32(5))),
                            Professores = (new ProfessorRepositorio().Recuperar(reader.GetInt32(6))),
                            Turma = (new TurmaRepositorio().Recuperar(reader.GetInt32(7)))
                        };
                        ListaRetorno.Add(pr);
                    }
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
