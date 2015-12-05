using SchoolManagement.Domain.Entidades;
using SchoolManagement.Domain.Interfaces.Repositorios;
using SchoolManagement.Data.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.SqlClient;

namespace SchoolManagement.Data.Repositorios
{
    public class AlunoRepositorio : RepositorioBase<Aluno>, IAlunoRepositorio
    {
        private readonly TurmaRepositorio _turmaRep = new TurmaRepositorio();

        public Aluno IncluirAluno(Aluno param)
        {
            try
            {
                Db.Alunos.Add(param);
                
                Db.SaveChanges();
                return param;

            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
        }

        //private static void Main(string[] args)
        //{
        //    DeleteAllEntities();
        //    CreateInitialEntities();
        //    int oId = LoadOwnerId();
        //    int cId = LoadChildId();
        //    AssociateAndSave(oId, cId);
        //}

        //private static int LoadOwnerId()
        //{
        //    using (var context = new ModelEntities())
        //    {
        //        return (from o in context.Owners
        //                select o).First().Id;
        //    }
        //}

        //private static int LoadChildId()
        //{
        //    using (var context = new ModelEntities())
        //    {
        //        return (from c in context.Children
        //                select c).First().Id;
        //    }
        //}

        //private static void AssociateAndSave(int oId, int cId)
        //{
        //    using (var context = new ModelEntities())
        //    {
        //        var owner = (from o in context.Owners
        //                     select o).FirstOrDefault(o => o.ID == oId);
        //        var child = (from o in context.Children
        //                     select o).FirstOrDefault(c => c.ID == cId);

        //        owner.Children.Add(child);
        //        context.Attach(owner);
        //        context.SaveChanges();
        //    }
        //}

        public bool VerificarNumeroDeMatriculaJaExistente(string numeroMatricula)
        {
            var identico = Db.Alunos.Where(p => p.NumeroMatricula == numeroMatricula);

            if (identico.Count() < 0)
                return true;
            else
                return false;
        }

        public IEnumerable<Aluno> PesquisarAlunoPorNome(string nomeAluno)
        {
            return Db.Alunos.Where(p => p.Nome.Contains(nomeAluno));
        }

        public IEnumerable<Aluno> PesquisarAlunoPorNumeroMatricula(string numeroMatricula)
        {
            return Db.Alunos.Where(p => p.NumeroMatricula == numeroMatricula);
        }

        public IEnumerable<Aluno> PesquisarAlunoPorNomeEmTurma(string nomeAluno, int codigoTurma)
        {
            var alunosEmTurma =
                from a in Db.Alunos
                where (a.Nome == nomeAluno && a.Turma.TurmaId == codigoTurma)
                select a;

            IEnumerable<Aluno> FiltroAlunoTurma = alunosEmTurma;

            return FiltroAlunoTurma;
        }

        public IEnumerable<Frequencia> RecuperarFrequenciaAluno(Aluno aluno)
        {
            var alunoDomain = Recuperar(aluno.Id);

            var frequencia = from a in Db.Frequencia
                             where a.Aluno.Id == alunoDomain.Id
                             select a;
            return frequencia;
        }

        public IEnumerable<ResultadosProvas> RecuperarResultadosAluno(Aluno aluno)
        {
            var alunoDomain = Recuperar(aluno.Id);

            var resultados = from a in Db.ResultadosProvas
                             where a.Aluno.Id == alunoDomain.Id
                             select a;
            return resultados;
        }

        public IEnumerable<Aluno> FiltrarAluno(string nomeAluno, int? turmaId)
        {
            if (turmaId.HasValue)
            {
                var enumAlunos = from a in Db.Alunos
                                 where a.Nome.Contains(nomeAluno)
                                 join t in Db.Turmas on a.Turma.TurmaId equals t.TurmaId
                                 where t.TurmaId == turmaId
                                 select a;
                IEnumerable<Aluno> RetornoAluno = enumAlunos;
                return RetornoAluno;
            }
            else if (nomeAluno != string.Empty)
            {
                var enumAlunosNome = from a in Db.Alunos
                                     where a.Nome.Contains(nomeAluno)
                                     select a;
                IEnumerable<Aluno> RetornoAluno = enumAlunosNome;
                return RetornoAluno;
            }
            else
            {
                var enumTodos = RecuperarTodos();
                IEnumerable<Aluno> RetornoAluno = enumTodos;
                return RetornoAluno;
            }
        }

        public IEnumerable<Aluno> RecuperarAlunosTurma(int TurmaId)
        {
            var alunosTurma = from a in Db.Alunos
                              where a.Turma.TurmaId == TurmaId
                              select a;

            return alunosTurma;
        }

        public bool RematricularAlunos(ICollection<Aluno> alunos)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Aluno> RecuperarAlunosTurmaProfessor(int professorId, int turmaId)
        {
            try
            {
                var professorParameter = new SqlParameter("@ProfessorId", professorId);
                var turmaParameter = new SqlParameter("@TurmaId", turmaId);

                var query = this.Db.Set<Aluno>().SqlQuery("SELECT * FROM Aluno AS A JOIN ProfessorTurma AS PT ON A.Turma_TurmaId = PT.Turma_TurmaId WHERE PT.Professor_Id = @ProfessorId", professorParameter, turmaParameter).ToList();
                return query;
            }
            catch (Exception)
            {
                throw new NotImplementedException("Erro ao recuperar alunos da turma");
            }
            
        }

    }
}
