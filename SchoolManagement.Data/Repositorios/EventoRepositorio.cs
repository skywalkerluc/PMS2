using SchoolManagement.Domain.Entidades;
using SchoolManagement.Domain.Interfaces.Repositorios;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Data.Repositorios
{
    public class EventoRepositorio : RepositorioBase<Evento>, IEventoRepositorio
    {
        public Evento IncluirEvento(Evento evento)
        {
            try
            {
                foreach (var prof in evento.FuncionarioResponsavel)
                {
                    Db.Entry(prof).State = EntityState.Unchanged;
                }

                Db.Eventos.Add(evento);
                Db.SaveChanges();

                return evento;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message.ToString());
            }
        }

        public IEnumerable<Evento> BuscarEventoPorDescricao(string descricao)
        {
            return Db.Eventos.Where(p => p.Descricao == descricao);
        }

        public IEnumerable<Evento> RecuperarEventosFuturos()
        {
            var eventos = from e in Db.Eventos
                          where e.DataEvento > DateTime.Now
                          select e;
            return eventos;
        }
    }
}
