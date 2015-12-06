using SchoolManagement.Domain.Entidades;
using SchoolManagement.Domain.Interfaces.Repositorios;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace SchoolManagement.Data.Repositorios
{
    public class ProfessorRepositorio : RepositorioBase<Professor>, IProfessorRepositorio
    {
        public Professor IncluirProfessor(Professor professor)
        {
            try
            {
                foreach (var disc in professor.Disciplinas)
                {
                    Db.Entry(disc).State = EntityState.Unchanged;
                }
                Db.Professores.Add(professor);
                Db.SaveChanges();
                return professor;
            }
            catch (System.Exception ex)
            {
                throw new NotImplementedException(ex.Message.ToString());
            }
        }

        public IEnumerable<Professor> BuscarPorNome(string nome)
        {
            return Db.Professores.Where(p => p.Nome == nome);
        }

        public bool VerificarConfiguracoesIdenticas(Professor professor)
        {
            var identico = Db.Professores.Where(p => p.Cpf == professor.Cpf || p.Rg == professor.Rg || p.Id == professor.Id);

            if (identico.Count() < 0)
                return true;
            else
                return false;
        }

        public IEnumerable<Turma> VisualizarTurmasProfessor(Professor professor)
        {
            var turmas = (new TurmaRepositorio().RecuperarTodos());
            List<Turma> TurmasProfessor = new List<Turma>();

            foreach (var turma in turmas)
            {
                foreach (var professorTurma in turma.Professores)
                {
                    if (professorTurma.Id == professor.Id)
                    {
                        TurmasProfessor.Add(turma);
                    }
                }
            }

            IEnumerable<Turma> RetornoTurmas = TurmasProfessor;
            return RetornoTurmas;
        }

        public IEnumerable<Professor> VisualizarProfessoresTurma(int TurmaId)
        {
            var query = "SELECT * FROM Professor AS P INNER JOIN ProfessorTurma AS PT ON P.Id = PT.Professor_Id WHERE PT.Turma_TurmaId = @TurmaId";
            
            List<Professor> ListaRetorno = new List<Professor>();
            var TurmaIdParam = new SqlParameter("@TurmaId", TurmaId);
            var valor = this.Db.Set<Professor>().SqlQuery(query, TurmaIdParam);

            if (valor != null)
            {
                foreach (var value in valor)
                {
                    ListaRetorno.Add(value);
                }
            }
            return ListaRetorno;
        }

        public bool IncluirProfessorEmTurma(int ProfessorId, int TurmaId)
        {
            try
            {
                var TurmaIdParameter = new SqlParameter("@TurmaId", TurmaId);
                var ProfessorIdParameter = new SqlParameter("@ProfessorId", ProfessorId);

                var query = this.Db.Database.ExecuteSqlCommand("INSERT INTO [dbo].[ProfessorTurma] ([Professor_Id], [Turma_TurmaId]) VALUES (@ProfessorId, @TurmaId)", ProfessorIdParameter, TurmaIdParameter);
                return true;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message.ToString());
            }
        }

        public bool RemoverProfessorDeTurma(int ProfessorId, int TurmaId)
        {
            try
            {
                var TurmaIdParameter = new SqlParameter("@TurmaId", TurmaId);
                var ProfessorIdParameter = new SqlParameter("@ProfessorId", ProfessorId);

                var query = this.Db.Database.ExecuteSqlCommand("DELETE FROM ProfessorTurma WHERE Professor_Id = @ProfessorId AND Turma_TurmaId = @TurmaId", ProfessorIdParameter, TurmaIdParameter);
                return true;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message.ToString());
            }
        }

    }
}
