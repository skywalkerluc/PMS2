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
    public class ResultadosProvasRepositorio : RepositorioBase<ResultadosProvas>, IResultadosProvasRepositorio
    {
        public ResultadosProvas IncluirNotaAluno(ResultadosProvas resultadoProva)
        {
            try
            {
                Db.Entry(resultadoProva.Aluno).State = EntityState.Unchanged;
                Db.Entry(resultadoProva.Prova).State = EntityState.Unchanged;
                Db.ResultadosProvas.Add(resultadoProva);
                Db.SaveChanges();
                return resultadoProva;
            }
            catch (Exception)
            {
                throw new NotImplementedException("Erro ao adicionar notas de aluno.");
            }
            
        }



        public IEnumerable<ResultadosProvas> RecuperarNotasAluno(int AlunoId)
        {
            var result = from n in Db.ResultadosProvas
                         where n.Aluno.Id.Equals(AlunoId)
                         select n;

            return result;
        }

        public IEnumerable<ResultadosProvas> RecuperarHistoricoNotasTurma(int TurmaId)
        {
            try
            {
                var TurmaParameter = new SqlParameter("@TurmaId", TurmaId);
                var query = this.Db.Set<ResultadosProvas>().SqlQuery("SELECT R.Prova_ProvaId, R.Aluno_Id, R.Gabarito, R.Nota, R.Observacao, R.ResultadoId FROM ResultadosProvas AS R INNER JOIN Aluno AS A ON R.Aluno_Id = A.Id INNER JOIN Turma AS T ON A.Turma_TurmaId = T.TurmaId", TurmaParameter);
                return query;
            }
            catch (Exception ex)
            {
                var message = ex.Message.ToString();
                throw new NotImplementedException(message);
            }
        }

        public bool AlterarResultadoAluno(ResultadosProvas resultado)
        {
            try
            {
                var notaParameter = new SqlParameter("@Nota", resultado.Nota);
                var observacaoParameter = new SqlParameter("@Obs", resultado.Observacao);
                var resultadoParameter = new SqlParameter("@ResultadoId", resultado.ResultadoId);

                var query = this.Db.Set<ResultadosProvas>().SqlQuery("UPDATE ResultadosProvas SET Nota = @Nota, Observacao = @Obs WHERE ResultadoId = @ResultadoId", notaParameter, observacaoParameter, resultadoParameter);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoverResultadosAlunos(List<ResultadosProvas> resultadosProvas)
        {
            try
            {
                foreach (var result in resultadosProvas)
                {
                    Db.Entry(result.Aluno).State = EntityState.Unchanged;
                    Db.Entry(result.Prova).State = EntityState.Unchanged;
                    Db.ResultadosProvas.Remove(result);
                }
                Db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<ResultadosProvas> RecuperarResultadosProva(int ProvaId)
        {
            try
            {
                List<ResultadosProvas> ListaRetorno = new List<ResultadosProvas>();
                SqlConnection conn = (SqlConnection)Db.Database.Connection;
                SqlCommand command = new SqlCommand("SELECT * FROM ResultadosProvas AS RP WHERE RP.Prova_ProvaId = " + ProvaId, conn);
                conn.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ResultadosProvas result = new ResultadosProvas()
                        {
                            ResultadoId = reader.GetInt32(0),
                            Observacao = reader.GetString(1),
                            Nota = reader.GetInt32(2),
                            Gabarito = reader.GetString(3),
                            Aluno = (new AlunoRepositorio().RecuperarDadosAluno(reader.GetInt32(4))),
                            Prova = (new ProvaRepositorio().RecuperarProva(reader.GetInt32(5)))
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

        public ResultadosProvas RecuperarResultadosProvasPorId(int ResultadoProvaId)
        {
            ResultadosProvas result = new ResultadosProvas();
            try
            {
                SqlConnection conn = (SqlConnection)Db.Database.Connection;
                SqlCommand command = new SqlCommand("SELECT * FROM ResultadosProvas AS RP WHERE RP.ResultadoId = " + ResultadoProvaId, conn);
                conn.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = new ResultadosProvas()
                        {
                            ResultadoId = reader.GetInt32(0),
                            Observacao = (reader.GetString(1) ?? string.Empty),
                            Nota = reader.GetInt32(2),
                            Gabarito = reader.GetString(3),
                            Aluno = (new AlunoRepositorio().RecuperarDadosAluno(reader.GetInt32(4))),
                            Prova = (new ProvaRepositorio().RecuperarProva(reader.GetInt32(5)))
                        };
                    }
                    conn.Close();
                    return result;
                }
                else
                {
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message.ToString());
            }
        }

        public bool RemoverResultadoProva(int ResultadoProvaId)
        {
            try
            {
                var ResultadoParameter = new SqlParameter("@ResultadoProvaId", ResultadoProvaId);
                var query = this.Db.Database.ExecuteSqlCommand("DELETE FROM ResultadosProvas WHERE ResultadoId = @ResultadoProvaId", ResultadoParameter);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
