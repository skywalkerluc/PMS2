using SchoolManagement.Domain.Entidades;
using SchoolManagement.Domain.Interfaces.Repositorios;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Data.Repositorios
{
    public class UsuarioRepositorio : RepositorioBase<Usuario>, IRepositorioUsuario
    {
        /// <summary>
        /// Implementação do método para autenticar login e recuperar o identificador do usuário
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns>Identificador do Usuário</returns>
        public Usuario RecuperarPorLogin(Usuario usuario)
        {
            return Db.Usuarios.Where(a => a.UserLogin.Equals(usuario.UserLogin) && a.Senha.Equals(usuario.Senha)).First();
        }

        /// <summary>
        /// Verificar se CPF usado em cadastro existe no banco
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        public bool verificarCPFSendoUtilizado(string cpf)
        {
            var cpfIdentico = Db.Usuarios.Where(c => c.Cpf.Contains(cpf));

            if (cpfIdentico.Count() > 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Verificar se login usado em cadastro já existe no banco
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public bool VerificarLoginExistente(string login)
        {
            var identico = Db.Usuarios.Where(p => p.UserLogin.Contains(login));

            if (identico.Count() > 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Verificar se RG usado em cadastro já existe no banco
        /// </summary>
        /// <param name="rg"></param>
        /// <returns></returns>
        public bool VerificarRGSendoUtilizado(string rg)
        {
            var identico = Db.Usuarios.Where(p => p.Rg.Contains(rg));

            if (identico.Count() > 0)
                return false;
            else
                return true;
        }


        public IEnumerable<Notificacao> ExibirNotificacoesUsuario(int indicadorAcesso, int idUsuario)
        {
            try
            {
                IEnumerable<Notificacao> TodasAsNotificacoes = RecuperarTodasNotificacoes();
                List<Notificacao> ListaNotificacao = new List<Notificacao>();

                var usuario = Recuperar(idUsuario);
                if (usuario.indicadorAcesso != indicadorAcesso)
                    throw new ArgumentException("Argumentos inválidos!");
                
                if (usuario.indicadorAcesso == 2)
                {
                    var aluno = Db.Alunos.Where(a => a.Id == idUsuario).FirstOrDefault();

                    foreach (var notif in TodasAsNotificacoes)
                    {
                        if (notif.TurmaPublicoAlvo.TurmaId == aluno.Turma.TurmaId)
                        {
                            ListaNotificacao.Add(notif);
                        }
                    }
                    IEnumerable<Notificacao> RetornoNotificacao = ListaNotificacao;
                    return RetornoNotificacao;
                }

                else if(usuario.indicadorAcesso == 4)
                {
                    var professor = Db.Professores.Where(a => a.Id == idUsuario).FirstOrDefault();

                    foreach (var notificacao in TodasAsNotificacoes)
                    {
                        foreach (var professorPublicoAlvo in notificacao.ProfessoresPublicoAlvo)
                        {
                            if(professor.Id == professorPublicoAlvo.Id)
                            {
                                ListaNotificacao.Add(notificacao);
                            }
                        }
                    }
                    IEnumerable<Notificacao> RetornoNotificacao = ListaNotificacao;
                    return RetornoNotificacao;
                }
                else if(usuario.indicadorAcesso == 6)
                {
                    return TodasAsNotificacoes;
                }
                else
                {
                    throw new ArgumentException("Argumentos inválidos!");
                }
            }
            catch (Exception ex) 
            {
                throw new NotImplementedException(ex.Message.ToString());
            }
        }

        private IEnumerable<Notificacao> RecuperarTodasNotificacoes()
        {
            var notific =
                            from a in Db.Notificacoes
                            select a;

            IEnumerable<Notificacao> notificacoes = notific;
            return notificacoes;
        }

    }
}
