﻿using SchoolManagement.Domain.Entidades;
using SchoolManagement.Domain.Interfaces.Repositorios;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
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
                if (evento.FuncionarioResponsavel != null)
                {
                    Db.Entry(evento.FuncionarioResponsavel).State = EntityState.Unchanged;
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

        public bool AtualizarDadosEvento(Evento evento)
        {
            try
            {
                var eventoIdParameter = new SqlParameter("@EventoId", evento.EventoId);
                var localParameter = new SqlParameter("@Local", evento.Local);
                var descricaoParameter = new SqlParameter("@Descricao", evento.Descricao);
                var dataEventoParameter = new SqlParameter("@DataEvento", evento.DataEvento);
                var necessidadeAprovacaoParameter = new SqlParameter("@NecessidadeAprovacao", evento.NecessidadeAprovacao);
                var precoEventoParameter = new SqlParameter("@PrecoEvento", evento.PrecoEvento);

                var query = this.Db.Database.ExecuteSqlCommand("UPDATE Evento SET Local = @Local, Descricao = @Descricao, DataEvento = @DataEvento, NecessidadeAprovacao = @NecessidadeAprovacao, PrecoEvento = @PrecoEvento WHERE EventoId = @EventoId", eventoIdParameter, localParameter, descricaoParameter, dataEventoParameter, necessidadeAprovacaoParameter, precoEventoParameter);
                return true;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message.ToString());
            }
        }

        public IEnumerable<Evento> BuscarEventoPorDescricao(string descricao)
        {
            return Db.Eventos.Where(p => p.Descricao.Contains(descricao));
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
