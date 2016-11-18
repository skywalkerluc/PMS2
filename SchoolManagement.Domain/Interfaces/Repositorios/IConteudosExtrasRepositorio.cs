using SchoolManagement.Domain.Entidades;
using System.Collections.Generic;

namespace SchoolManagement.Domain.Interfaces.Repositorios
{
    public interface IConteudosExtrasRepositorio : IRepositorioBase<ConteudosExtras>
    {
        IEnumerable<ConteudosExtras> RecuperarConteudosExtrasProfessor(int ProfessorId);
        IEnumerable<ConteudosExtras> RecuperarConteudosExtrasTurma(int TurmaId);
    }
}
