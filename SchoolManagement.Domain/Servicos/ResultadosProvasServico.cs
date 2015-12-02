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

        public IEnumerable<ResultadosProvas> RecuperarNotasAluno(int AlunoId)
        {
            return _resultadosProvasRep.RecuperarNotasAluno(AlunoId);
        }
    }
}
