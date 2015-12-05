using SchoolManagement.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Interfaces.Servicos
{
    public interface IResultadosProvasServico : IServicoBase<ResultadosProvas>
    {
        ResultadosProvas IncluirNotaAluno(ResultadosProvas resultadoProva);
        IEnumerable<ResultadosProvas> RecuperarNotasAluno(int AlunoId);
        IEnumerable<ResultadosProvas> RecuperarHistoricoNotasTurma(int TurmaId);
        bool AlterarResultadoAluno(ResultadosProvas resultado);
        bool RemoverResultadosAlunos(List<ResultadosProvas> resultadosProvas);
    }
}
