using SchoolManagement.Domain.Entidades;
using SchoolManagement.Domain.Interfaces.Repositorios;
using SchoolManagement.Domain.Interfaces.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Servicos
{
    public class TurmaServico : ServicoBase<Turma>, ITurmaServico
    {
        private readonly ITurmaRepositorio turmaRep;

        public TurmaServico(ITurmaRepositorio repositorio)
            : base(repositorio)
        {
            this.turmaRep = repositorio;
        }

        public Turma IncluirTurma(Turma turma)
        {
            return this.turmaRep.IncluirTurma(turma);
        }

        public IEnumerable<Aluno> RecuperarTodosAlunosTurma(int TurmaId)
        {
            return this.turmaRep.RecuperarTodosAlunosTurma(TurmaId);
        }

        public IEnumerable<ResultadosProvas> RecuperarResultadosProvasTurma(int TurmaId)
        {
            return this.turmaRep.RecuperarResultadosProvasTurma(TurmaId);
        }

        public IEnumerable<Turma> FiltrarTurma(string descTurma, int ProfessorId, int AnoLetivo, int horarioId)
        {
            return this.turmaRep.FiltrarTurma(descTurma, ProfessorId, AnoLetivo, horarioId);
        }

        public bool RemoverAlunosTurma(int TurmaId, List<Aluno> ListaAlunos)
        {
            return this.turmaRep.RemoverAlunosTurma(TurmaId, ListaAlunos);
        }

        public bool AdicionarAlunosTurma(int TurmaId, List<Aluno> ListaAlunos)
        {
            return this.turmaRep.AdicionarAlunosTurma(TurmaId, ListaAlunos);
        }

        public IEnumerable<Turma> RecuperarTurmasQueProfessorLeciona(int professorId)
        {
            return this.turmaRep.RecuperarTurmasQueProfessorLeciona(professorId);
        }

        public IEnumerable<Turma> RecuperarTurmasProfessorNaoLeciona(int ProfessorId)
        {
            return this.turmaRep.RecuperarTurmasProfessorNaoLeciona(ProfessorId);
        }

        public Turma RecuperarDadosTurma(int TurmaId)
        {
            return this.turmaRep.RecuperarDadosTurma(TurmaId);
        }

        public bool AtualizarDadosTurma(Turma turma)
        {
            return this.turmaRep.AtualizarDadosTurma(turma);
        }

        
    }
}
