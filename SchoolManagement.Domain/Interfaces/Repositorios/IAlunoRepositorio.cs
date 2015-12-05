using SchoolManagement.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Interfaces.Repositorios
{
    public interface IAlunoRepositorio : IRepositorioBase<Aluno>
    {
        Aluno IncluirAluno(Aluno param);
        IEnumerable<Aluno> PesquisarAlunoPorNome(string nomeAluno);
        IEnumerable<Aluno> FiltrarAluno(string nomeAluno, int? turmaId);
        IEnumerable<Aluno> PesquisarAlunoPorNomeEmTurma(string nomeAluno, int codigoTurma);
        IEnumerable<Aluno> RecuperarAlunosTurma(int TurmaId);
        IEnumerable<ResultadosProvas> RecuperarResultadosAluno(Aluno aluno);
    }
}
