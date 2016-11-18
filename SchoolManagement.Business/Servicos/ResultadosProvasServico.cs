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
    public class ResultadosProvasServico : ServicoBase<ResultadosProvas>, IResultadosProvasServico
    {
        private readonly IResultadosProvasRepositorio _resultadosProvasRep;

        public ResultadosProvasServico(IResultadosProvasRepositorio resultadosProvasRep)
            : base(resultadosProvasRep)
        {
            _resultadosProvasRep = resultadosProvasRep;
        }

        public ResultadosProvas IncluirNotaAluno(ResultadosProvas resultadoProva)
        {
            return this._resultadosProvasRep.IncluirNotaAluno(resultadoProva);
        }

        public IEnumerable<ResultadosProvas> RecuperarNotasAluno(int AlunoId)
        {
            return this._resultadosProvasRep.RecuperarNotasAluno(AlunoId);
        }

        public IEnumerable<ResultadosProvas> RecuperarHistoricoNotasTurma(int TurmaId)
        {
            return this._resultadosProvasRep.RecuperarHistoricoNotasTurma(TurmaId);
        }

        public bool AlterarResultadoAluno(ResultadosProvas resultado)
        {
            return this._resultadosProvasRep.AlterarResultadoAluno(resultado);
        }

        public bool RemoverResultadosAlunos(List<ResultadosProvas> resultadosProvas)
        {
            return this._resultadosProvasRep.RemoverResultadosAlunos(resultadosProvas);
        }

        public List<ResultadosProvas> RecuperarResultadosProva(int ProvaId)
        {
            return this._resultadosProvasRep.RecuperarResultadosProva(ProvaId);
        }

        public ResultadosProvas RecuperarResultadosProvasPorId(int ResultadoProvaId)
        {
            return this._resultadosProvasRep.RecuperarResultadosProvasPorId(ResultadoProvaId);
        }

        public bool RemoverResultadoProva(int ResultadoProvaId)
        {
            return this._resultadosProvasRep.RemoverResultadoProva(ResultadoProvaId);
        }
    }
}
