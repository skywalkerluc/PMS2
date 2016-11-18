using SchoolManagement.Domain.Entidades;
using System.Collections.Generic;

namespace SchoolManagement.Domain.Interfaces.Servicos
{
    public interface IFrequenciaServico : IServicoBase<Frequencia>
    {
        Frequencia IncluirFrequenciaAluno(Frequencia frequencia);
        bool RemoverFrequencia(int FrequenciaId);
        IEnumerable<Frequencia> RecuperarHistoricoFrequenciasAluno(int AlunoId);
        IEnumerable<Frequencia> RecuperarHistorioFrequenciasTurma(int TurmaId);
        Frequencia AlterarFrequenciaAluno(Frequencia frequencia);
        bool RemoverFrequenciasAlunos(List<Frequencia> frequenciasAlunos);
        Frequencia RecuperarDadosFrequencia(int FrequenciaId);
    }
}
