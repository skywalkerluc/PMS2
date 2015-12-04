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
    public class AnoLetivoServico : ServicoBase<AnoLetivo>, IAnoLetivoServico
    {
        private readonly IAnoLetivoRepositorio _anoLetivoRep;

        public AnoLetivoServico(IAnoLetivoRepositorio anoLetivoRep)
            :base(anoLetivoRep)
        {
            _anoLetivoRep = anoLetivoRep;
        }

        public AnoLetivo IncluirAnoLetivo(AnoLetivo anoLetivo)
        {
            return this._anoLetivoRep.IncluirAnoLetivo(anoLetivo);
        }
    }
}
