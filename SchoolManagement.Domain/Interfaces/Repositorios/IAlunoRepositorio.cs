using SchoolManagement.Domain.Entidades;
using System.Collections.Generic;

namespace SchoolManagement.Domain.Interfaces.Repositorios
{
    public interface IAlunoRepositorio : IRepositorioBase<Aluno>
    {
        IEnumerable<Aluno> PesquisarAlunoPorNome(string nomeAluno);
        IEnumerable<Aluno> FiltrarAluno(string nomeAluno, int? turmaId);
        IEnumerable<Aluno> PesquisarAlunoPorNomeEmTurma(string nomeAluno, int codigoTurma);
        IEnumerable<Aluno> RecuperarAlunosTurma(int TurmaId);
    }
}
