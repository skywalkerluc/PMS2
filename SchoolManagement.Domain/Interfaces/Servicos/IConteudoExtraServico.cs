using SchoolManagement.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
