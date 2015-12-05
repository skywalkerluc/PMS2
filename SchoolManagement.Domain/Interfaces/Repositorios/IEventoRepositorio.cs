using SchoolManagement.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
