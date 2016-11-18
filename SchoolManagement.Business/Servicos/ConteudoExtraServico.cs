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
    public class ConteudoExtraServico : ServicoBase<ConteudosExtras>, IConteudoExtraServico
    {
        private readonly IConteudoExtraRepositorio _repositorioConteudo;

        public ConteudoExtraServico(IConteudoExtraRepositorio repositorioConteudo)
            :base(repositorioConteudo)
        {
            _repositorioConteudo = repositorioConteudo;
        }

        public ConteudosExtras IncluirConteudosExtras(ConteudosExtras conteudosExtras)
        {
            return this._repositorioConteudo.IncluirConteudosExtras(conteudosExtras);
        }

        public bool EditarDadosConteudosExtras(ConteudosExtras conteudosExtras)
        {
            return this._repositorioConteudo.EditarDadosConteudosExtras(conteudosExtras);
        }

        public IEnumerable<ConteudosExtras> RecuperarConteudosExtrasProfessor(int ProfessorId)
        {
            return this._repositorioConteudo.RecuperarConteudosExtrasProfessor(ProfessorId);
        }

        public IEnumerable<ConteudosExtras> RecuperarConteudosExtrasTurma(int TurmaId)
        {
            return _repositorioConteudo.RecuperarConteudosExtrasTurma(TurmaId);
        }

        public IEnumerable<ConteudosExtras> RecuperarProvasProfessor(int ProfessorId)
        {
            return _repositorioConteudo.RecuperarProvasProfessor(ProfessorId);
        }
    }
}
