using SchoolManagement.Domain.Entidades;
using System.Collections.Generic;

namespace SchoolManagement.Domain.Interfaces.Repositorios
{
    public interface IResultadosProvasRepositorio : IRepositorioBase<ResultadosProvas>
    {
        ResultadosProvas IncluirNotaAluno(ResultadosProvas resultadoProva);
        IEnumerable<ResultadosProvas> RecuperarNotasAluno(int AlunoId);
        IEnumerable<ResultadosProvas> RecuperarHistoricoNotasTurma(int TurmaId);
        bool AlterarResultadoAluno(ResultadosProvas resultado);
        bool RemoverResultadosAlunos(List<ResultadosProvas> resultadosProvas);
        List<ResultadosProvas> RecuperarResultadosProva(int ProvaId);
        ResultadosProvas RecuperarResultadosProvasPorId(int ResultadoProvaId);
        bool RemoverResultadoProva(int ResultadoProvaId);
    }
}
