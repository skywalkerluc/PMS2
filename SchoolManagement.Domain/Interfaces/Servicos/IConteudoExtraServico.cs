using SchoolManagement.Domain.Entidades;
using System.Collections.Generic;

namespace SchoolManagement.Domain.Interfaces.Servicos
{
    public interface IConteudoExtraServico : IServicoBase<ConteudosExtras>
    {
        ConteudosExtras IncluirConteudosExtras(ConteudosExtras conteudosExtras);
        bool EditarDadosConteudosExtras(ConteudosExtras conteudosExtras);
        IEnumerable<ConteudosExtras> RecuperarConteudosExtrasProfessor(int ProfessorId);
        IEnumerable<ConteudosExtras> RecuperarConteudosExtrasTurma(int TurmaId);
        IEnumerable<ConteudosExtras> RecuperarProvasProfessor(int ProfessorId);
    }
}
