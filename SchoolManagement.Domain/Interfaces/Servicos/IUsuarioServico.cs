using SchoolManagement.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
