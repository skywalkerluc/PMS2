using SchoolManagement.Domain.Entidades;
using System.Collections.Generic;

namespace SchoolManagement.Domain.Interfaces.Servicos
{
    public interface IUsuarioServico : IServicoBase<Usuario>
    {
        //metodo da interface para recuperar login. Implementado no UsuarioServico.
        Usuario RecuperarPorLogin(Usuario usuario);
        bool verificarCPFSendoUtilizado(string cpf);
        bool VerificarLoginExistente(string login);
        bool VerificarRGSendoUtilizado(string rg);
        IEnumerable<Notificacao> ExibirNotificacoesUsuario(int indicadorAcesso, int idUsuario);
        //bool VerificarLoginExistente(string login);
    }
}
