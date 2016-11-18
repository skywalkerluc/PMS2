using SchoolManagement.Domain.Entidades;
using System.Collections.Generic;

namespace SchoolManagement.Domain.Interfaces.Servicos
{
    public interface IEventoServico : IServicoBase<Evento>
    {
        Evento IncluirEvento(Evento evento);
        bool AtualizarDadosEvento(Evento evento);
        IEnumerable<Evento> BuscarEventoPorDescricao(string descricao);
        IEnumerable<Evento> RecuperarEventosFuturos();
    }
}
