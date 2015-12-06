using SchoolManagement.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
