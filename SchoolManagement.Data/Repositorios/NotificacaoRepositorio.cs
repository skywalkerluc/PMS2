using SchoolManagement.Domain.Entidades;
using SchoolManagement.Domain.Interfaces.Repositorios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Data.Repositorios
{
    public class NotificacaoRepositorio : RepositorioBase<Notificacao>, INotificacaoRepositorio
    {
        public Notificacao CriarNotificacao(Notificacao notificacao)
        {
            try
            {
                if (notificacao.TurmaPublicoAlvo != null)
                {
                    Db.Entry(notificacao.TurmaPublicoAlvo).State = EntityState.Unchanged;
                }
                Db.Entry(notificacao.UsuarioCriacao).State = EntityState.Unchanged;
                Db.Notificacoes.Add(notificacao);
                Db.SaveChanges();

                return notificacao;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message.ToString());
            }
        }

        public bool EditarNotificacao(Notificacao notificacao)
        {
            try
            {
                var NotificacaoIdParameter = new SqlParameter("@NotificacaoId", notificacao.NotificacaoId);
                var NotificacaoAssuntoParameter = new SqlParameter("@NotificacaoAssunto", notificacao.Assunto);
                var NotificacaoDescricaoParameter = new SqlParameter("@NotificacaoDescricao", notificacao.Descricao);

                var query = Db.Database.ExecuteSqlCommand("UPDATE Notificacao SET Assunto = @NotificacaoAssunto, Descricao = @NotificacaoDescricao WHERE NotificacaoId = @NotificacaoId", NotificacaoIdParameter, NotificacaoAssuntoParameter, NotificacaoDescricaoParameter);
                return true;


            }
            catch (Exception)
            {
                return false;
            }


        }

        public IEnumerable<Notificacao> BuscarNotificacaoPorAssunto(string assunto, int turma)
        {
            //return Db.Notificacoes.Where(p => p.Assunto.Contains(assunto) || p.TurmaPublicoAlvo.TurmaId.Equals(turma));

            var assuntoETurmaEmNotificacao =
                from a in Db.Notificacoes
                where (a.Assunto.Contains(assunto) || a.TurmaPublicoAlvo.TurmaId == turma)
                select a;

            IEnumerable<Notificacao> FiltroNotificacoes = assuntoETurmaEmNotificacao;

            return FiltroNotificacoes;

        }

        public IEnumerable<Notificacao> VisualizarNotificacoesMinhaTurma(int TurmaId)
        {
            var notif = from n in Db.Notificacoes
                        where n.TurmaPublicoAlvo.TurmaId == TurmaId
                        select n;
            return notif;
        }

        public IEnumerable<Notificacao> VisualizarNotificacoesPorCriador(int UsuarioId)
        {
            var notif = from n in Db.Notificacoes
                        where n.UsuarioCriacao.Id == UsuarioId
                        select n;
            return notif;
        }

        private void tratarParametros(Object valorAtributo, SqlParameter parametro)
        {
            if (valorAtributo == null)
            {
                parametro.SqlValue = DBNull.Value;
            }
            else if ((valorAtributo != null) &&
                     (valorAtributo.GetType() == typeof(String)) &&
                     (String.IsNullOrEmpty((String)valorAtributo)))
            {
                parametro.SqlValue = DBNull.Value;
            }
            else
            {
                parametro.SqlValue = valorAtributo;
            }
        }

    }
}

