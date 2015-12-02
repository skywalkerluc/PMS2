using SchoolManagement.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Interfaces.Servicos
{
    public interface INotificacaoServico : IServicoBase<Notificacao>
    {
        Notificacao CriarNotificacao(Notificacao notificacao);
        IEnumerable<Notificacao> BuscarNotificacaoPorAssunto(string assunto, int turma);
        bool EditarNotificacao(Notificacao notificacao);
        IEnumerable<Notificacao> VisualizarNotificacoesMinhaTurma(int TurmaId);
    }
}
