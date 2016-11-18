using SchoolManagement.Domain.Entidades;
using System.Collections.Generic;

namespace SchoolManagement.Domain.Interfaces.Repositorios
{
    public interface INotificacaoRepositorio : IRepositorioBase<Notificacao>
    {
        IEnumerable<Notificacao> BuscarNotificacaoPorAssunto(string assunto, int turma);
        IEnumerable<Notificacao> VisualizarNotificacoesPorTurma(int TurmaId);
        IEnumerable<Notificacao> VisualizarNotificacoesPorCriador(int UsuarioId);
    }
}
