using SchoolManagement.Domain.Entidades;
using SchoolManagement.Domain.Interfaces.Repositorios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Data.Repositorios
{
    public class FrequenciaRepositorio : RepositorioBase<Frequencia>, IFrequenciaRepositorio
    {
        public Frequencia IncluirFrequenciaAluno(Frequencia frequencia)
        {
            try
            {
                Db.Entry(frequencia.Aluno).State = EntityState.Unchanged;
                Db.Entry(frequencia.Disciplina).State = EntityState.Unchanged;
                Db.Frequencia.Add(frequencia);
                Db.SaveChanges();
                return frequencia;
            }
            catch (Exception)
            {
                throw new NotImplementedException("Erro ao adicionar frequência de aluno.");
            }
        }

        public bool RemoverFrequencia(int FrequenciaId)
        {
            try
            {
                var frequ = this.Recuperar(FrequenciaId);
                Db.Entry(frequ.Aluno).State = EntityState.Unchanged;
                Db.Entry(frequ.Disciplina).State = EntityState.Unchanged;
                Db.Frequencia.Remove(frequ);
                Db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Frequencia> RecuperarHistoricoFrequenciasAluno(int AlunoId)
        {
            var query = from f in Db.Frequencia
                        where f.Aluno.Id == AlunoId
                        select f;
            return query;
        }

        public IEnumerable<Frequencia> RecuperarHistorioFrequenciasTurma(int TurmaId)
        {
            try
            {
                var TurmaParameter = new SqlParameter("@TurmaId", TurmaId);
                var query = this.Db.Set<Frequencia>().SqlQuery("SELECT * FROM Frequencia AS F INNER JOIN Aluno AS A ON F.Aluno_Id = A.Id INNER JOIN Turma AS T ON T.TurmaId = A.Turma_TurmaId WHERE T.TurmaId = @TurmaId", TurmaParameter);
                return query;
            }
            catch (Exception ex)
            {
                var message = ex.Message.ToString();
                throw new NotImplementedException(message);
            }
            
        }

        public Frequencia AlterarFrequenciaAluno(Frequencia frequencia)
        {
            var frequenciaId = new SqlParameter("@FrequenciaId", SqlDbType.Int);
            var frequenciaAlunoId = new SqlParameter("@AlunoId", SqlDbType.Int);
            var disciplinaId = new SqlParameter("@DisciplinaId", SqlDbType.Int);
            var dataReferencia = new SqlParameter("@DataReferencia", SqlDbType.DateTime);
            var presenca = new SqlParameter("@Presenca", SqlDbType.Int);
            
            if(frequencia.Presente)
            {
                presenca.SqlValue = 1;
            }
            else
            {
                presenca.SqlValue = 0;
            }

            tratarParametros(frequencia.FrequenciaId, frequenciaId);
            tratarParametros(frequencia.Aluno.Id, frequenciaAlunoId);
            tratarParametros(frequencia.Disciplina.DisciplinaId, disciplinaId);
            tratarParametros(frequencia.DataReferencia, dataReferencia);
            var value = this.Db.Set<Frequencia>().SqlQuery("UPDATE Frequencia SET Aluno_Id = @AlunoId, DataReferencia = @DataReferencia, Disciplina_DisciplinaId = @DisciplinaId, Presente = @Presenca WHERE FrequenciaId = @FrequenciaId", frequenciaAlunoId, dataReferencia, disciplinaId, presenca, frequenciaId);
            return frequencia;
        }

        public bool RemoverFrequenciasAlunos(List<Frequencia> frequenciasAlunos)
        {
            try
            {
                foreach (var frquc in frequenciasAlunos)
                {
                    Db.Entry(frquc.Aluno).State = EntityState.Unchanged;
                    Db.Entry(frquc.Disciplina).State = EntityState.Unchanged;
                    Db.Frequencia.Remove(frquc);
                }
                Db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                throw new NotImplementedException("Erro ao adicionar frequência de aluno.");
            }
        }

        private void tratarParametros(Object valorAtributo, SqlParameter parametro)
        {
            if (valorAtributo == null)
            {
                parametro.SqlValue = DBNull.Value;
            }
            else if ((valorAtributo != null) &&
                     (valorAtributo.GetType() == typeof(String)) &&
                     (String.IsNullOrEmpty((String)valorAtributo)))
            {
                parametro.SqlValue = DBNull.Value;
            }
            else
            {
                parametro.SqlValue = valorAtributo;
            }
        }
    }
}
