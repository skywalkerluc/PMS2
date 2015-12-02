using SchoolManagement.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Interfaces.Servicos
{
    public interface IEventoServico : IServicoBase<Evento>
    {
        Evento IncluirEvento(Evento evento);
        IEnumerable<Evento> BuscarEventoPorDescricao(string descricao);
        IEnumerable<Evento> RecuperarEventosFuturos();
    }
}
