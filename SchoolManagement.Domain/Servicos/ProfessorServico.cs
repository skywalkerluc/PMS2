using SchoolManagement.Domain.Entidades;
using SchoolManagement.Domain.Interfaces.Repositorios;
using SchoolManagement.Domain.Interfaces.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Servicos
{
    public class ProfessorServico : ServicoBase<Professor>, IProfessorServico
    {
        private readonly IProfessorRepositorio _professorRepositorio;

        public ProfessorServico(IProfessorRepositorio professorRepositorio)
            : base(professorRepositorio)
        {
            _professorRepositorio = professorRepositorio;
        }

        public Professor IncluirProfessor(Professor professor)
        {
            return _professorRepositorio.IncluirProfessor(professor);
        }

        public IEnumerable<Professor> BuscarPorNome(string nome)
        {
            return _professorRepositorio.BuscarPorNome(nome);
        }

        public bool VerificarConfiguracoesIdenticas(Professor professor)
        {
            return _professorRepositorio.VerificarConfiguracoesIdenticas(professor);
        }

        public IEnumerable<Professor> VisualizarProfessoresTurma(int TurmaId)
        {
            return _professorRepositorio.VisualizarProfessoresTurma(TurmaId);
        }

    }
}
