using SchoolManagement.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Interfaces.Repositorios
{
    public interface IFrequenciaRepositorio : IRepositorioBase<Frequencia>
    {
        Frequencia IncluirFrequenciaAluno(Frequencia frequencia);
        IEnumerable<Frequencia> RecuperarHistoricoFrequenciasAluno(int AlunoId);
        Frequencia AlterarFrequenciaAluno(Frequencia frequencia);
        bool RemoverFrequenciasAlunos(List<Frequencia> frequenciasAlunos);
    }
}
