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
    public class FrequenciaServico : ServicoBase<Frequencia>, IFrequenciaServico
    {
        private readonly IFrequenciaRepositorio _frequenciaRep;

        public FrequenciaServico(IFrequenciaRepositorio frequenciaRep)
            :base(frequenciaRep)
        {
            _frequenciaRep = frequenciaRep;
        }
    }
}
