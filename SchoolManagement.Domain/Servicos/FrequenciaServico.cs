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

        public Frequencia IncluirFrequenciaAluno(Frequencia frequencia)
        {
            return this._frequenciaRep.IncluirFrequenciaAluno(frequencia);
        }

        public IEnumerable<Frequencia> RecuperarHistoricoFrequenciasAluno(int AlunoId)
        {
            return this._frequenciaRep.RecuperarHistoricoFrequenciasAluno(AlunoId);
        }

        public IEnumerable<Frequencia> RecuperarHistorioFrequenciasTurma(int TurmaId)
        {
            return this._frequenciaRep.RecuperarHistorioFrequenciasTurma(TurmaId);
        }

        public Frequencia AlterarFrequenciaAluno(Frequencia frequencia)
        {
            return this._frequenciaRep.AlterarFrequenciaAluno(frequencia);
        }

        public bool RemoverFrequenciasAlunos(List<Frequencia> frequenciasAlunos)
        {
            return this._frequenciaRep.RemoverFrequenciasAlunos(frequenciasAlunos);
        }
    }
}
