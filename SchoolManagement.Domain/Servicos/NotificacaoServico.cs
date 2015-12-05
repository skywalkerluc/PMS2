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
    public class NotificacaoServico : ServicoBase<Notificacao>, INotificacaoServico
    {
        private readonly INotificacaoRepositorio _notificacaoRep;

        public NotificacaoServico(INotificacaoRepositorio notificacaoRep)
            :base(notificacaoRep)
        {
            _notificacaoRep = notificacaoRep;
        }

        public Notificacao CriarNotificacao(Notificacao notificacao)
        {
            return this._notificacaoRep.CriarNotificacao(notificacao);
        }

        public IEnumerable<Notificacao> BuscarNotificacaoPorAssunto(string assunto, int turma)
        {
            return this._notificacaoRep.BuscarNotificacaoPorAssunto(assunto, turma);
        }
        public bool EditarNotificacao(Notificacao notificacao)
        {
            return this._notificacaoRep.EditarNotificacao(notificacao);
        }

        public IEnumerable<Notificacao> VisualizarNotificacoesMinhaTurma(int TurmaId)
        {
            return this._notificacaoRep.VisualizarNotificacoesMinhaTurma(TurmaId);
        }

        public IEnumerable<Notificacao> VisualizarNotificacoesPorCriador(int UsuarioId)
        {
            return this._notificacaoRep.VisualizarNotificacoesPorCriador(UsuarioId);
        }
    }
}
