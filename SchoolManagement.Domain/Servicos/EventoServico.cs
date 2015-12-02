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
    public class EventoServico : ServicoBase<Evento>, IEventoServico
    {
        private readonly IEventoRepositorio _eventoRep;

        public EventoServico(IEventoRepositorio eventoRep)
            :base(eventoRep)
        {
            _eventoRep = eventoRep;
        }

        public Evento IncluirEvento(Evento evento)
        {
            return this._eventoRep.IncluirEvento(evento);
        }

        public IEnumerable<Evento> BuscarEventoPorDescricao(string descricao)
        {
            return this._eventoRep.BuscarEventoPorDescricao(descricao);
        }

        public IEnumerable<Evento> RecuperarEventosFuturos()
        {
            return this._eventoRep.RecuperarEventosFuturos();
        }
    }
}
