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
    public class DisciplinaServico : ServicoBase<Disciplina>, IDisciplinaServico
    {
        private readonly IDisciplinaRepositorio _disciplinaRep;

        public DisciplinaServico(IDisciplinaRepositorio disciplinaRep)
            : base(disciplinaRep)
        {
            _disciplinaRep = disciplinaRep;
        }

        public Disciplina IncluirDisciplina(Disciplina disciplina)
        {
            return _disciplinaRep.IncluirDisciplina(disciplina);
        }

        public IEnumerable<Disciplina> FiltroDisciplina(string nomeDisciplina, int LivroId)
        {
            return _disciplinaRep.FiltroDisciplina(nomeDisciplina, LivroId);
        }

        public IEnumerable<Disciplina> BuscarPorNome(string nome)
        {
            return this._disciplinaRep.BuscarPorNome(nome);
        }

        public IEnumerable<Disciplina> RecuperarDisciplinasTurma(int TurmaId)
        {
            return _disciplinaRep.RecuperarDisciplinasTurma(TurmaId);
        }

        public bool IncluirDisciplinasEmTurma(int TurmaId, List<Disciplina> ListaDisciplinas)
        {
            return _disciplinaRep.IncluirDisciplinasEmTurma(TurmaId, ListaDisciplinas);
        }

        public bool RemoverDisciplinasTurma(int TurmaId, List<Disciplina> ListaDisciplinas)
        {
            return _disciplinaRep.RemoverDisciplinasTurma(TurmaId, ListaDisciplinas);
        }

        public IEnumerable<Disciplina> RecuperarDisciplinasProfessorLeciona(int ProfessorId)
        {
            return _disciplinaRep.RecuperarDisciplinasProfessorLeciona(ProfessorId);
        }

        public IEnumerable<Disciplina> RecuperarDisciplinasTurmaProfessor(int TurmaId, int ProfessorId)
        {
            return _disciplinaRep.RecuperarDisciplinasTurmaProfessor(TurmaId, ProfessorId);
        }
    }
}
