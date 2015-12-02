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

        public IEnumerable<Turma> FiltrarTurma(string descTurma, Professor professor, AnoLetivo ano, int horarioId)
        {
            return this.turmaRep.FiltrarTurma(descTurma, professor, ano, horarioId);
        }
    }
}
