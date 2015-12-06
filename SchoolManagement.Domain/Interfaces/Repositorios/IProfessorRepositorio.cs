using SchoolManagement.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Interfaces.Repositorios
{
    public interface IProfessorRepositorio : IRepositorioBase<Professor>
    {
        Professor IncluirProfessor(Professor professor);
        IEnumerable<Professor> BuscarPorNome(string nome);
        bool VerificarConfiguracoesIdenticas(Professor professor);
        IEnumerable<Professor> VisualizarProfessoresTurma(int TurmaId);
        bool IncluirProfessorEmTurma(int ProfessorId, int TurmaId);
        bool RemoverProfessorDeTurma(int ProfessorId, int TurmaId);
    }
}
