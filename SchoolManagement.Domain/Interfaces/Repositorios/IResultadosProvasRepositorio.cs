using SchoolManagement.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Interfaces.Repositorios
{
    public interface IResultadosProvasRepositorio : IRepositorioBase<ResultadosProvas>
    {
        ResultadosProvas IncluirNotaAluno(ResultadosProvas resultadoProva);
        IEnumerable<ResultadosProvas> RecuperarNotasAluno(int AlunoId);
        IEnumerable<ResultadosProvas> RecuperarHistoricoNotasTurma(int TurmaId);
        bool AlterarResultadoAluno(ResultadosProvas resultado);
        bool RemoverResultadosAlunos(List<ResultadosProvas> resultadosProvas);
    }
}
