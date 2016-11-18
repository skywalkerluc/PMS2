using SchoolManagement.Domain.Entidades;
using System.Collections.Generic;

namespace SchoolManagement.Domain.Interfaces.Servicos
{
    public interface IProvaServico : IServicoBase<Prova>
    {
        Prova RecuperarProva(int ProvaId);
        Prova IncluirProva(Prova prova);
        bool AtualizarDadosProva(Prova prova);
        IEnumerable<Prova> BuscarPorDisciplina(int codDisciplina);
        IEnumerable<Prova> RecuperarProvasProfessor(int ProfessorId);
        IEnumerable<Prova> RecuperarProvasTurma(int TurmaId);
        bool ExcluirProva(int ProvaId);
        IEnumerable<Prova> RecuperarTodasAsProvas();
        IEnumerable<Prova> RecuperarProvasPendentesTurmaProfessor(int ProfessorId, int TurmaId);
        IEnumerable<Prova> RecuperarProvasConcluidas(int TurmaId);

        IEnumerable<Prova> RecuperarProvasConcluidasTurmaProfessor(int ProfessorId, int TurmaId);

        bool AtualizarStatusProva(int ProvaId, int StatusProva);
    }
}
