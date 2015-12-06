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
        public Prova RecuperarProva(int ProvaId)
        {
            try
            {
                var ProvaIdParameter = new SqlParameter("@ProvaId", ProvaId);
                var query = this.Db.Set<Prova>().SqlQuery("SELECT * FROM Prova AS P INNER JOIN Disciplina AS D ON P.Disciplina_DisciplinaId = D.DisciplinaId INNER JOIN Professor AS PR ON P.Professores_Id = PR.Id INNER JOIN Funcionario AS F ON PR.Id = F.Id INNER JOIN Usuario AS U ON F.Id = U.Id INNER JOIN Turma AS T ON P.Turma_TurmaId = T.TurmaId WHERE P.ProvaId = @ProvaId", ProvaIdParameter).First();
                return query;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message.ToString());
            }
        }

        public Prova IncluirProva(Prova prova)
        {
            //Db.Entry(prova.Disciplina).State = EntityState.Unchanged;
            //Db.Entry(prova.Professores).State = EntityState.Unchanged;
            //Db.Provas.Add(prova);
            //Db.SaveChanges();

            //return prova;

            var dataProvaParameter = new SqlParameter("@DataProva", prova.DataProva);
            var unidadeParameter = new SqlParameter("@Unidade", prova.Unidade);
            var statusProvaParameter = new SqlParameter("@StatusProva", prova.StatusProva);
            var tipoProvaParameter = new SqlParameter("@TipoProva", prova.TipoProva);
            var disciplinaIdParameter = new SqlParameter("@DisciplinaId", prova.Disciplina.DisciplinaId);
            var professorIdParameter = new SqlParameter("@ProfessorId", prova.Professores.Id);
            var turmaIdParameter = new SqlParameter("@TurmaId", prova.Turma.TurmaId);

            var query = this.Db.Database.ExecuteSqlCommand("INSERT INTO [dbo].[Prova] ([DataProva], [Unidade], [StatusProva], [TipoProva], [Disciplina_DisciplinaId], [Professores_Id], [Turma_TurmaId]) VALUES (@DataProva, @Unidade, @StatusProva, @TipoProva, @DisciplinaId, @ProfessorId, @TurmaId)", 
                dataProvaParameter, unidadeParameter, statusProvaParameter, tipoProvaParameter, disciplinaIdParameter, professorIdParameter, turmaIdParameter);
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
