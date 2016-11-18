using SchoolManagement.Domain.Cross;
using SchoolManagement.Domain.Entidades;
using SchoolManagement.Domain.Interfaces.Repositorios;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Data.Repositorios.NewReps
{
    public class NotificacaoRepositorio : RepositorioBase, INotificacaoRepositorio
    {
        public RetornoBase<bool> Atualizar(Notificacao param)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Notificacao> BuscarNotificacaoPorAssunto(string assunto, int turma)
        {
            throw new NotImplementedException();
        }
        #region Procedures



        #endregion

        public RetornoBase<bool> Incluir(Notificacao param)
        {
            RetornoBase<bool> retorno = new RetornoBase<bool>()
            {
                Status = false,
                Mensagem = string.Empty
            };

            try
            {
                if (param == null)
                    return retorno;

                AbrirConexao();
                AdicionarParametro("", param.NotificacaoId, System.Data.SqlDbType.Int);
                AdicionarParametro("", param.Assunto, System.Data.SqlDbType.VarChar);
                AdicionarParametro("", param.DataCriacao, System.Data.SqlDbType.DateTime);
                AdicionarParametro("", param.Descricao, System.Data.SqlDbType.VarChar);
                if (param.TurmaPublicoAlvo != null)
                    AdicionarParametro("", param.TurmaPublicoAlvo.TurmaId, System.Data.SqlDbType.Int);
                AdicionarParametro("", param.UsuarioCriacao.Id, System.Data.SqlDbType.Int);

                return retorno;


            }
            catch (Exception ex)
            {
                retorno.Mensagem = ex.Message.ToString();
                return retorno;
            }
        }

        public RetornoBase Recuperar(int id)
        {
            throw new NotImplementedException();
        }

        public RetornoBase<Notificacao> RecuperarTodos()
        {
            throw new NotImplementedException();
        }

        public RetornoBase<bool> Remover(Notificacao param)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Notificacao> VisualizarNotificacoesPorCriador(int UsuarioId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Notificacao> VisualizarNotificacoesPorTurma(int TurmaId)
        {
            throw new NotImplementedException();
        }
    }
}
