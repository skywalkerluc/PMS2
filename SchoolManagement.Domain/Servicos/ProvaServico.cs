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
    public class ProvaServico : ServicoBase<Prova>, IProvaServico
    {
        private readonly IProvaRepositorio _provaRep;

        public ProvaServico(IProvaRepositorio provaRep)
            :base (provaRep)
        {
            _provaRep = provaRep;
        }

        public Prova IncluirProva(Prova prova)
        {
            return this._provaRep.IncluirProva(prova);
        }

        public bool AtualizarDadosProva(Prova prova)
        {
            return this._provaRep.AtualizarDadosProva(prova);
        }

        public IEnumerable<Prova> BuscarPorDisciplina(int codDisciplina)
        {
            return this._provaRep.BuscarPorDisciplina(codDisciplina);
        }

        public IEnumerable<Prova> RecuperarProvasProfessor(int ProfessorId)
        {
            return this._provaRep.RecuperarProvasProfessor(ProfessorId);
        }

        public IEnumerable<Prova> RecuperarProvasTurma(int TurmaId)
        {
            return this._provaRep.RecuperarProvasTurma(TurmaId);
        }
    }
}
