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
    public class UsuarioServico : ServicoBase<Usuario>, IUsuarioServico
    {
        private readonly IRepositorioUsuario usuarioRepositorio;

        public UsuarioServico(IRepositorioUsuario repositorio)
            : base(repositorio)
        {
            this.usuarioRepositorio = repositorio;
        }

        //Implementação do metodo da interface Usuario Serviço para recuperar o login do repositorio.
        public Usuario RecuperarPorLogin(Usuario usuario)
        {
            return this.usuarioRepositorio.RecuperarPorLogin(usuario);
        }

        public bool verificarCPFSendoUtilizado(string cpf)
        {
            return this.usuarioRepositorio.verificarCPFSendoUtilizado(cpf);
        }

        public bool VerificarLoginExistente(string login)
        {
            return this.usuarioRepositorio.VerificarLoginExistente(login);
        }

        public bool VerificarRGSendoUtilizado(string rg)
        {
            return this.usuarioRepositorio.VerificarRGSendoUtilizado(rg);
        }

        public IEnumerable<Notificacao> ExibirNotificacoesUsuario(int indicadorAcesso, int idUsuario)
        {
            return this.usuarioRepositorio.ExibirNotificacoesUsuario(indicadorAcesso, idUsuario);
        }
    }
}
