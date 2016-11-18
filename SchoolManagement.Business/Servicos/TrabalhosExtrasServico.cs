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
    public class TrabalhosExtrasServico : ServicoBase<TrabalhosExtras>, ITrabalhosExtrasServico
    {
        private readonly ITrabalhosExtrasRepositorio _trabalhosExtrasRep;

        public TrabalhosExtrasServico(ITrabalhosExtrasRepositorio trabalhosExtrasRep)
            :base (trabalhosExtrasRep)
        {
            _trabalhosExtrasRep = trabalhosExtrasRep;
        }

        public bool IncluirTrabalhoExtra(TrabalhosExtras trabalhoExtra)
        {
            return _trabalhosExtrasRep.IncluirTrabalhoExtra(trabalhoExtra);
        }

        public bool LancarNota(Aluno aluno, Professor professor, TrabalhosExtras trab, Prova prova, int nota)
        {
            return _trabalhosExtrasRep.LancarNota(aluno, professor, trab, prova, nota);
        }

        public IEnumerable<TrabalhosExtras> RecuperarTrabalhosTurma(int TurmaId)
        {
            return _trabalhosExtrasRep.RecuperarTrabalhosTurma(TurmaId);
        }

    }
}
