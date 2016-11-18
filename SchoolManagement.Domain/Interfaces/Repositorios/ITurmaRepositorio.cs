using SchoolManagement.Domain.Entidades;
using System.Collections.Generic;

namespace SchoolManagement.Domain.Interfaces.Repositorios
{
    public interface ITurmaRepositorio : IRepositorioBase<Turma>
    {
        Turma IncluirTurma(Turma turma);
        IEnumerable<Aluno> RecuperarTodosAlunosTurma(int TurmaId);
        IEnumerable<Turma> FiltrarTurma(string descTurma, int ProfessorId, int AnoLetivo, int horarioId);
        IEnumerable<ResultadosProvas> RecuperarResultadosProvasTurma(int TurmaId);
        bool RemoverAlunosTurma(int TurmaId, List<Aluno> ListaAlunos);
        bool AdicionarAlunosTurma(int TurmaId, List<Aluno> ListaAlunos);
        IEnumerable<Turma> RecuperarTurmasQueProfessorLeciona(int professorId);
        IEnumerable<Turma> RecuperarTurmasProfessorNaoLeciona(int ProfessorId);
        Turma RecuperarDadosTurma(int TurmaId);
        bool AtualizarDadosTurma(Turma turma);
    }
}
