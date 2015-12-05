using SchoolManagement.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Interfaces.Servicos
{
    public interface IProfessorServico : IServicoBase<Professor>
    {
        Professor IncluirProfessor(Professor professor);
        IEnumerable<Professor> BuscarPorNome(string nome);
        bool VerificarConfiguracoesIdenticas(Professor professor);
        IEnumerable<Professor> VisualizarProfessoresTurma(int TurmaId);
    }
}
