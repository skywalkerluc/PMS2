using SchoolManagement.Domain.Entidades;
using System.Collections.Generic;

namespace SchoolManagement.Domain.Interfaces.Servicos
{
    public interface INotificacaoServico : IServicoBase<Notificacao>
    {
        Notificacao CriarNotificacao(Notificacao notificacao);
        IEnumerable<Notificacao> BuscarNotificacaoPorAssunto(string assunto, int turma);
        bool EditarNotificacao(Notificacao notificacao);
        IEnumerable<Notificacao> VisualizarNotificacoesMinhaTurma(int TurmaId);
        IEnumerable<Notificacao> VisualizarNotificacoesPorCriador(int UsuarioId);
    }
}
