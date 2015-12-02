using SchoolManagement.Domain.Entidades;
using SchoolManagement.Domain.Interfaces.Repositorios;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Data.Repositorios
{
    public class TrabalhosExtrasRepositorio : RepositorioBase<TrabalhosExtras>, ITrabalhosExtrasRepositorio
    {
        public bool LancarNota(Aluno aluno, Professor professor, TrabalhosExtras trab, Prova prova, int nota)
        {
            try
            {
                if (prova == null && trab == null)
                    return false;

                if (prova != null && trab == null)
                {
                    var inserirNotaProva = Db.ResultadosProvas.Add(new ResultadosProvas()
                    {
                        Aluno = aluno,
                        Prova = prova,
                        Nota = nota
                    });

                    Db.Entry(prova.Disciplina).State = EntityState.Unchanged;
                    Db.Entry(prova.Professores).State = EntityState.Unchanged;
                    Db.Entry(aluno).State = EntityState.Unchanged;
                    foreach (var item in aluno.Responsaveis)
                    {
                        Db.Entry(item).State = EntityState.Unchanged;
                    }
                    Db.Entry(aluno.Turma).State = EntityState.Unchanged;
                    Db.Entry(prova).State = EntityState.Unchanged;

                    Db.SaveChanges();
                    return true;
                }
                else if (trab != null && prova == null)
                {
                    var insertNotaTrab = Db.TrabalhosExtras.Add(new TrabalhosExtras()
                    {
                        Aluno = aluno,
                        DataConclusao = DateTime.Now,
                        Nota = nota,
                        Professor = professor,
                    });

                    Db.Entry(aluno).State = EntityState.Unchanged;
                    foreach (var item in aluno.Responsaveis)
                    {
                        Db.Entry(item).State = EntityState.Unchanged;
                    }
                    Db.Entry(aluno.Turma).State = EntityState.Unchanged;
                    Db.Entry(professor).State = EntityState.Unchanged;

                    foreach (var item in professor.Disciplinas)
                    {
                        Db.Entry(item).State = EntityState.Unchanged;
                    }

                    Db.SaveChanges();
                    return true;
                }
                else if (trab != null && prova != null)
                {
                    throw new NotImplementedException("Operação inválida, uma nota de prova ou trabalho deve ser inserida por vez.");
                }
                return false;
            }
            catch (Exception)
            {
                throw new NotImplementedException("Operação inválida, uma nota de prova ou trabalho deve ser inserida por vez.");
            }
        }

        public IEnumerable<TrabalhosExtras> RecuperarTrabalhosTurma(int TurmaId)
        {
            var trab = from t in Db.TrabalhosExtras
                       where t.Aluno.Turma.TurmaId.Equals(TurmaId)
                       select t;
            return trab;
        }
    }
}
