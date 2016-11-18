using SchoolManagement.Domain.Entidades;
using System.Collections.Generic;

namespace SchoolManagement.Domain.Interfaces.Repositorios
{
    public interface IEventoRepositorio : IRepositorioBase<Evento>
    {
        Evento IncluirEvento(Evento evento);
        bool AtualizarDadosEvento(Evento evento);
        IEnumerable<Evento> BuscarEventoPorDescricao(string descricao);
        IEnumerable<Evento> RecuperarEventosFuturos();
    }
}
