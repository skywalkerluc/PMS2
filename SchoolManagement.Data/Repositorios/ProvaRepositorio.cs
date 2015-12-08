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
            Db.Entry(prova.Disciplina).State = EntityState.Unchanged;
            Db.Entry(prova.Turma).State = EntityState.Unchanged;

            //Db.Provas.Attach(prova);
            Db.Provas.Add(prova);
            Db.SaveChanges();

        //    //Db.Entry(prova.Disciplina).State = EntityState.Unchanged;
        //    //Db.Entry(prova.Professores).State = EntityState.Unchanged;
        //    //Db.Provas.Add(prova);
        //    //Db.SaveChanges();

        //    //return prova;

        //    //var dataProvaParameter = new SqlParameter("@DataProva", prova.DataProva);
        //    //var unidadeParameter = new SqlParameter("@Unidade", prova.Unidade);
        //    //var statusProvaParameter = new SqlParameter("@StatusProva", prova.StatusProva);
        //    //var tipoProvaParameter = new SqlParameter("@TipoProva", prova.TipoProva);
        //    //var disciplinaIdParameter = new SqlParameter("@DisciplinaId", prova.Disciplina.DisciplinaId);
        //    //var professorIdParameter = new SqlParameter("@ProfessorId", prova.Professores.Id);
        //    //var turmaIdParameter = new SqlParameter("@TurmaId", prova.Turma.TurmaId);

        //    //var query = this.Db.Database.ExecuteSqlCommand("INSERT INTO [dbo].[Prova] ([DataProva], [Unidade], [StatusProva], [TipoProva], [Disciplina_DisciplinaId], [Professores_Id], [Turma_TurmaId]) VALUES (@DataProva, @Unidade, @StatusProva, @TipoProva, @DisciplinaId, @ProfessorId, @TurmaId)", 
        //    //    dataProvaParameter, unidadeParameter, statusProvaParameter, tipoProvaParameter, disciplinaIdParameter, professorIdParameter, turmaIdParameter);
        //    //return prova;



        //    Db.Entry(prova.Disciplina).State = EntityState.Unchanged;

        //    if(prova.Professores != null)
        //    {
        //        Db.Entry(prova.Professores).State = EntityState.Unchanged;
        //    }
            
        //    //if (prova.Disciplina.Livros != null)
        //    //{
        //    //    foreach (var livro in prova.Disciplina.Livros)
        //    //    {
        //    //        Db.Entry(livro).State = EntityState.Unchanged;
        //    //    }
        //    //}

        //    //if (prova.Disciplina.Professores != null)
        //    //{
        //    //    foreach (var prof in prova.Disciplina.Professores)
        //    //    {
        //    //        Db.Entry(prof).State = EntityState.Unchanged;
        //    //    }
        //    //}

        //    //if (prova.Disciplina.Turmas != null)
        //    //{
        //    //    foreach (var turma in prova.Disciplina.Turmas)
        //    //    {
        //    //        Db.Entry(turma).State = EntityState.Unchanged;
        //    //    }
        //    //}

        //    //if (prova.Professores != null)
        //    //{
        //    //    Db.Entry(prova.Professores).State = EntityState.Unchanged;
        //    //}

        //    //if (prova.Professores.Disciplinas != null)
        //    //{
        //    //    foreach (var disc in prova.Professores.Disciplinas)
        //    //    {
        //    //        Db.Entry(disc).State = EntityState.Unchanged;
        //    //    }
        //    //}

        //    //if (prova.Professores.Turmas != null)
        //    //{
        //    //    foreach (var turm in prova.Professores.Turmas)
        //    //    {
        //    //        Db.Entry(turm).State = EntityState.Unchanged;
        //    //    }
        //    //}


        //    if (prova.Turma != null)
        //    {
        //        Db.Entry(prova.Turma).State = EntityState.Unchanged;
        //    }

        //    //if (prova.Turma.Alunos != null)
        //    //{
        //    //    foreach (var aluno in prova.Turma.Alunos)
        //    //    {
        //    //        Db.Entry(aluno).State = EntityState.Unchanged;
        //    //    }
        //    //}

        //    //if (prova.Turma.AnoLetivo != null)
        //    //{
        //    //    Db.Entry(prova.Turma.AnoLetivo).State = EntityState.Unchanged;
        //    //}


        //    //if (prova.Turma.Disciplinas != null)
        //    //{
        //    //    foreach (var disc in prova.Turma.Disciplinas)
        //    //    {
        //    //        Db.Entry(disc).State = EntityState.Unchanged;
        //    //    }
        //    //}

        //    //if (prova.Turma.Materiais != null)
        //    //{
        //    //    foreach (var material in prova.Turma.Materiais)
        //    //    {
        //    //        Db.Entry(material).State = EntityState.Unchanged;
        //    //    }
        //    //}

        //    //if (prova.Turma.Professores != null)
        //    //{
        //    //    foreach (var prof in prova.Turma.Professores)
        //    //    {
        //    //        Db.Entry(prof).State = EntityState.Unchanged;
        //    //    }
        //    //}

        //    //Db.Provas.Attach(prova);
        //    Db.SaveChanges();
        //    Db.Provas.Add(prova);
        //    Db.SaveChanges();

            return prova;
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
                return ListaRetorno;
            }
            else
            {
                throw new NotImplementedException();
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


    }
}
