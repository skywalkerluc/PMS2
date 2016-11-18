using SchoolManagement.Domain.Entidades;
using System.Collections.Generic;

namespace SchoolManagement.Domain.Interfaces.Servicos
{
    public interface IAlunoServico : IServicoBase<Aluno>
    {
        Aluno IncluirAluno(Aluno param);
        IEnumerable<Aluno> PesquisarAlunoPorNome(string nomeAluno);
        IEnumerable<Aluno> FiltrarAluno(string nomeAluno, int? turmaId);
        IEnumerable<Aluno> PesquisarAlunoPorNomeEmTurma(string nomeAluno, int codigoTurma);
        IEnumerable<Aluno> RecuperarAlunosTurma(int TurmaId);
        IEnumerable<ResultadosProvas> RecuperarResultadosAluno(Aluno aluno);
        Aluno RecuperarDadosAluno(int AlunoId);
        bool AtualizarDadosAluno(Aluno aluno);
        bool RemoverAluno(int AlunoId);
    }
}
