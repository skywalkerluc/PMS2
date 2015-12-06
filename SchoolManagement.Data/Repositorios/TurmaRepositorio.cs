﻿using SchoolManagement.Domain.Entidades;
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

        public IEnumerable<Turma> FiltrarTurma(string descTurma, Professor professor, AnoLetivo ano, int horarioId)
        {
            List<Turma> ListaTurmasFiltradas = new List<Turma>();
            var turmaFiltro = from t in Db.Turmas.Where(t => t.Descricao == null || t.Descricao == descTurma
                                  || t.AnoLetivo.AnoLetivoId == 0 || t.AnoLetivo.AnoLetivoId == ano.AnoLetivoId
                                  || t.HorariosTurmaId == 0 || t.HorariosTurmaId == horarioId)
                              select t;

            if (professor != null)
            {
                foreach (var turmaFiltrada in turmaFiltro)
                {
                    foreach (var prof in turmaFiltrada.Professores)
                    {
                        if (prof.Id == professor.Id)
                        {
                            ListaTurmasFiltradas.Add(turmaFiltrada);
                        }
                    }
                }
                IEnumerable<Turma> RetornoTurmaFiltrada = ListaTurmasFiltradas;
                return RetornoTurmaFiltrada;
            }
            else
            {
                IEnumerable<Turma> RetornoTurmaFiltrada = turmaFiltro;
                return RetornoTurmaFiltrada;
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
            var professorIdParameter = new SqlParameter("@ProfessorId", professorId);
        
            var query = this.Db.Turmas.SqlQuery("SELECT * FROM Turma AS T INNER JOIN ProfessorTurma AS PT ON T.TurmaId = PT.Turma_TurmaId WHERE PT.Professor_Id = @ProfessorId", professorIdParameter).ToList();
            return query;
        }

        public IEnumerable<Turma> RecuperarTurmasProfessorNaoLeciona(int ProfessorId)
        {
            try
            {
                List<Turma> ListaRetorno = new List<Turma>();
                var turmas = this.RecuperarTodos();
                foreach (var turma in turmas)
                {
                    foreach (var prof in turma.Professores)
                    {
                        if (prof.Id != ProfessorId)
                        {
                            ListaRetorno.Add(turma);
                        }
                    }
                }
                return ListaRetorno;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message.ToString());
            }
        }

    }
}
